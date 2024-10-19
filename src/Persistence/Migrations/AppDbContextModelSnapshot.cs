﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Application.Common.OutboxMessage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("TEXT")
                        .HasColumnName("content");

                    b.Property<string>("Error")
                        .HasMaxLength(1024)
                        .HasColumnType("TEXT")
                        .HasColumnName("error");

                    b.Property<DateTime>("OccuredOnUtc")
                        .HasColumnType("TEXT")
                        .HasColumnName("occured_on_utc");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("TEXT")
                        .HasColumnName("processed_on_utc");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("p_k_outbox_message");

                    b.ToTable("outbox_message");
                });

            modelBuilder.Entity("Domain.Aggregates.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("TEXT")
                        .HasColumnName("created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT")
                        .HasColumnName("date_of_birth");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("TEXT")
                        .HasColumnName("deleted");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("deleted_by");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT")
                        .HasColumnName("email");

                    b.Property<string>("EmploymentStatus")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("employment_status");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT")
                        .HasColumnName("full_name");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_modified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_modified_by");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT")
                        .HasColumnName("password_hash");

                    b.Property<string>("PreferredCurrency")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("preferred_currency");

                    b.HasKey("Id")
                        .HasName("p_k_user");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("TEXT")
                        .HasColumnName("created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_by");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("currency");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("TEXT")
                        .HasColumnName("deleted");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("deleted_by");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_modified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_modified_by");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("owner_id");

                    b.HasKey("Id")
                        .HasName("p_k_account");

                    b.HasIndex("OwnerId")
                        .HasDatabaseName("i_x_account_owner_id");

                    b.ToTable("account");
                });

            modelBuilder.Entity("Domain.Entities.SavingGoal", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("AmountSaved")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("amount_saved");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("TEXT")
                        .HasColumnName("created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("TEXT")
                        .HasColumnName("deleted");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("deleted_by");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_modified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_modified_by");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("owner_id");

                    b.Property<string>("Total")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("total");

                    b.Property<int>("Years")
                        .HasColumnType("INTEGER")
                        .HasColumnName("years");

                    b.HasKey("Id")
                        .HasName("p_k_saving_goal");

                    b.HasIndex("OwnerId")
                        .HasDatabaseName("i_x_saving_goal_owner_id");

                    b.ToTable("saving_goal");
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("amount");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("TEXT")
                        .HasColumnName("created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT")
                        .HasColumnName("date");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("TEXT")
                        .HasColumnName("deleted");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("deleted_by");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_modified");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("last_modified_by");

                    b.Property<int>("TransactionCategory")
                        .HasColumnType("INTEGER")
                        .HasColumnName("transaction_category");

                    b.HasKey("Id")
                        .HasName("p_k_transaction");

                    b.ToTable("transaction");
                });

            modelBuilder.Entity("Domain.ValueObjects.TransactionParticipant", b =>
                {
                    b.Property<string>("TransactionId")
                        .HasColumnType("TEXT")
                        .HasColumnName("transaction_id");

                    b.Property<string>("AccountId")
                        .HasColumnType("TEXT")
                        .HasColumnName("account_id");

                    b.Property<bool>("IsSender")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_sender");

                    b.HasKey("TransactionId", "AccountId")
                        .HasName("p_k_transaction_participant");

                    b.HasIndex("AccountId")
                        .HasDatabaseName("i_x_transaction_participant_account_id");

                    b.ToTable("transaction_participant");
                });

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.HasOne("Domain.Aggregates.User", "Owner")
                        .WithMany("Accounts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_account_user_owner_id");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Entities.SavingGoal", b =>
                {
                    b.HasOne("Domain.Aggregates.User", "Owner")
                        .WithMany("SavingGoals")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_saving_goal_user_owner_id");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.ValueObjects.TransactionParticipant", b =>
                {
                    b.HasOne("Domain.Entities.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_transaction_participant_account_account_id");

                    b.HasOne("Domain.Entities.Transaction", "Transaction")
                        .WithMany("Participants")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_transaction_participant_transaction_transaction_id");

                    b.Navigation("Account");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Domain.Aggregates.User", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("SavingGoals");
                });

            modelBuilder.Entity("Domain.Entities.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Domain.Entities.Transaction", b =>
                {
                    b.Navigation("Participants");
                });
#pragma warning restore 612, 618
        }
    }
}
