using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Docentes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEspecialidadTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cursos_impartidos_docente_docente_id",
                table: "cursos_impartidos");

            migrationBuilder.CreateTable(
                name: "especialidades",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_especialidades", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "fk_cursos_impartidos_docentes_docente_id",
                table: "cursos_impartidos",
                column: "docente_id",
                principalTable: "docentes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cursos_impartidos_docentes_docente_id",
                table: "cursos_impartidos");

            migrationBuilder.DropTable(
                name: "especialidades");

            migrationBuilder.AddForeignKey(
                name: "fk_cursos_impartidos_docente_docente_id",
                table: "cursos_impartidos",
                column: "docente_id",
                principalTable: "docentes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
