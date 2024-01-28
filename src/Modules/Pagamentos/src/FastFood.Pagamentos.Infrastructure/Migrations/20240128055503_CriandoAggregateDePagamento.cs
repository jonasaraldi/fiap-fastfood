using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.Pagamentos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriandoAggregateDePagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    PedidoId = table.Column<string>(type: "text", nullable: false),
                    Situacao = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_PedidoId",
                table: "Pagamento",
                column: "PedidoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagamento");
        }
    }
}
