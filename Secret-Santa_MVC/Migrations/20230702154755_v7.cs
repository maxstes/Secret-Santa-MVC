using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Secret_Santa_MVC.Migrations
{
    /// <inheritdoc />
    public partial class v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGuests_RoomCreated_IdRoomId",
                table: "RoomGuests");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropIndex(
                name: "IX_RoomGuests_IdRoomId",
                table: "RoomGuests");

            migrationBuilder.DropColumn(
                name: "IdRoomId",
                table: "RoomGuests");

            migrationBuilder.DropColumn(
                name: "InvateLink",
                table: "RoomGuests");

            migrationBuilder.AlterColumn<string>(
                name: "Wish",
                table: "RoomGuests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomCreatedId",
                table: "RoomGuests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RoomGuests_RoomCreatedId",
                table: "RoomGuests",
                column: "RoomCreatedId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGuests_RoomCreated_RoomCreatedId",
                table: "RoomGuests",
                column: "RoomCreatedId",
                principalTable: "RoomCreated",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGuests_RoomCreated_RoomCreatedId",
                table: "RoomGuests");

            migrationBuilder.DropIndex(
                name: "IX_RoomGuests_RoomCreatedId",
                table: "RoomGuests");

            migrationBuilder.DropColumn(
                name: "RoomCreatedId",
                table: "RoomGuests");

            migrationBuilder.AlterColumn<string>(
                name: "Wish",
                table: "RoomGuests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "IdRoomId",
                table: "RoomGuests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InvateLink",
                table: "RoomGuests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    IdRoom = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Budget = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkRoom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameRoom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.IdRoom);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomIdRoom = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CassettesCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FragmentCount = table.Column<int>(type: "int", nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RoomId = table.Column<long>(type: "bigint", nullable: true),
                    SampleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Applications_Room_RoomIdRoom",
                        column: x => x.RoomIdRoom,
                        principalTable: "Room",
                        principalColumn: "IdRoom");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomGuests_IdRoomId",
                table: "RoomGuests",
                column: "IdRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_RoomIdRoom",
                table: "Applications",
                column: "RoomIdRoom");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGuests_RoomCreated_IdRoomId",
                table: "RoomGuests",
                column: "IdRoomId",
                principalTable: "RoomCreated",
                principalColumn: "Id");
        }
    }
}
