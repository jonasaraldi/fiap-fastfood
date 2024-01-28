using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.Pedidos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriandoPropriedadePagoNoPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Pago",
                table: "Pedido",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pago",
                table: "Pedido");
        }
    }
}
