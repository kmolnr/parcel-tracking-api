using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParcelTracking.EFCore.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParcelStatus",
                columns: table => new
                {
                    ParcelStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HungarianDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelStatus", x => x.ParcelStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Parcel",
                columns: table => new
                {
                    ParcelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcel", x => x.ParcelId);
                    table.ForeignKey(
                        name: "FK_Parcel_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParcelItem",
                columns: table => new
                {
                    ParcelItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParcelId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelItem", x => x.ParcelItemId);
                    table.ForeignKey(
                        name: "FK_ParcelItem_Parcel_ParcelId",
                        column: x => x.ParcelId,
                        principalTable: "Parcel",
                        principalColumn: "ParcelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParcelItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParcelParcelStatus",
                columns: table => new
                {
                    ParcelId = table.Column<int>(type: "int", nullable: false),
                    ParcelStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelParcelStatus", x => new { x.ParcelId, x.ParcelStatusId });
                    table.ForeignKey(
                        name: "FK_ParcelParcelStatus_Parcel_ParcelId",
                        column: x => x.ParcelId,
                        principalTable: "Parcel",
                        principalColumn: "ParcelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParcelParcelStatus_ParcelStatus_ParcelStatusId",
                        column: x => x.ParcelStatusId,
                        principalTable: "ParcelStatus",
                        principalColumn: "ParcelStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ParcelStatus",
                columns: new[] { "ParcelStatusId", "Code", "Description", "HungarianDescription" },
                values: new object[,]
                {
                    { 1, "WfPU", "Waiting for Pick Up", "Csomag a feladónál. Futárra vár." },
                    { 2, "PU", "Picked Up", "Csomag a futárnál. Depóba tart." },
                    { 3, "ID", "In Deposit", "Depóban van. Kiszállításra vár." },
                    { 4, "OD", "On Delivery", "Kiszállítás alatt áll. Célba tart." },
                    { 5, "DD", "Delivered", "Kiszállítva." }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Name" },
                values: new object[,]
                {
                    { 1, "Fender Stratocaster" },
                    { 2, "Gibson Les Paul" },
                    { 3, "Fender Telecaster" },
                    { 4, "Gibson SG Standard" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "user1", "user1", "user" },
                    { 2, "user2", "user2", "user" }
                });

            migrationBuilder.InsertData(
                table: "Parcel",
                columns: new[] { "ParcelId", "Code", "UserId" },
                values: new object[,]
                {
                    { 1, "QWER0001", 1 },
                    { 2, "QWER0002", 1 },
                    { 3, "QWER0003", 2 },
                    { 4, "QWER0004", 2 }
                });

            migrationBuilder.InsertData(
                table: "ParcelItem",
                columns: new[] { "ParcelItemId", "ParcelId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "ParcelParcelStatus",
                columns: new[] { "ParcelId", "ParcelStatusId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 5 },
                    { 2, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 3, 3 },
                    { 4, 3 },
                    { 4, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parcel_Code",
                table: "Parcel",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parcel_UserId",
                table: "Parcel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelItem_ParcelId",
                table: "ParcelItem",
                column: "ParcelId");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelItem_ProductId",
                table: "ParcelItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelParcelStatus_ParcelStatusId",
                table: "ParcelParcelStatus",
                column: "ParcelStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelStatus_Code",
                table: "ParcelStatus",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Name",
                table: "User",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParcelItem");

            migrationBuilder.DropTable(
                name: "ParcelParcelStatus");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Parcel");

            migrationBuilder.DropTable(
                name: "ParcelStatus");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
