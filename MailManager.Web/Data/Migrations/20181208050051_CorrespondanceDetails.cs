using Microsoft.EntityFrameworkCore.Migrations;

namespace MailManager.Web.Data.Migrations
{
    public partial class CorrespondanceDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Correspondances",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Correspondances");
        }
    }
}
