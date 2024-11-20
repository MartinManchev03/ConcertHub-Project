using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTicketTypeAndChangeTicketEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_BuyerId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_BuyerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketType",
                table: "Tickets");

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTypeId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TicketTypeId1",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketTypeId",
                table: "Tickets",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketTypeId1",
                table: "Tickets",
                column: "TicketTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketTypes_TicketTypeId",
                table: "Tickets",
                column: "TicketTypeId",
                principalTable: "TicketTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_TicketTypes_TicketTypeId1",
                table: "Tickets",
                column: "TicketTypeId1",
                principalTable: "TicketTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketTypes_TicketTypeId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_TicketTypes_TicketTypeId1",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "TicketTypes");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TicketTypeId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_TicketTypeId1",
                table: "Tickets");

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

            migrationBuilder.DropColumn(
                name: "TicketTypeId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketTypeId1",
                table: "Tickets");

            migrationBuilder.AddColumn<string>(
                name: "BuyerId",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TicketType",
                table: "Tickets",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_BuyerId",
                table: "Tickets",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_BuyerId",
                table: "Tickets",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
