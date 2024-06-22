using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AthleteHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixingSubscribtionEndDateColumnToBeMapped : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "SubscribtionEndDate",
                table: "AthleteActiveSubscribtions",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscribtionEndDate",
                table: "AthleteActiveSubscribtions");
        }
    }
}
