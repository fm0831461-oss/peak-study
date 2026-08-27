using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeekStudy.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFocusSessionIslandItemRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FocusSessionId",
                table: "IslandItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IslandTemId",
                table: "FocusSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IslandItems_FocusSessionId",
                table: "IslandItems",
                column: "FocusSessionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IslandItems_FocusSessions_FocusSessionId",
                table: "IslandItems",
                column: "FocusSessionId",
                principalTable: "FocusSessions",
                principalColumn: "FocusSessionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IslandItems_FocusSessions_FocusSessionId",
                table: "IslandItems");

            migrationBuilder.DropIndex(
                name: "IX_IslandItems_FocusSessionId",
                table: "IslandItems");

            migrationBuilder.DropColumn(
                name: "FocusSessionId",
                table: "IslandItems");

            migrationBuilder.DropColumn(
                name: "IslandTemId",
                table: "FocusSessions");
        }
    }
}
