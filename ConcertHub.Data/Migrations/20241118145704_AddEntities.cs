using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcertHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "IdentityUserRole",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "IdentityUserLogin",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEndDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Concerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcertName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Concerts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Concerts_Categories_CategoryId1",
                        column: x => x.CategoryId1,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Concerts_IdentityUser_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim_IdentityUser_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Performers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Performers_IdentityUser_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedBacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcertId = table.Column<int>(type: "int", nullable: false),
                    PostedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ConcertId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedBacks_Concerts_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedBacks_Concerts_ConcertId1",
                        column: x => x.ConcertId1,
                        principalTable: "Concerts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeedBacks_IdentityUser_PostedById",
                        column: x => x.PostedById,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConcertId = table.Column<int>(type: "int", nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    ConcertId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Concerts_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Concerts_ConcertId1",
                        column: x => x.ConcertId1,
                        principalTable: "Concerts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_IdentityUser_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConcertsPerformers",
                columns: table => new
                {
                    ConcertId = table.Column<int>(type: "int", nullable: false),
                    PerformerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcertsPerformers", x => new { x.ConcertId, x.PerformerId });
                    table.ForeignKey(
                        name: "FK_ConcertsPerformers_Concerts_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConcertsPerformers_Performers_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "Performers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserRole_IdentityUserId",
                table: "IdentityUserRole",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserLogin_IdentityUserId",
                table: "IdentityUserLogin",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Concerts_CategoryId",
                table: "Concerts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Concerts_CategoryId1",
                table: "Concerts",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Concerts_OrganizerId",
                table: "Concerts",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_ConcertsPerformers_PerformerId",
                table: "ConcertsPerformers",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBacks_ConcertId",
                table: "FeedBacks",
                column: "ConcertId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBacks_ConcertId1",
                table: "FeedBacks",
                column: "ConcertId1");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBacks_PostedById",
                table: "FeedBacks",
                column: "PostedById");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserClaim_IdentityUserId",
                table: "IdentityUserClaim",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Performers_CreatorId",
                table: "Performers",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_BuyerId",
                table: "Tickets",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ConcertId",
                table: "Tickets",
                column: "ConcertId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ConcertId1",
                table: "Tickets",
                column: "ConcertId1");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin_IdentityUser_IdentityUserId",
                table: "IdentityUserLogin",
                column: "IdentityUserId",
                principalTable: "IdentityUser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole_IdentityUser_IdentityUserId",
                table: "IdentityUserRole",
                column: "IdentityUserId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserLogin_IdentityUser_IdentityUserId",
                table: "IdentityUserLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserRole_IdentityUser_IdentityUserId",
                table: "IdentityUserRole");

            migrationBuilder.DropTable(
                name: "ConcertsPerformers");

            migrationBuilder.DropTable(
                name: "FeedBacks");

            migrationBuilder.DropTable(
                name: "IdentityUserClaim");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Performers");

            migrationBuilder.DropTable(
                name: "Concerts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropIndex(
                name: "IX_IdentityUserRole_IdentityUserId",
                table: "IdentityUserRole");

            migrationBuilder.DropIndex(
                name: "IX_IdentityUserLogin_IdentityUserId",
                table: "IdentityUserLogin");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "IdentityUserRole");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "IdentityUserLogin");
        }
    }
}
