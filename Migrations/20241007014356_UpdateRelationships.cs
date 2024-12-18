using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_LibraryBranches_LibraryBranchId",
                table: "Books");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_LibraryBranches_LibraryBranchId",
                table: "Books",
                column: "LibraryBranchId",
                principalTable: "LibraryBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_LibraryBranches_LibraryBranchId",
                table: "Books");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_LibraryBranches_LibraryBranchId",
                table: "Books",
                column: "LibraryBranchId",
                principalTable: "LibraryBranches",
                principalColumn: "Id");
        }
    }
}
