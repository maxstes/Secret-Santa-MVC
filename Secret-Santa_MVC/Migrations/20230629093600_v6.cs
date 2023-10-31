using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Secret_Santa_MVC.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGuests_RoomCreated_IdRoom1",
                table: "RoomGuests");

            migrationBuilder.RenameColumn(
                name: "IdRoom1",
                table: "RoomGuests",
                newName: "IdRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomGuests_IdRoom1",
                table: "RoomGuests",
                newName: "IX_RoomGuests_IdRoomId");

            migrationBuilder.RenameColumn(
                name: "IdRoom",
                table: "RoomCreated",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "NameGuest",
                table: "RoomGuests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "InvateLink",
                table: "RoomGuests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InviteLink",
                table: "RoomCreated",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGuests_RoomCreated_IdRoomId",
                table: "RoomGuests",
                column: "IdRoomId",
                principalTable: "RoomCreated",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomGuests_RoomCreated_IdRoomId",
                table: "RoomGuests");

            migrationBuilder.DropColumn(
                name: "InvateLink",
                table: "RoomGuests");

            migrationBuilder.RenameColumn(
                name: "IdRoomId",
                table: "RoomGuests",
                newName: "IdRoom1");

            migrationBuilder.RenameIndex(
                name: "IX_RoomGuests_IdRoomId",
                table: "RoomGuests",
                newName: "IX_RoomGuests_IdRoom1");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RoomCreated",
                newName: "IdRoom");

            migrationBuilder.AlterColumn<string>(
                name: "NameGuest",
                table: "RoomGuests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InviteLink",
                table: "RoomCreated",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomGuests_RoomCreated_IdRoom1",
                table: "RoomGuests",
                column: "IdRoom1",
                principalTable: "RoomCreated",
                principalColumn: "IdRoom");
        }
    }
}
