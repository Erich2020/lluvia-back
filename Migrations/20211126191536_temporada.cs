using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace TempporalWS.Migrations
{
    public partial class temporada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    Descripcion = table.Column<string>(type: "text", maxLength: 9999, nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Existencia = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "ProductosTicket",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    Descripcion = table.Column<string>(type: "text", maxLength: 9999, nullable: true),
                    Cantidad = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Importe = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductosTicket", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Folio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ListaProductosString = table.Column<string>(type: "text", maxLength: 9999, nullable: true),
                    TotalArticulos = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TotalVenta = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Fecha = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    Hora = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    Fk_Usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Folio);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Username = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Password = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: true),
                    Rol = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "ProductosTicket");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
