using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "annual_income",
                table: "user");

            migrationBuilder.DropColumn(
                name: "annual_needs",
                table: "user");

            migrationBuilder.DropColumn(
                name: "annual_savings",
                table: "user");

            migrationBuilder.DropColumn(
                name: "annual_wants",
                table: "user");

            migrationBuilder.DropColumn(
                name: "credit_utilization",
                table: "user");

            migrationBuilder.DropColumn(
                name: "historical_spending",
                table: "user");

            migrationBuilder.DropColumn(
                name: "risk_tolerance",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "saving_goal",
                newName: "total");

            migrationBuilder.AddColumn<string>(
                name: "amount_saved",
                table: "saving_goal",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    amount = table.Column<string>(type: "TEXT", nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    transaction_category = table.Column<int>(type: "INTEGER", nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    created_by = table.Column<string>(type: "TEXT", nullable: true),
                    last_modified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    last_modified_by = table.Column<string>(type: "TEXT", nullable: true),
                    deleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    deleted_by = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_transaction", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transaction_participant",
                columns: table => new
                {
                    transaction_id = table.Column<string>(type: "TEXT", nullable: false),
                    account_id = table.Column<string>(type: "TEXT", nullable: false),
                    is_sender = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_transaction_participant", x => new { x.transaction_id, x.account_id });
                    table.ForeignKey(
                        name: "f_k_transaction_participant_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "f_k_transaction_participant_transaction_transaction_id",
                        column: x => x.transaction_id,
                        principalTable: "transaction",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "i_x_transaction_participant_account_id",
                table: "transaction_participant",
                column: "account_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transaction_participant");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropColumn(
                name: "amount_saved",
                table: "saving_goal");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "saving_goal",
                newName: "value");

            migrationBuilder.AddColumn<string>(
                name: "annual_income",
                table: "user",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "annual_needs",
                table: "user",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "annual_savings",
                table: "user",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "annual_wants",
                table: "user",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "credit_utilization",
                table: "user",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "historical_spending",
                table: "user",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "risk_tolerance",
                table: "user",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
