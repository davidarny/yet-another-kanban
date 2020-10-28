using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class user_uniq_keys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_user_account_email",
                table: "user_account",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_account_login",
                table: "user_account",
                column: "login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_account_email",
                table: "user_account");

            migrationBuilder.DropIndex(
                name: "IX_user_account_login",
                table: "user_account");
        }
    }
}
