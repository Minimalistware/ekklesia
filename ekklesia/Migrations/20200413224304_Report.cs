using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ekklesia.Migrations
{
    public partial class Report : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PreacherId = table.Column<int>(nullable: false),
                    CoordinatorId = table.Column<int>(nullable: false),
                    Reunions = table.Column<int>(nullable: false),
                    Convertions = table.Column<int>(nullable: false),
                    PreviousMonth = table.Column<decimal>(nullable: false),
                    Income = table.Column<decimal>(nullable: false),
                    Expense = table.Column<decimal>(nullable: false),
                    Tenth = table.Column<decimal>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Bibles = table.Column<int>(nullable: true),
                    ReunionWithTeachers = table.Column<int>(nullable: true),
                    Visitants = table.Column<int>(nullable: true),
                    PeoplePresent = table.Column<int>(nullable: true),
                    PedagogicalBody = table.Column<int>(nullable: true),
                    CoordenationMeatings = table.Column<int>(nullable: true),
                    NumberOfVisits = table.Column<int>(nullable: true),
                    NumberOfEvangelisms = table.Column<int>(nullable: true),
                    NumberOfBoardMembers = table.Column<int>(nullable: true),
                    ExternalCults = table.Column<int>(nullable: true),
                    CellsNumber = table.Column<int>(nullable: true),
                    Baptized = table.Column<int>(nullable: true),
                    MeetingsWithTheCoordination = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Members_CoordinatorId",
                        column: x => x.CoordinatorId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reports_Members_PreacherId",
                        column: x => x.PreacherId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CoordinatorId",
                table: "Reports",
                column: "CoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_PreacherId",
                table: "Reports",
                column: "PreacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
