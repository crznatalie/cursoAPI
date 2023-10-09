using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ControleFacil.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriarEntidadeApagar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NaturezaDeLancamento_usuario_IdUsuario",
                table: "NaturezaDeLancamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NaturezaDeLancamento",
                table: "NaturezaDeLancamento");

            migrationBuilder.RenameTable(
                name: "NaturezaDeLancamento",
                newName: "naturezadelancamento");

            migrationBuilder.RenameIndex(
                name: "IX_NaturezaDeLancamento_IdUsuario",
                table: "naturezadelancamento",
                newName: "IX_naturezadelancamento_IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_naturezadelancamento",
                table: "naturezadelancamento",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "apagar",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUsuario = table.Column<long>(type: "bigint", nullable: false),
                    idNaturezaDeLancamento = table.Column<long>(type: "bigint", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR", nullable: false),
                    ValorInicial = table.Column<double>(type: "double precision", nullable: false),
                    ValorPago = table.Column<double>(type: "double precision", nullable: false),
                    Observacao = table.Column<string>(type: "VARCHAR", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "timestamp", nullable: false),
                    DataInativacao = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DataReferencia = table.Column<DateTime>(type: "timestamp", nullable: true),
                    DataPagamento = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apagar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_apagar_naturezadelancamento_idNaturezaDeLancamento",
                        column: x => x.idNaturezaDeLancamento,
                        principalTable: "naturezadelancamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_apagar_usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_apagar_idNaturezaDeLancamento",
                table: "apagar",
                column: "idNaturezaDeLancamento");

            migrationBuilder.CreateIndex(
                name: "IX_apagar_IdUsuario",
                table: "apagar",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_naturezadelancamento_usuario_IdUsuario",
                table: "naturezadelancamento",
                column: "IdUsuario",
                principalTable: "usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_naturezadelancamento_usuario_IdUsuario",
                table: "naturezadelancamento");

            migrationBuilder.DropTable(
                name: "apagar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_naturezadelancamento",
                table: "naturezadelancamento");

            migrationBuilder.RenameTable(
                name: "naturezadelancamento",
                newName: "NaturezaDeLancamento");

            migrationBuilder.RenameIndex(
                name: "IX_naturezadelancamento_IdUsuario",
                table: "NaturezaDeLancamento",
                newName: "IX_NaturezaDeLancamento_IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NaturezaDeLancamento",
                table: "NaturezaDeLancamento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NaturezaDeLancamento_usuario_IdUsuario",
                table: "NaturezaDeLancamento",
                column: "IdUsuario",
                principalTable: "usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
