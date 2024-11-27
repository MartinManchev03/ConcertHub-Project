using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTicketEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "Tickets");

            migrationBuilder.CreateTable(
                name: "UsersTickets",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTickets", x => new { x.UserId, x.TicketId });
                    table.ForeignKey(
                        name: "FK_UsersTickets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersTickets_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("120bc0d4-a181-4327-b3b2-7631ff2534d0"), "Pop" },
                    { new Guid("16847db2-8194-4d1d-bfd1-9865ac531bf9"), "Country" },
                    { new Guid("5a3729f3-f84e-452c-91fe-cf95b457c9bb"), "Rock" },
                    { new Guid("618d7f04-d7f5-4662-9b58-3c52c0bf3874"), "Hip-Hop" },
                    { new Guid("631a8b03-ace4-485a-9cdc-9cbe4b772893"), "Jazz" },
                    { new Guid("805e8b5c-f820-484c-a561-37ac08d8310c"), "Folk" },
                    { new Guid("9396b6cd-1f4f-4cdd-a173-dc3854bf1e58"), "Latin" },
                    { new Guid("c4d4484f-ba09-4eff-be2f-fb56ed8cd2c5"), "Classical" }
                });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("56b9dc65-af5e-4fdd-aadf-d6bdae00b88c"), "Free", 0.0 },
                    { new Guid("7b673637-21e0-4c9c-94be-8d70746b03d5"), "Regular", 50.0 },
                    { new Guid("7ed7c2c2-7363-4fc7-9936-a2b968a9cb7a"), "General", 30.0 },
                    { new Guid("cbd52d60-a0cf-48e8-9f13-edfed2855197"), "VIP", 100.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersTickets_TicketId",
                table: "UsersTickets",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersTickets");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("120bc0d4-a181-4327-b3b2-7631ff2534d0"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("16847db2-8194-4d1d-bfd1-9865ac531bf9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5a3729f3-f84e-452c-91fe-cf95b457c9bb"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("618d7f04-d7f5-4662-9b58-3c52c0bf3874"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("631a8b03-ace4-485a-9cdc-9cbe4b772893"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("805e8b5c-f820-484c-a561-37ac08d8310c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9396b6cd-1f4f-4cdd-a173-dc3854bf1e58"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c4d4484f-ba09-4eff-be2f-fb56ed8cd2c5"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("56b9dc65-af5e-4fdd-aadf-d6bdae00b88c"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("7b673637-21e0-4c9c-94be-8d70746b03d5"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("7ed7c2c2-7363-4fc7-9936-a2b968a9cb7a"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("cbd52d60-a0cf-48e8-9f13-edfed2855197"));

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
    }
}
