using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Secret_Santa_MVC.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Rooms_RoomIdRoom",
                table: "Applications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "IdRoom");

            migrationBuilder.CreateTable(
                name: "RoomCreated",
                columns: table => new
                {
                    IdRoom = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerRoomId = table.Column<int>(type: "int", nullable: false),
                    NameRoom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InviteLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxPriceGift = table.Column<float>(type: "real", nullable: false),
                    MinPriceGift = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomCreated", x => x.IdRoom);
                });

            migrationBuilder.CreateTable(
                name: "RoomGuests",
                columns: table => new
                {
                    IdGuest = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRoom1 = table.Column<int>(type: "int", nullable: true),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    Wish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameGuest = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomGuests", x => x.IdGuest);
                    table.ForeignKey(
                        name: "FK_RoomGuests_RoomCreated_IdRoom1",
                        column: x => x.IdRoom1,
                        principalTable: "RoomCreated",
                        principalColumn: "IdRoom");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomGuests_IdRoom1",
                table: "RoomGuests",
                column: "IdRoom1");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Room_RoomIdRoom",
                table: "Applications",
                column: "RoomIdRoom",
                principalTable: "Room",
                principalColumn: "IdRoom");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Room_RoomIdRoom",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "RoomGuests");

            migrationBuilder.DropTable(
                name: "RoomCreated");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "IdRoom");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Rooms_RoomIdRoom",
                table: "Applications",
                column: "RoomIdRoom",
                principalTable: "Rooms",
                principalColumn: "IdRoom");
        }
    }
}
