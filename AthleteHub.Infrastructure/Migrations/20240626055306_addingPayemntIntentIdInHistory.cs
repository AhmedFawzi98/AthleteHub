using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AthleteHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingPayemntIntentIdInHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "AthletesSubscribtionsHistory",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "AthletesSubscribtionsHistory");
        }
    }
}
