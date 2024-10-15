using Application.Common;
using Application.Services;
using Domain.Common;
using Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using Serilog;
using Serilog.Events;
using SerilogTracing;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public sealed class ProcessOutboxMessagesBackgroundJob(IPublisher publisher, IAppDbContext dbContext) : IJob
{
    public static readonly JobKey Key = new("process_outbox_messages");
    private static readonly int MessagesPerBatch = int.Parse("OUTBOX__MESSAGES_PER_BATCH".FromEnv("20"));

    public async Task Execute(IJobExecutionContext context)
    {
        var isAny = await dbContext
            .Set<OutboxMessage>()
            .CountAsync(m => m.ProcessedOnUtc == null);

        if (isAny == 0) return;

        var messages = await dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .OrderBy(m => m.OccuredOnUtc)
            .Take(MessagesPerBatch)
            .ToListAsync(context.CancellationToken);

        foreach (var message in messages)
        {
            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content, OutboxMessage.JsonSerializerSettings);

            if (domainEvent is null)
            {
                // fatal
                Log.Logger.Fatal("failed to deserialize message {@MessageId}", message.Id);
                continue;
            }

            using var activity = Log.Logger.StartActivity("Publish {@DomainEvent}", domainEvent);

            try
            {
                await publisher.Publish(domainEvent, context.CancellationToken);
            }
            catch (Exception ex)
            {
                message.Error = ex.ToString();
                Log.Logger.Fatal("failed to process message {@MessageId}", message.Id);
                activity.Complete(LogEventLevel.Fatal, ex);
            }
            finally
            {
                message.ProcessedOnUtc = DateTime.UtcNow;
                await Log.CloseAndFlushAsync();
            }
        }

        // TODO do we save changes here? or right after publishing each event?
        await dbContext.SaveChangesAsync(context.CancellationToken);
    }
}