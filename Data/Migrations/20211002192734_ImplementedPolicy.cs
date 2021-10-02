using Microsoft.EntityFrameworkCore.Migrations;

namespace InuranceAssignmentAPD03.Data.Migrations
{
    public partial class ImplementedPolicy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Policies",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseCost",
                table: "Policies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PolicyDescription",
                table: "Policies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PolicyName",
                table: "Policies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VAT",
                table: "Policies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseCost",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "PolicyDescription",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "PolicyName",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "VAT",
                table: "Policies");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Policies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
