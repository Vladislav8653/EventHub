using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("da8d8f70-9cf1-48c4-9284-3a0a3bdd6339"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("e062d2bc-d931-48ad-8189-3249bcda33ec"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("9537d4ea-fd94-4dfd-988e-ff1106d31654"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("d3af39a8-9238-4940-a84e-16f88f08a83e"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: new Guid("1f37fbac-6818-485d-85c7-67937816ebcf"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: new Guid("8543909f-52db-4ecc-b51d-505ea27f4b43"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("87e8aa08-5b79-4ba4-ba4a-38ab999a9a9e"), "Exhibition" },
                    { new Guid("dbbca2f3-5a49-4608-8854-d63c0c208699"), "Metal Concert" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "CategoryId", "DateTime", "Description", "Image", "MaxQuantityParticipant", "Name", "Place" },
                values: new object[,]
                {
                    { new Guid("889fd939-2c77-488d-b72b-a15c77ee9a02"), null, new DateTimeOffset(new DateTime(2024, 7, 5, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "The coolest apples in the world are here", new byte[0], 100L, "Exhibition of apples", "Minsk, Komarovsky rinok" },
                    { new Guid("b6a125ac-47bd-46f3-ae2e-22d9793a3792"), null, new DateTimeOffset(new DateTime(2024, 7, 5, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "M72 Seasons World Tour", new byte[0], 100000L, "Metallica Concert", "Warsaw, PGE Narodowy" }
                });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "ParticipantId", "DateOfBirth", "Email", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("0358b3cd-ea86-45ae-9e0a-33505146dfca"), new DateTime(2005, 5, 14, 0, 0, 0, 0, DateTimeKind.Utc), "arefin.vlad@gmail.com", "Vladislav", "Arefin" },
                    { new Guid("8082739a-fb03-4589-adab-e80428923ddd"), new DateTime(2006, 5, 25, 20, 0, 0, 0, DateTimeKind.Utc), "egor.pomidor@gmail.com", "Egor", "Shcherbin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("87e8aa08-5b79-4ba4-ba4a-38ab999a9a9e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("dbbca2f3-5a49-4608-8854-d63c0c208699"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("889fd939-2c77-488d-b72b-a15c77ee9a02"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("b6a125ac-47bd-46f3-ae2e-22d9793a3792"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: new Guid("0358b3cd-ea86-45ae-9e0a-33505146dfca"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: new Guid("8082739a-fb03-4589-adab-e80428923ddd"));

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
        }
    }
}
