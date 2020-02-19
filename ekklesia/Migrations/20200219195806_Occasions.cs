using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ekklesia.Migrations
{
    public partial class Occasions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Occasions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    EventType = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Place = table.Column<string>(nullable: true),
                    BaptizerId = table.Column<int>(nullable: true),
                    Convertions = table.Column<int>(nullable: true),
                    NumberOfPeople = table.Column<int>(nullable: true),
                    MainVerse = table.Column<string>(nullable: true),
                    CultType = table.Column<int>(nullable: true),
                    Internal = table.Column<bool>(nullable: true),
                    Topic = table.Column<string>(nullable: true),
                    ReunionType = table.Column<int>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    SpeakerId = table.Column<int>(nullable: true),
                    Theme = table.Column<string>(nullable: true),
                    Verse = table.Column<string>(nullable: true),
                    NumberOfBibles = table.Column<int>(nullable: true),
                    TeacherId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occasions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Occasions_Members_BaptizerId",
                        column: x => x.BaptizerId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Occasions_Members_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Occasions_Members_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OccasionMember",
                columns: table => new
                {
                    MemberId = table.Column<int>(nullable: false),
                    OccasionId = table.Column<int>(nullable: false),
                    BaptismId = table.Column<int>(nullable: true),
                    ReunionId = table.Column<int>(nullable: true),
                    SundaySchoolId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccasionMember", x => new { x.MemberId, x.OccasionId });
                    table.ForeignKey(
                        name: "FK_OccasionMember_Occasions_BaptismId",
                        column: x => x.BaptismId,
                        principalTable: "Occasions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OccasionMember_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OccasionMember_Occasions_OccasionId",
                        column: x => x.OccasionId,
                        principalTable: "Occasions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OccasionMember_Occasions_ReunionId",
                        column: x => x.ReunionId,
                        principalTable: "Occasions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OccasionMember_Occasions_SundaySchoolId",
                        column: x => x.SundaySchoolId,
                        principalTable: "Occasions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OccasionMember_BaptismId",
                table: "OccasionMember",
                column: "BaptismId");

            migrationBuilder.CreateIndex(
                name: "IX_OccasionMember_OccasionId",
                table: "OccasionMember",
                column: "OccasionId");

            migrationBuilder.CreateIndex(
                name: "IX_OccasionMember_ReunionId",
                table: "OccasionMember",
                column: "ReunionId");

            migrationBuilder.CreateIndex(
                name: "IX_OccasionMember_SundaySchoolId",
                table: "OccasionMember",
                column: "SundaySchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Occasions_BaptizerId",
                table: "Occasions",
                column: "BaptizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Occasions_SpeakerId",
                table: "Occasions",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_Occasions_TeacherId",
                table: "Occasions",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OccasionMember");

            migrationBuilder.DropTable(
                name: "Occasions");
        }
    }
}
