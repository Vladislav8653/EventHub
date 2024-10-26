using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.Migrations
{
    /// <inheritdoc />
    public partial class EditedDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ParticipantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Surname = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Email = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.ParticipantId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    DateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Place = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    MaxQuantityParticipant = table.Column<long>(type: "bigint", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "EventsParticipants",
                columns: table => new
                {
                    EventParticipantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    RegistrationTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsParticipants", x => x.EventParticipantId);
                    table.ForeignKey(
                        name: "FK_EventsParticipants_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventsParticipants_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "ParticipantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("da8d8f70-9cf1-48c4-9284-3a0a3bdd6339"), "Exhibition" },
                    { new Guid("e062d2bc-d931-48ad-8189-3249bcda33ec"), "Metal Concert" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "CategoryId", "DateTime", "Description", "Image", "MaxQuantityParticipant", "Name", "Place" },
                values: new object[,]
                {
                    { new Guid("9537d4ea-fd94-4dfd-988e-ff1106d31654"), null, new DateTimeOffset(new DateTime(2024, 7, 5, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "The coolest apples in the world are here", new byte[0], 100L, "Exhibition of apples", "Minsk, Komarovsky rinok" },
                    { new Guid("d3af39a8-9238-4940-a84e-16f88f08a83e"), null, new DateTimeOffset(new DateTime(2024, 7, 5, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "M72 Seasons World Tour", new byte[0], 100000L, "Metallica Concert", "Warsaw, PGE Narodowy" }
                });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "ParticipantId", "DateOfBirth", "Email", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("1f37fbac-6818-485d-85c7-67937816ebcf"), new DateTime(2006, 5, 26, 0, 0, 0, 0, DateTimeKind.Utc), "egor.pomidor@gmail.com", "Egor", "Shcherbin" },
                    { new Guid("8543909f-52db-4ecc-b51d-505ea27f4b43"), new DateTime(2005, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), "arefin.vlad@gmail.com", "Vladislav", "Arefin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsParticipants_EventId",
                table: "EventsParticipants",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventsParticipants_ParticipantId",
                table: "EventsParticipants",
                column: "ParticipantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventsParticipants");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
