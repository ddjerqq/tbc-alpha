using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "outbox_message",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    type = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    content = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: false),
                    occured_on_utc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    processed_on_utc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    error = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_outbox_message", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    full_name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    password_hash = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    credit_utilization = table.Column<float>(type: "REAL", nullable: false),
                    historical_spending = table.Column<string>(type: "TEXT", nullable: false),
                    risk_tolerance = table.Column<int>(type: "INTEGER", nullable: false),
                    preferred_currency = table.Column<string>(type: "TEXT", nullable: false),
                    annual_income = table.Column<string>(type: "TEXT", nullable: false),
                    annual_needs = table.Column<string>(type: "TEXT", nullable: false),
                    annual_savings = table.Column<string>(type: "TEXT", nullable: false),
                    annual_wants = table.Column<string>(type: "TEXT", nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    created_by = table.Column<string>(type: "TEXT", nullable: true),
                    last_modified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    last_modified_by = table.Column<string>(type: "TEXT", nullable: true),
                    deleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    deleted_by = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    owner_id = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    currency = table.Column<string>(type: "TEXT", nullable: false),
                    balance = table.Column<string>(type: "TEXT", nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    created_by = table.Column<string>(type: "TEXT", nullable: true),
                    last_modified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    last_modified_by = table.Column<string>(type: "TEXT", nullable: true),
                    deleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    deleted_by = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_account", x => x.id);
                    table.ForeignKey(
                        name: "f_k_account_user_owner_id",
                        column: x => x.owner_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "saving_goal",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    owner_id = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    value = table.Column<string>(type: "TEXT", nullable: false),
                    years = table.Column<int>(type: "INTEGER", nullable: false),
                    level = table.Column<int>(type: "INTEGER", nullable: false),
                    created = table.Column<DateTime>(type: "TEXT", nullable: true),
                    created_by = table.Column<string>(type: "TEXT", nullable: true),
                    last_modified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    last_modified_by = table.Column<string>(type: "TEXT", nullable: true),
                    deleted = table.Column<DateTime>(type: "TEXT", nullable: true),
                    deleted_by = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_saving_goal", x => x.id);
                    table.ForeignKey(
                        name: "f_k_saving_goal_user_owner_id",
                        column: x => x.owner_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "i_x_account_owner_id",
                table: "account",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "i_x_saving_goal_owner_id",
                table: "saving_goal",
                column: "owner_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "outbox_message");

            migrationBuilder.DropTable(
                name: "saving_goal");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
