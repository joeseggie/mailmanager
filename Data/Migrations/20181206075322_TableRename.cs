using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailManager.Data.Migrations
{
    public partial class TableRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrespondanceActions");

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

            migrationBuilder.CreateIndex(
                name: "IX_ActionPoints_ActionStatusId",
                table: "ActionPoints",
                column: "ActionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionPoints_MailId",
                table: "ActionPoints",
                column: "MailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionPoints");

            migrationBuilder.CreateTable(
                name: "CorrespondanceActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ActionStatusId = table.Column<Guid>(nullable: true),
                    Details = table.Column<string>(nullable: false),
                    MailId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrespondanceActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CorrespondanceActions_ActionStatuses_ActionStatusId",
                        column: x => x.ActionStatusId,
                        principalTable: "ActionStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CorrespondanceActions_Mails_MailId",
                        column: x => x.MailId,
                        principalTable: "Mails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorrespondanceActions_ActionStatusId",
                table: "CorrespondanceActions",
                column: "ActionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrespondanceActions_MailId",
                table: "CorrespondanceActions",
                column: "MailId");
        }
    }
}
