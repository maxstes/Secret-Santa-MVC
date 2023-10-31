using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Secret_Santa_MVC.Migrations
{
    /// <inheritdoc />
    public partial class v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "RoomCreatedRoomGuest",
                columns: table => new
                {
                    RoomCreatedsId = table.Column<int>(type: "int", nullable: false),
                    RoomGuestsIdGuest = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomCreatedRoomGuest", x => new { x.RoomCreatedsId, x.RoomGuestsIdGuest });
                    table.ForeignKey(
                        name: "FK_RoomCreatedRoomGuest_RoomCreated_RoomCreatedsId",
                        column: x => x.RoomCreatedsId,
                        principalTable: "RoomCreated",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomCreatedRoomGuest_RoomGuests_RoomGuestsIdGuest",
                        column: x => x.RoomGuestsIdGuest,
                        principalTable: "RoomGuests",
                        principalColumn: "IdGuest",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomCreatedRoomGuest_RoomGuestsIdGuest",
                table: "RoomCreatedRoomGuest",
                column: "RoomGuestsIdGuest");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomCreatedRoomGuest");

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
    }
}
