using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteOnConcert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("0c88eaba-4ebb-42e1-8c85-cc5896b9c521"), "Pop" },
                    { new Guid("45545ffe-db1b-49c9-a622-e08e0e4ca6be"), "Rock" },
                    { new Guid("4973be86-8eb5-4f07-9487-13697b62777c"), "Jazz" },
                    { new Guid("8dc618b0-18c9-4a78-8316-0874c85bc028"), "Latin" },
                    { new Guid("b3dd2d2e-070c-4c47-900d-edd620c3965f"), "Classical" },
                    { new Guid("bc538897-c50f-494b-855a-458adc56ef2f"), "Folk" },
                    { new Guid("bd0d438f-5db3-4726-bcf0-af524bc2d9b1"), "Hip-Hop" },
                    { new Guid("ddde4c1e-03ce-4497-bfab-9cdb68332d4e"), "Country" }
                });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("53f46ac1-93b2-4ceb-8efc-ea905c876e69"), "Regular", 50.0 },
                    { new Guid("8f804e22-2c34-4b30-9431-52c1acb63fb5"), "VIP", 100.0 },
                    { new Guid("c893f3d0-61f2-47d6-a654-810f37ea8f64"), "Free", 0.0 },
                    { new Guid("ec7c01fe-3ef6-4863-9273-d77414b7cac8"), "General", 30.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0c88eaba-4ebb-42e1-8c85-cc5896b9c521"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("45545ffe-db1b-49c9-a622-e08e0e4ca6be"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4973be86-8eb5-4f07-9487-13697b62777c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8dc618b0-18c9-4a78-8316-0874c85bc028"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("b3dd2d2e-070c-4c47-900d-edd620c3965f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bc538897-c50f-494b-855a-458adc56ef2f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bd0d438f-5db3-4726-bcf0-af524bc2d9b1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ddde4c1e-03ce-4497-bfab-9cdb68332d4e"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("53f46ac1-93b2-4ceb-8efc-ea905c876e69"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("8f804e22-2c34-4b30-9431-52c1acb63fb5"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("c893f3d0-61f2-47d6-a654-810f37ea8f64"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("ec7c01fe-3ef6-4863-9273-d77414b7cac8"));

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
    }
}
