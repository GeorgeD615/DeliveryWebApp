using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class ImageInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Image",
                columns: new[] { "Id", "ProductId", "Url" },
                values: new object[,]
                {
                    { new Guid("35aba7da-0380-4ef6-81e2-715765b2c60b"), new Guid("1ee09a09-d3fa-464d-a81c-28907cbae5e0"), "/images/products/35aba7da-0380-4ef6-81e2-715765b2c60b.jpg" },
                    { new Guid("499c6682-166c-4983-ae0f-fd769210190f"), new Guid("2ad860b1-90b1-41ba-b703-350a4af09585"), "/images/products/499c6682-166c-4983-ae0f-fd769210190f.jpg" },
                    { new Guid("555f5555-0fb8-42b2-a683-d129a0540b1e"), new Guid("c04c74b4-25ee-483c-b63c-e7c62ddfb2f4"), "/images/products/555f5555-0fb8-42b2-a683-d129a0540b1e.jpg" },
                    { new Guid("70bfa4b1-15ec-4165-9180-5137f0c94fd5"), new Guid("50d4c0c8-247b-4725-9dcc-4b7d40d200d9"), "/images/products/70bfa4b1-15ec-4165-9180-5137f0c94fd5.jpeg" },
                    { new Guid("863e76e0-7dd9-4291-9dcf-d5f48bec0999"), new Guid("df650d29-4749-430f-b5ae-9a464b402cd8"), "/images/products/863e76e0-7dd9-4291-9dcf-d5f48bec0999.jpg" },
                    { new Guid("8e925d1d-7c28-4519-831d-a33ad731338e"), new Guid("31b3b010-a18a-4f10-b334-ff29e6376fc4"), "/images/products/8e925d1d-7c28-4519-831d-a33ad731338e.jpg" },
                    { new Guid("9140d097-a558-4bba-92e1-5f1862480d1d"), new Guid("455ef917-bb65-44bc-856e-36a45f788e26"), "/images/products/9140d097-a558-4bba-92e1-5f1862480d1d.jpg" },
                    { new Guid("a6600881-74b9-4cbe-b2fd-19cdbbe4dadc"), new Guid("a8126b52-7a71-4604-8b51-8ed1e78fe115"), "/images/products/a6600881-74b9-4cbe-b2fd-19cdbbe4dadc.jpg" },
                    { new Guid("ce411d49-5599-4aa3-a056-f8591bc65480"), new Guid("9a8e4885-d9e9-4a27-bc0e-444d034b7276"), "/images/products/ce411d49-5599-4aa3-a056-f8591bc65480.jpg" },
                    { new Guid("e4f305ee-bbb7-4781-b127-64e079948094"), new Guid("6844a9af-cf36-42c8-9ab6-694e2d91fbf1"), "/images/products/e4f305ee-bbb7-4781-b127-64e079948094.jpg" },
                    { new Guid("ebc925ec-6233-4614-8b1f-3fab757a88ac"), new Guid("2ee38d36-d843-4540-a6c1-e76c4c7337f1"), "/images/products/ebc925ec-6233-4614-8b1f-3fab757a88ac.jpg" },
                    { new Guid("f12d1d26-f70d-4ac7-a81b-eb95a6c672c5"), new Guid("b1747d04-5529-4b07-bd2f-de95656d6a48"), "/images/products/f12d1d26-f70d-4ac7-a81b-eb95a6c672c5.jpg" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("455ef917-bb65-44bc-856e-36a45f788e26"),
                column: "Description",
                value: "Очень вкусная булочка");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ProductId",
                table: "Image",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1ee09a09-d3fa-464d-a81c-28907cbae5e0"),
                column: "ImagePath",
                value: "/images/products/dolma.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2ad860b1-90b1-41ba-b703-350a4af09585"),
                column: "ImagePath",
                value: "/images/products/khinkali.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2ee38d36-d843-4540-a6c1-e76c4c7337f1"),
                column: "ImagePath",
                value: "/images/products/kufta.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("31b3b010-a18a-4f10-b334-ff29e6376fc4"),
                column: "ImagePath",
                value: "/images/products/samsa.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("455ef917-bb65-44bc-856e-36a45f788e26"),
                columns: new[] { "Description", "ImagePath" },
                values: new object[] { "Очень вкусная булочка ver2.0", "/images/products/eachpochmak.jpg" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("50d4c0c8-247b-4725-9dcc-4b7d40d200d9"),
                column: "ImagePath",
                value: "/images/products/nams.jpeg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6844a9af-cf36-42c8-9ab6-694e2d91fbf1"),
                column: "ImagePath",
                value: "/images/products/chuck-chuck.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9a8e4885-d9e9-4a27-bc0e-444d034b7276"),
                column: "ImagePath",
                value: "/images/products/chihirtma.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a8126b52-7a71-4604-8b51-8ed1e78fe115"),
                column: "ImagePath",
                value: "/images/products/hash.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1747d04-5529-4b07-bd2f-de95656d6a48"),
                column: "ImagePath",
                value: "/images/products/hachapury.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c04c74b4-25ee-483c-b63c-e7c62ddfb2f4"),
                column: "ImagePath",
                value: "/images/products/chahohbili.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("df650d29-4749-430f-b5ae-9a464b402cd8"),
                column: "ImagePath",
                value: "/images/products/soup_harcho.jpg");
        }
    }
}
