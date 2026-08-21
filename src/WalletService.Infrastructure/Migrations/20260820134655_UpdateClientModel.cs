using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WalletService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wallets_ClientId_Status",
                table: "Wallets");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_ClientId",
                table: "Wallets",
                column: "ClientId",
                unique: true,
                filter: "[Status] IN (1, 2, 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wallets_ClientId",
                table: "Wallets");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_ClientId_Status",
                table: "Wallets",
                columns: new[] { "ClientId", "Status" },
                unique: true,
                filter: "[Status] IN (1, 2, 3)");
        }
    }
}
