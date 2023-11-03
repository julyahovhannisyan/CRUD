using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class migr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DataAccessLayer",
                table: "DataAccessLayer");

            migrationBuilder.RenameTable(
                name: "DataAccessLayer",
                newName: "Article");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Article",
                table: "Article",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Article",
                table: "Article");

            migrationBuilder.RenameTable(
                name: "Article",
                newName: "DataAccessLayer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataAccessLayer",
                table: "DataAccessLayer",
                column: "Id");
        }
    }
}
