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

            migrationBuilder.CreateTable(
                name: "UsersTickets",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    { new Guid("07d1fe14-fec7-410f-994a-a48092477ddf"), "Hip-Hop" },
                    { new Guid("1cafbd83-bb85-45ab-b7d2-c2afcbeb8662"), "Rock" },
                    { new Guid("7b061d30-94a8-4611-b0c2-f956adedc8a9"), "Country" },
                    { new Guid("acfab590-c846-4074-ab6f-10b3620dcd3e"), "Classical" },
                    { new Guid("bb46cab6-a974-4737-a0d7-cb02b2354ece"), "Pop" },
                    { new Guid("cae2e364-5198-4da7-afb6-ce850b83f041"), "Jazz" },
                    { new Guid("e54b553d-fd01-489d-ae4d-76eb8ba72bf6"), "Latin" },
                    { new Guid("e6010e2d-31c0-4862-8735-eda8707baa30"), "Folk" }
                });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("011b61a8-d989-429d-a920-311e5fe29016"), "VIP", 100.0 },
                    { new Guid("08274da0-756c-4586-8cf6-68a24d87bfca"), "Regular", 50.0 },
                    { new Guid("3ef5087c-7734-4b35-bcb1-60a25a7a988c"), "Free", 0.0 },
                    { new Guid("515c5933-f3b1-4345-8e7b-7a10d2139022"), "General", 30.0 }
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
                keyValue: new Guid("07d1fe14-fec7-410f-994a-a48092477ddf"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1cafbd83-bb85-45ab-b7d2-c2afcbeb8662"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7b061d30-94a8-4611-b0c2-f956adedc8a9"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("acfab590-c846-4074-ab6f-10b3620dcd3e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bb46cab6-a974-4737-a0d7-cb02b2354ece"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cae2e364-5198-4da7-afb6-ce850b83f041"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e54b553d-fd01-489d-ae4d-76eb8ba72bf6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e6010e2d-31c0-4862-8735-eda8707baa30"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("011b61a8-d989-429d-a920-311e5fe29016"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("08274da0-756c-4586-8cf6-68a24d87bfca"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("3ef5087c-7734-4b35-bcb1-60a25a7a988c"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("515c5933-f3b1-4345-8e7b-7a10d2139022"));

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
