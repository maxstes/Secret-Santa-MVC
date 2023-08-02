using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Secret_Santa_MVC.Migrations
{
    /// <inheritdoc />
    public partial class v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "RoomCreated",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "RoomCreated");
        }
    }
}
