using Microsoft.EntityFrameworkCore.Migrations;

namespace AgendaT.Migrations
{
    public partial class creacionDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    PersonaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 40, nullable: false),
                    Apellido = table.Column<string>(maxLength: 40, nullable: false),
                    Edad = table.Column<int>(nullable: false),
                    Correo = table.Column<string>(maxLength: 40, nullable: false),
                    Telefono = table.Column<string>(nullable: false),
                    TipoTelefono = table.Column<int>(nullable: false),
                    Foto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.PersonaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persona");
        }
    }
}
