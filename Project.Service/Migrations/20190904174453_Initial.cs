using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Service.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleMakers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Abrv = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleMakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Abrv = table.Column<string>(maxLength: 10, nullable: true),
                    MakeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleModels_VehicleMakers_MakeId",
                        column: x => x.MakeId,
                        principalTable: "VehicleMakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "VehicleMakers",
                columns: new[] { "Id", "Abrv", "Name" },
                values: new object[,]
                {
                    { 1, "A", "Audi" },
                    { 2, "D", "Dacia" },
                    { 3, "B", "BMW" },
                    { 4, "F", "Ford" },
                    { 5, "P", "Peugeot" }
                });

            migrationBuilder.InsertData(
                table: "VehicleModels",
                columns: new[] { "Id", "Abrv", "MakeId", "Name" },
                values: new object[,]
                {
                    { 6, "A", 1, "S4" },
                    { 7, "A", 1, "S6" },
                    { 8, "A", 1, "A4" },
                    { 9, "A", 1, "A6" },
                    { 10, "A", 1, "A8" },
                    { 11, "S", 2, "Sandero" },
                    { 12, "D", 2, "Duster" },
                    { 13, "S", 3, "X5" },
                    { 14, "D", 3, "X6" },
                    { 15, "F", 4, "Fiesta" },
                    { 16, "M", 4, "Mondeo" },
                    { 17, "1", 5, "106" },
                    { 18, "2", 5, "207" },
                    { 19, "3", 5, "306" },
                    { 20, "4", 5, "407" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_MakeId",
                table: "VehicleModels",
                column: "MakeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleModels");

            migrationBuilder.DropTable(
                name: "VehicleMakers");
        }
    }
}
