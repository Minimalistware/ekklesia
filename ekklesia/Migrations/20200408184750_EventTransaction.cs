using Microsoft.EntityFrameworkCore.Migrations;

namespace ekklesia.Migrations
{
    public partial class EventTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Transactions",
                newName: "OccasionId");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Transactions",
                newName: "Discriminator");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Invoice",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RevenueType",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_OccasionId",
                table: "Transactions",
                column: "OccasionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Occasions_OccasionId",
                table: "Transactions",
                column: "OccasionId",
                principalTable: "Occasions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Occasions_OccasionId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_OccasionId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Invoice",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "RevenueType",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "OccasionId",
                table: "Transactions",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Transactions",
                newName: "Category");
        }
    }
}
