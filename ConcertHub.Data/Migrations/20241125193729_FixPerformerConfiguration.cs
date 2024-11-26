using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixPerformerConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("4eb69b70-5565-4235-bd1e-688bb72b0c59"), "Rock" },
                    { new Guid("58527f65-d974-4cf2-bad1-f93eaafe422f"), "Classical" },
                    { new Guid("6d5f7aca-bc96-40f0-bf4c-38ab188d9deb"), "Pop" },
                    { new Guid("6ef30f22-6012-4742-9f62-f3c5fcad07f1"), "Hip-Hop" },
                    { new Guid("bda9bde9-4a95-4c54-80ee-cd260d761d19"), "Country" },
                    { new Guid("db608dd3-8525-4792-90fc-fe4410cb575b"), "Folk" },
                    { new Guid("e78f1fa8-998a-42f3-97d0-f3c3713a5456"), "Latin" },
                    { new Guid("ed8716db-3c4b-42ef-b64a-d717fa3e63f9"), "Jazz" }
                });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("077e6f62-a045-4cfa-ba3f-97d108a78a31"), "General", 30.0 },
                    { new Guid("1d3bad13-95b7-4068-8db2-c92de464a9b0"), "VIP", 100.0 },
                    { new Guid("4bf9fc07-021f-4a06-abff-b4383dfebf25"), "Free", 0.0 },
                    { new Guid("6f40a196-cb3e-46dd-b443-53296696d793"), "Regular", 50.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4eb69b70-5565-4235-bd1e-688bb72b0c59"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("58527f65-d974-4cf2-bad1-f93eaafe422f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6d5f7aca-bc96-40f0-bf4c-38ab188d9deb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6ef30f22-6012-4742-9f62-f3c5fcad07f1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bda9bde9-4a95-4c54-80ee-cd260d761d19"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("db608dd3-8525-4792-90fc-fe4410cb575b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e78f1fa8-998a-42f3-97d0-f3c3713a5456"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ed8716db-3c4b-42ef-b64a-d717fa3e63f9"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("077e6f62-a045-4cfa-ba3f-97d108a78a31"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("1d3bad13-95b7-4068-8db2-c92de464a9b0"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("4bf9fc07-021f-4a06-abff-b4383dfebf25"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("6f40a196-cb3e-46dd-b443-53296696d793"));

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
    }
}
