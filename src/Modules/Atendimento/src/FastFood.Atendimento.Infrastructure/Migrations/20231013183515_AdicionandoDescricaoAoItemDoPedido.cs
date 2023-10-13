using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.Atendimento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoDescricaoAoItemDoPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "ItemDePedido",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "ItemDePedido");
        }
    }
}
