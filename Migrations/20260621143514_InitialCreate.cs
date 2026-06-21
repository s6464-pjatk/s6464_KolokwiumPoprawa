using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace s6464_KolokwiumPoprawa.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    client_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.client_id);
                });

            migrationBuilder.CreateTable(
                name: "Mechanic",
                columns: table => new
                {
                    mechanic_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    licence_number = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanic", x => x.mechanic_id);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    service_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    base_fee = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.service_id);
                });

            migrationBuilder.CreateTable(
                name: "Visit",
                columns: table => new
                {
                    visit_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    mechanic_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visit", x => x.visit_id);
                    table.ForeignKey(
                        name: "FK_Visit_Client_client_id",
                        column: x => x.client_id,
                        principalTable: "Client",
                        principalColumn: "client_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visit_Mechanic_mechanic_id",
                        column: x => x.mechanic_id,
                        principalTable: "Mechanic",
                        principalColumn: "mechanic_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visit_Service",
                columns: table => new
                {
                    visit_id = table.Column<int>(type: "int", nullable: false),
                    service_id = table.Column<int>(type: "int", nullable: false),
                    service_fee = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visit_Service", x => new { x.visit_id, x.service_id });
                    table.ForeignKey(
                        name: "FK_Visit_Service_Service_service_id",
                        column: x => x.service_id,
                        principalTable: "Service",
                        principalColumn: "service_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visit_Service_Visit_visit_id",
                        column: x => x.visit_id,
                        principalTable: "Visit",
                        principalColumn: "visit_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mechanic_licence_number",
                table: "Mechanic",
                column: "licence_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_name",
                table: "Service",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visit_client_id",
                table: "Visit",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_mechanic_id",
                table: "Visit",
                column: "mechanic_id");

            migrationBuilder.CreateIndex(
                name: "IX_Visit_Service_service_id",
                table: "Visit_Service",
                column: "service_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Visit_Service");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Visit");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Mechanic");
        }
    }
}
