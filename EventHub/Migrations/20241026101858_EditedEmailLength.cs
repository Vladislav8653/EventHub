using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHub.Migrations
{
    /// <inheritdoc />
    public partial class EditedEmailLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Participants",
                type: "character varying(254)",
                maxLength: 254,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("103c7a5b-8fe5-4154-93d5-4faa2fc2894c"), "Exhibition" },
                    { new Guid("62b4146c-1131-4863-88c4-0a4e5539b3f1"), "Metal Concert" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "CategoryId", "DateTime", "Description", "Image", "MaxQuantityParticipant", "Name", "Place" },
                values: new object[,]
                {
                    { new Guid("1b689dc8-9e99-4266-9d76-cd4d37b5691f"), null, new DateTimeOffset(new DateTime(2024, 7, 5, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "M72 Seasons World Tour", new byte[0], 100000L, "Metallica Concert", "Warsaw, PGE Narodowy" },
                    { new Guid("5bf861e3-581a-45c9-b23c-d750f096beb2"), null, new DateTimeOffset(new DateTime(2024, 7, 5, 20, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "The coolest apples in the world are here", new byte[0], 100L, "Exhibition of apples", "Minsk, Komarovsky rinok" }
                });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "ParticipantId", "DateOfBirth", "Email", "Name", "Surname" },
                values: new object[,]
                {
                    { new Guid("1a2817a0-2bf8-4dbc-8736-7ba0001e22f5"), new DateOnly(2005, 5, 14), "arefin.vlad@gmail.com", "Vladislav", "Arefin" },
                    { new Guid("3ffae6f2-c636-4382-bc4e-67df25c27bf3"), new DateOnly(2006, 5, 26), "egor.pomidor@gmail.com", "Egor", "Shcherbin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("103c7a5b-8fe5-4154-93d5-4faa2fc2894c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("62b4146c-1131-4863-88c4-0a4e5539b3f1"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("1b689dc8-9e99-4266-9d76-cd4d37b5691f"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: new Guid("5bf861e3-581a-45c9-b23c-d750f096beb2"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: new Guid("1a2817a0-2bf8-4dbc-8736-7ba0001e22f5"));

            migrationBuilder.DeleteData(
                table: "Participants",
                keyColumn: "ParticipantId",
                keyValue: new Guid("3ffae6f2-c636-4382-bc4e-67df25c27bf3"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Participants",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(254)",
                oldMaxLength: 254);

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
    }
}
