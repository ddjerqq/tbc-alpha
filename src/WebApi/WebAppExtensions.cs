using Domain.Aggregates;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Serilog;
using WebApi.Middleware;

namespace WebApi;

/// <summary>
/// Web application extensions
/// </summary>
public static class WebAppExt
{
    private static User GetDefaultUser()
    {
        var userId = Ulid.Parse("01j67mbsgdp53bw4ztm3200xz6");

        var user = new User(userId)
        {
            FullName = "john smith",
            Email = "ddjerqq@gmail.com",
            PasswordHash = BC.EnhancedHashPassword("password"),
            DateOfBirth = new DateTime(2001, 10, 1),
            EmploymentStatus = new EmploymentStatus.Employed(true),
            PreferredCurrency = (Currency)"USD",
            SavingGoals = [],
            Accounts = [],
        };

        user.Accounts.Add(new Account(Iban.Generate(user.Id.Time.Ticks))
        {
            OwnerId = user.Id,
            Owner = user,
            Name = "master",
            Currency = (Currency)"USD",
            Balance = new Money((Currency)"USD", 100_000),
            Transactions = [],
        });

        user.SavingGoals.Add(new SavingGoal(Ulid.Parse("01j67mbsgdp53bw4ztm3200xy5"))
        {
            OwnerId = user.Id,
            Owner = user,
            Name = "Vacation",
            AmountSaved = new Money((Currency)"USD", 3_000),
            Total = new Money((Currency)"USD", 10_000),
            Years = 1,
            Level = Level.Low,
        });

        user.SavingGoals.Add(new SavingGoal(Ulid.Parse("01j67mbsgdp53bw4ztm3200xz3"))
        {
            OwnerId = user.Id,
            Owner = user,
            Name = "Emergency",
            AmountSaved = new Money((Currency)"USD", 12_000),
            Total = new Money((Currency)"USD", 30_000),
            Years = 4,
            Level = Level.High,
        });

        return user;
    }

    /// <summary>
    /// Apply any pending migrations to the database if any.
    /// </summary>
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if ((await dbContext.Database.GetPendingMigrationsAsync()).ToList() is { Count: > 0 } migrations)
        {
            Log.Information("Applying migrations: {@Migrations}", string.Join(", ", migrations));
            await dbContext.Database.MigrateAsync();
        }

        var defaultUser = GetDefaultUser();
        if (await dbContext.Users.FindAsync(defaultUser.Id) is null)
        {
            Log.Information("adding default user");
            dbContext.Users.Add(defaultUser);
        }

        Log.Information("All migrations applied");

        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Use general web app middleware
    /// </summary>
    public static void UseApplicationMiddleware(this WebApplication app)
    {
        app.UseGlobalExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseIdempotency();
        }

        if (app.Environment.IsProduction())
        {
            // compress and then cache static files only in production
            app.UseResponseCompression();
            app.UseResponseCaching();
        }

        app.UseStaticFiles();

        app.UseRouting();
        app.UseRateLimiter();
        app.UseRequestLocalization();

        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();
        app.UseCustomHeaderMiddleware();

        app.MapAppHealthChecks();
        app.MapControllers();

        app.MapDefaultControllerRoute();

        app.MapFallback(httpContext =>
        {
            httpContext.Response.StatusCode = StatusCodes.Status302Found;
            httpContext.Response.Redirect("/404");
            return Task.CompletedTask;
        });
    }

    private static void UseCustomHeaderMiddleware(this WebApplication app)
    {
        app.Use((ctx, next) =>
        {
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
            ctx.Response.Headers.Append("X-Frame-Options", "DENY");

            // // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
            // ctx.Response.Headers.Append("X-Content-Type-Options", "nosniff");

            return next();
        });
    }

    private static void UseGlobalExceptionHandler(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }

    private static void MapAppHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/api/v1/health", new HealthCheckOptions
        {
            // if the predicate is null, then all checks are included
            // Predicate = _ => true,
            AllowCachingResponses = false,
        });
    }
}