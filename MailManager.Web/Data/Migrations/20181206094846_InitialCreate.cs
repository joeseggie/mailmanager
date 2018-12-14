using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailManager.Web.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReferenceNumber = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    From = table.Column<string>(nullable: false),
                    To = table.Column<string>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    Received = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActionPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Details = table.Column<string>(nullable: false),
                    MailId = table.Column<Guid>(nullable: true),
                    ActionStatusId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionPoints_ActionStatuses_ActionStatusId",
                        column: x => x.ActionStatusId,
                        principalTable: "ActionStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActionPoints_Mails_MailId",
                        column: x => x.MailId,
                        principalTable: "Mails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Correspondances",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MailId = table.Column<Guid>(nullable: false),
                    Logged = table.Column<DateTime>(nullable: false),
                    Office = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correspondances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Correspondances_Mails_MailId",
                        column: x => x.MailId,
                        principalTable: "Mails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionPoints_ActionStatusId",
                table: "ActionPoints",
                column: "ActionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionPoints_MailId",
                table: "ActionPoints",
                column: "MailId");

            migrationBuilder.CreateIndex(
                name: "IX_Correspondances_MailId",
                table: "Correspondances",
                column: "MailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionPoints");

            migrationBuilder.DropTable(
                name: "Correspondances");

            migrationBuilder.DropTable(
                name: "ActionStatuses");

            migrationBuilder.DropTable(
                name: "Mails");
        }
    }
}
