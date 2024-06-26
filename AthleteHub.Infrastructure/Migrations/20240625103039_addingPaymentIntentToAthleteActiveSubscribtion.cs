using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AthleteHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingPaymentIntentToAthleteActiveSubscribtion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscribtionId",
                table: "Athletes");

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "AthleteActiveSubscribtions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "AthleteActiveSubscribtions");

            migrationBuilder.AddColumn<int>(
                name: "SubscribtionId",
                table: "Athletes",
                type: "int",
                nullable: true);
        }
    }
}
