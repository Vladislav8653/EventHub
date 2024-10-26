using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.Migrations
{
    /// <inheritdoc />
    public partial class EditedDateOfParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Participants",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("d7a9167a-36af-42b2-981e-16594052a594"), "Exhibition" },
                    { new Guid("e5a6bdc2-ef02-4e2c-8268-326fe56faaec"), "Metal Concert" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "CategoryId", "DateTime", "Description", "Image", "MaxQuantityParticipant", "Name", "Place" },
                values: new object[,]
                {
                    { new Guid("3775f336-06be-4ca4-9b68-29015e9c3399"), null, new DateTimeOffset(new DateTime(2024, 7, 5, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "The coolest apples in the world are here", new byte[0], 100L, "Exhibition of apples", "Minsk, Komarovsky rinok" },
                    { new Guid("587f34f9-0d92-4a50-bd89-24395311e310"), null, new DateTimeOffset(new DateTime(2024, 7, 5, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "M72 Seasons World Tour", new byte[0], 100000L, "Metallica Concert", "Warsaw, PGE Narodowy" }
                });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "ParticipantId", "DateOfBirth", "Email", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("c256accd-7c23-4794-9730-f405cb01c6f7"), new DateOnly(2005, 5, 14), "arefin.vlad@gmail.com", "Vladislav", "Arefin" },
                    { new Guid("e77618b6-283d-4346-8757-f293b3374639"), new DateOnly(2006, 5, 26), "egor.pomidor@gmail.com", "Egor", "Shcherbin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("d7a9167a-36af-42b2-981e-16594052a594"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("e5a6bdc2-ef02-4e2c-8268-326fe56faaec"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("3775f336-06be-4ca4-9b68-29015e9c3399"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("587f34f9-0d92-4a50-bd89-24395311e310"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: new Guid("c256accd-7c23-4794-9730-f405cb01c6f7"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: new Guid("e77618b6-283d-4346-8757-f293b3374639"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Participants",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

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
    }
}
