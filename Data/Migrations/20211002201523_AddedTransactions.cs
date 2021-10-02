using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InuranceAssignmentAPD03.Data.Migrations
{
    public partial class AddedTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    ProfileId = table.Column<string>(nullable: true),
                    AccountId = table.Column<string>(nullable: true),
                    ClaimId = table.Column<string>(nullable: true),
                    PolicyId = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    TimeSent = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
