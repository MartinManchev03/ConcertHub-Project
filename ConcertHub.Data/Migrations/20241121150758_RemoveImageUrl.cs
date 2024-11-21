using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1a3b58c2-f5d9-4399-86a2-a8909977835e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("230f8087-8dc0-4c9b-85eb-323b048fa588"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("37d2e5b1-df42-401d-a54f-afb219157016"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("45dd24c2-c48c-4e80-a8ba-c8a38af1971c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("92ce4113-a729-4919-8fd5-39f2b279d875"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("94948024-2c6d-4a51-8c26-6d5fa2c5fd07"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ca96ec59-f10b-4e0c-ae50-3f7c6692fd45"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e9af239b-1a5f-4d3e-bcaf-2c61f9ad430f"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("28886a23-4317-474b-8528-3945a43dd2ca"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("93b080c7-7d88-4c34-8855-b3af4274fad1"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("a71906ca-6161-41d5-b2f6-5f61f5c6629a"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("f4d09c69-c0d3-4e22-a851-7a7d65e6b0c6"));

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Concerts");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Concerts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1a3b58c2-f5d9-4399-86a2-a8909977835e"), "Hip-Hop" },
                    { new Guid("230f8087-8dc0-4c9b-85eb-323b048fa588"), "Jazz" },
                    { new Guid("37d2e5b1-df42-401d-a54f-afb219157016"), "Rock" },
                    { new Guid("45dd24c2-c48c-4e80-a8ba-c8a38af1971c"), "Pop" },
                    { new Guid("92ce4113-a729-4919-8fd5-39f2b279d875"), "Folk" },
                    { new Guid("94948024-2c6d-4a51-8c26-6d5fa2c5fd07"), "Latin" },
                    { new Guid("ca96ec59-f10b-4e0c-ae50-3f7c6692fd45"), "Classical" },
                    { new Guid("e9af239b-1a5f-4d3e-bcaf-2c61f9ad430f"), "Country" }
                });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("28886a23-4317-474b-8528-3945a43dd2ca"), "VIP", 100.0 },
                    { new Guid("93b080c7-7d88-4c34-8855-b3af4274fad1"), "Regural", 50.0 },
                    { new Guid("a71906ca-6161-41d5-b2f6-5f61f5c6629a"), "Free", 0.0 },
                    { new Guid("f4d09c69-c0d3-4e22-a851-7a7d65e6b0c6"), "General", 30.0 }
                });
        }
    }
}
