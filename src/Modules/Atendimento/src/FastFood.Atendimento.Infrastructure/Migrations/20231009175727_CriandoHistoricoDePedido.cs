using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.Atendimento.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriandoHistoricoDePedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Pedido",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ItemDePedido",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Cliente",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HistoricoDePedido",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PedidoId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoDePedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoDePedido_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoDePedido_PedidoId",
                table: "HistoricoDePedido",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoDePedido");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ItemDePedido");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Cliente");
        }
    }
}
