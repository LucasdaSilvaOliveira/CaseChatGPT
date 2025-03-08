using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseChatGPT.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoColunaTeste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColunaTeste",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColunaTeste",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
