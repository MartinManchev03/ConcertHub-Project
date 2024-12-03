using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixConcertPerformerConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConcertsPerformers_Performers_PerformerId",
                table: "ConcertsPerformers");

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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("15d1ef65-e1d7-40b0-8622-e0ce9b079ddd"), "Pop" },
                    { new Guid("29a9d443-807c-4d4e-9390-f9cfc1ecb8aa"), "Latin" },
                    { new Guid("438671d3-e069-4552-b395-a4f89b0ece56"), "Country" },
                    { new Guid("4e27409c-2a70-4fca-8c5f-94472c173308"), "Folk" },
                    { new Guid("7d6e45ab-9e62-4054-813f-fe6e0406d09c"), "Classical" },
                    { new Guid("a494f394-980b-44ed-b2f7-504547f513e6"), "Hip-Hop" },
                    { new Guid("cc16614f-6032-416e-86b5-78195027bb54"), "Jazz" },
                    { new Guid("f34ef59a-2d7a-49c2-aa11-a01c8d78cd95"), "Rock" }
                });

            migrationBuilder.InsertData(
                table: "TicketTypes",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("70d32940-8a0f-4602-9b0f-4afd61dc6dca"), "Regular", 50.0 },
                    { new Guid("74474671-67ad-4fa4-b36e-d966367f4a9f"), "General", 30.0 },
                    { new Guid("a038ad27-f7ea-4b3e-add5-878349ddbe26"), "VIP", 100.0 },
                    { new Guid("aede28bf-a356-416a-a5af-e626fbb7e523"), "Free", 0.0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ConcertsPerformers_Performers_PerformerId",
                table: "ConcertsPerformers",
                column: "PerformerId",
                principalTable: "Performers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConcertsPerformers_Performers_PerformerId",
                table: "ConcertsPerformers");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("15d1ef65-e1d7-40b0-8622-e0ce9b079ddd"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("29a9d443-807c-4d4e-9390-f9cfc1ecb8aa"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("438671d3-e069-4552-b395-a4f89b0ece56"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4e27409c-2a70-4fca-8c5f-94472c173308"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("7d6e45ab-9e62-4054-813f-fe6e0406d09c"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a494f394-980b-44ed-b2f7-504547f513e6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cc16614f-6032-416e-86b5-78195027bb54"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("f34ef59a-2d7a-49c2-aa11-a01c8d78cd95"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("70d32940-8a0f-4602-9b0f-4afd61dc6dca"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("74474671-67ad-4fa4-b36e-d966367f4a9f"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("a038ad27-f7ea-4b3e-add5-878349ddbe26"));

            migrationBuilder.DeleteData(
                table: "TicketTypes",
                keyColumn: "Id",
                keyValue: new Guid("aede28bf-a356-416a-a5af-e626fbb7e523"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_ConcertsPerformers_Performers_PerformerId",
                table: "ConcertsPerformers",
                column: "PerformerId",
                principalTable: "Performers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
