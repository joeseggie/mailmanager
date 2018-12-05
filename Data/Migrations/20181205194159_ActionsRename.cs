using Microsoft.EntityFrameworkCore.Migrations;

namespace MailManager.Data.Migrations
{
    public partial class ActionsRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_ActionStatuses_ActionStatusId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Mails_MailId",
                table: "Actions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actions",
                table: "Actions");

            migrationBuilder.RenameTable(
                name: "Actions",
                newName: "CorrespondanceActions");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_MailId",
                table: "CorrespondanceActions",
                newName: "IX_CorrespondanceActions_MailId");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_ActionStatusId",
                table: "CorrespondanceActions",
                newName: "IX_CorrespondanceActions_ActionStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CorrespondanceActions",
                table: "CorrespondanceActions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CorrespondanceActions_ActionStatuses_ActionStatusId",
                table: "CorrespondanceActions",
                column: "ActionStatusId",
                principalTable: "ActionStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorrespondanceActions_Mails_MailId",
                table: "CorrespondanceActions",
                column: "MailId",
                principalTable: "Mails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorrespondanceActions_ActionStatuses_ActionStatusId",
                table: "CorrespondanceActions");

            migrationBuilder.DropForeignKey(
                name: "FK_CorrespondanceActions_Mails_MailId",
                table: "CorrespondanceActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CorrespondanceActions",
                table: "CorrespondanceActions");

            migrationBuilder.RenameTable(
                name: "CorrespondanceActions",
                newName: "Actions");

            migrationBuilder.RenameIndex(
                name: "IX_CorrespondanceActions_MailId",
                table: "Actions",
                newName: "IX_Actions_MailId");

            migrationBuilder.RenameIndex(
                name: "IX_CorrespondanceActions_ActionStatusId",
                table: "Actions",
                newName: "IX_Actions_ActionStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actions",
                table: "Actions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_ActionStatuses_ActionStatusId",
                table: "Actions",
                column: "ActionStatusId",
                principalTable: "ActionStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Mails_MailId",
                table: "Actions",
                column: "MailId",
                principalTable: "Mails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
