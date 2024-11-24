using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixRegular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0701aa25-ced0-4f39-96e9-dcb263ed33a1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0ce50b11-9293-4a02-a966-3c9c3fb61060"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0f084a25-130f-4683-bc49-11fc8ba9cfbf"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0fa3cd0c-f5ab-4bcb-9272-4d123f79ab0e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("36c6d9d2-7a40-457f-b58f-cfe892e58a5b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("80cf6cad-558d-4359-8e52-784fcc5c9159"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a7c7e251-8bc3-459a-ba47-92efcc5bea1b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d5a99af7-3af0-4eab-a241-e8efaf883abd"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("8deca3bf-3b83-4668-977c-8eec183192f4"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("b9a007f9-f3c9-4f71-8f6d-596b9c8da5e3"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("df9c4d6a-36d8-4ed6-9cff-f5050a6052b3"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("fef77266-b027-4c0d-9eac-b69b706cc1d5"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("18de19c0-adcb-4a25-87a8-93017c0bffd9"), "Latin" },
                    { new Guid("33ca1bb1-fd1c-4a42-8e74-d866c9021a61"), "Country" },
                    { new Guid("7b83b40c-0f5f-4f33-a710-a641c572e5e2"), "Pop" },
                    { new Guid("ada16077-d49c-45cd-9751-7a3fdc0d53f8"), "Folk" },
                    { new Guid("b3e7bb61-3be4-479c-aff9-deddfa2edcc6"), "Hip-Hop" },
                    { new Guid("c0b0dac1-6acd-40a7-9f68-6e96fee511e8"), "Classical" },
                    { new Guid("f08073b4-451b-469c-a453-f9a2ff21aee7"), "Rock" },
                    { new Guid("f0a501ed-fca9-45d4-9d04-e95995a91ce9"), "Jazz" }
                });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("0903b641-0f08-486e-9833-69fc87042e49"), "Regular", 50.0 },
                    { new Guid("7e1e72c6-f978-4304-9517-02a0338ed0c4"), "Free", 0.0 },
                    { new Guid("a6ed8b78-b28d-4c3a-a4ac-913c5e39d83a"), "VIP", 100.0 },
                    { new Guid("d2ab4274-4825-4690-94f0-77258929ffce"), "General", 30.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("18de19c0-adcb-4a25-87a8-93017c0bffd9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("33ca1bb1-fd1c-4a42-8e74-d866c9021a61"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7b83b40c-0f5f-4f33-a710-a641c572e5e2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ada16077-d49c-45cd-9751-7a3fdc0d53f8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b3e7bb61-3be4-479c-aff9-deddfa2edcc6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c0b0dac1-6acd-40a7-9f68-6e96fee511e8"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f08073b4-451b-469c-a453-f9a2ff21aee7"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f0a501ed-fca9-45d4-9d04-e95995a91ce9"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("0903b641-0f08-486e-9833-69fc87042e49"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("7e1e72c6-f978-4304-9517-02a0338ed0c4"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("a6ed8b78-b28d-4c3a-a4ac-913c5e39d83a"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("d2ab4274-4825-4690-94f0-77258929ffce"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0701aa25-ced0-4f39-96e9-dcb263ed33a1"), "Pop" },
                    { new Guid("0ce50b11-9293-4a02-a966-3c9c3fb61060"), "Rock" },
                    { new Guid("0f084a25-130f-4683-bc49-11fc8ba9cfbf"), "Country" },
                    { new Guid("0fa3cd0c-f5ab-4bcb-9272-4d123f79ab0e"), "Classical" },
                    { new Guid("36c6d9d2-7a40-457f-b58f-cfe892e58a5b"), "Latin" },
                    { new Guid("80cf6cad-558d-4359-8e52-784fcc5c9159"), "Folk" },
                    { new Guid("a7c7e251-8bc3-459a-ba47-92efcc5bea1b"), "Jazz" },
                    { new Guid("d5a99af7-3af0-4eab-a241-e8efaf883abd"), "Hip-Hop" }
                });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("8deca3bf-3b83-4668-977c-8eec183192f4"), "VIP", 100.0 },
                    { new Guid("b9a007f9-f3c9-4f71-8f6d-596b9c8da5e3"), "Free", 0.0 },
                    { new Guid("df9c4d6a-36d8-4ed6-9cff-f5050a6052b3"), "General", 30.0 },
                    { new Guid("fef77266-b027-4c0d-9eac-b69b706cc1d5"), "Regural", 50.0 }
                });
        }
    }
}
