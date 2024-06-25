using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AthleteHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removingSubscribtionIdFromAthlete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscribtionId",
                table: "Athletes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubscribtionId",
                table: "Athletes",
                type: "int",
                nullable: true);
        }
    }
}
