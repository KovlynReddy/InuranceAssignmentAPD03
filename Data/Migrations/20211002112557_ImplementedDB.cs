using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InuranceAssignmentAPD03.Data.Migrations
{
    public partial class ImplementedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cancer",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Profiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DangerousWorkingEnviroment",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Diabeties",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DrinkAlcohol",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FamilyId",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FamilyMembers",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HeridataryDeseases",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Kids",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OnCronicDrugs",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OnPercesciptiveDrugs",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sinus",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SmokeCigerrets",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TerminalIllnesses",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TravelForWork",
                table: "Profiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Profiles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AverageAge",
                table: "Policies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Constant",
                table: "Policies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyPremium",
                table: "Policies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Policies",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Valid",
                table: "Policies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "MyUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyId",
                table: "MyUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FamilyMembers",
                table: "MyUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfileId",
                table: "MyUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeCreated",
                table: "MyUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Features",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateValid",
                table: "Features",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Features",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Features",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Claims",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "Claims",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Claims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Claims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PolicyId",
                table: "Claims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileId",
                table: "Claims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Claims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Claims",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Balance",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Cancer",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "DangerousWorkingEnviroment",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Diabeties",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "DrinkAlcohol",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "FamilyMembers",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "HeridataryDeseases",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Kids",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "OnCronicDrugs",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "OnPercesciptiveDrugs",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Sinus",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "SmokeCigerrets",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "TerminalIllnesses",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "TravelForWork",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "AverageAge",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "Constant",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "MonthlyPremium",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "Valid",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "MyUsers");

            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "MyUsers");

            migrationBuilder.DropColumn(
                name: "FamilyMembers",
                table: "MyUsers");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "MyUsers");

            migrationBuilder.DropColumn(
                name: "TimeCreated",
                table: "MyUsers");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "DateValid",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "PolicyId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Accounts");
        }
    }
}
