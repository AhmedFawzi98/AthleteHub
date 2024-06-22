using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AthleteHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addingRatingandFavouritesandBlockedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "AthletesCoaches");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "AthletesCoaches");

            migrationBuilder.AddColumn<bool>(
                name: "IsSuspended",
                table: "Coaches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentlySubscribed",
                table: "AthletesCoaches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AthletesFavouriteCoaches",
                columns: table => new
                {
                    AthleteId = table.Column<int>(type: "int", nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthletesFavouriteCoaches", x => new { x.AthleteId, x.CoachId });
                    table.ForeignKey(
                        name: "FK_AthletesFavouriteCoaches_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AthletesFavouriteCoaches_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoachesBlockedAthletees",
                columns: table => new
                {
                    AthleteId = table.Column<int>(type: "int", nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachesBlockedAthletees", x => new { x.AthleteId, x.CoachId });
                    table.ForeignKey(
                        name: "FK_CoachesBlockedAthletees_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoachesBlockedAthletees_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoachesRatings",
                columns: table => new
                {
                    AthleteId = table.Column<int>(type: "int", nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachesRatings", x => new { x.AthleteId, x.CoachId });
                    table.ForeignKey(
                        name: "FK_CoachesRatings_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoachesRatings_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AthletesFavouriteCoaches_CoachId",
                table: "AthletesFavouriteCoaches",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachesBlockedAthletees_CoachId",
                table: "CoachesBlockedAthletees",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachesRatings_CoachId",
                table: "CoachesRatings",
                column: "CoachId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AthletesFavouriteCoaches");

            migrationBuilder.DropTable(
                name: "CoachesBlockedAthletees");

            migrationBuilder.DropTable(
                name: "CoachesRatings");

            migrationBuilder.DropColumn(
                name: "IsSuspended",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "IsCurrentlySubscribed",
                table: "AthletesCoaches");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "AthletesCoaches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "AthletesCoaches",
                type: "int",
                nullable: true);
        }
    }
}
