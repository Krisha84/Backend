using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendExam.Migrations
{
    /// <inheritdoc />
    public partial class BackendExam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rolesid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_Rolesid",
                        column: x => x.Rolesid,
                        principalTable: "Roles",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    priority = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    assigned_to = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usersid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_Usersid",
                        column: x => x.Usersid,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ticket_Status_Logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticket_id = table.Column<int>(type: "int", nullable: false),
                    old_status = table.Column<int>(type: "int", nullable: false),
                    new_status = table.Column<int>(type: "int", nullable: false),
                    changed_by = table.Column<int>(type: "int", nullable: false),
                    changed_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ticketsid = table.Column<int>(type: "int", nullable: true),
                    Usersid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket_Status_Logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_ticket_Status_Logs_Tickets_Ticketsid",
                        column: x => x.Ticketsid,
                        principalTable: "Tickets",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_ticket_Status_Logs_Users_Usersid",
                        column: x => x.Usersid,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "TicketComments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticket_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ticketsid = table.Column<int>(type: "int", nullable: true),
                    Usersid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketComments", x => x.id);
                    table.ForeignKey(
                        name: "FK_TicketComments_Tickets_Ticketsid",
                        column: x => x.Ticketsid,
                        principalTable: "Tickets",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TicketComments_Users_Usersid",
                        column: x => x.Usersid,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ticket_Status_Logs_Ticketsid",
                table: "ticket_Status_Logs",
                column: "Ticketsid");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_Status_Logs_Usersid",
                table: "ticket_Status_Logs",
                column: "Usersid");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComments_Ticketsid",
                table: "TicketComments",
                column: "Ticketsid");

            migrationBuilder.CreateIndex(
                name: "IX_TicketComments_Usersid",
                table: "TicketComments",
                column: "Usersid");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Usersid",
                table: "Tickets",
                column: "Usersid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Rolesid",
                table: "Users",
                column: "Rolesid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticket_Status_Logs");

            migrationBuilder.DropTable(
                name: "TicketComments");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
