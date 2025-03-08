using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseChatGPT.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoColunaTeste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColunaTeste",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColunaTeste",
                table: "AspNetUsers");
        }
    }
}
