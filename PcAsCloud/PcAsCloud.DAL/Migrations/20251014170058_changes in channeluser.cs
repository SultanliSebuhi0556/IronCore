using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PcAsCloud.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changesinchanneluser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelUsers_AspNetUsers_UserId",
                table: "ChannelUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ChannelUsers",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChannelUsers_UserId",
                table: "ChannelUsers",
                newName: "IX_ChannelUsers_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelUsers_AspNetUsers_AppUserId",
                table: "ChannelUsers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChannelUsers_AspNetUsers_AppUserId",
                table: "ChannelUsers");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "ChannelUsers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChannelUsers_AppUserId",
                table: "ChannelUsers",
                newName: "IX_ChannelUsers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChannelUsers_AspNetUsers_UserId",
                table: "ChannelUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
