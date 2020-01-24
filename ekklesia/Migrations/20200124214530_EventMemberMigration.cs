using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ekklesia.Migrations
{
    public partial class EventMemberMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    EventType = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    NumberOfPeople = table.Column<int>(nullable: true),
                    MainVerse = table.Column<string>(nullable: true),
                    SpeakerId = table.Column<int>(nullable: true),
                    Topic = table.Column<string>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    SundaySchool_SpeakerId = table.Column<int>(nullable: true),
                    Theme = table.Column<string>(nullable: true),
                    Verse = table.Column<string>(nullable: true),
                    NumberOfBibles = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Members_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Members_SundaySchool_SpeakerId",
                        column: x => x.SundaySchool_SpeakerId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventMember",
                columns: table => new
                {
                    MemberId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    ReunionId = table.Column<int>(nullable: true),
                    SundaySchoolId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventMember", x => new { x.MemberId, x.EventId });
                    table.ForeignKey(
                        name: "FK_EventMember_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventMember_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventMember_Events_ReunionId",
                        column: x => x.ReunionId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventMember_Events_SundaySchoolId",
                        column: x => x.SundaySchoolId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventMember_EventId",
                table: "EventMember",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventMember_ReunionId",
                table: "EventMember",
                column: "ReunionId");

            migrationBuilder.CreateIndex(
                name: "IX_EventMember_SundaySchoolId",
                table: "EventMember",
                column: "SundaySchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_SpeakerId",
                table: "Events",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_SundaySchool_SpeakerId",
                table: "Events",
                column: "SundaySchool_SpeakerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventMember");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
