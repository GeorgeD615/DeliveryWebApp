using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "ImagePath", "Name" },
                values: new object[,]
                {
                    { new Guid("1ee09a09-d3fa-464d-a81c-28907cbae5e0"), 250m, "Рулетики из виноградных листьев с фаршем внутри", "/images/products/dolma.jpg", "Долма" },
                    { new Guid("2ad860b1-90b1-41ba-b703-350a4af09585"), 300m, "Очень вкусные пельмени", "/images/products/khinkali.jpg", "Хинкали" },
                    { new Guid("2ee38d36-d843-4540-a6c1-e76c4c7337f1"), 230m, "Армянские тефтели", "/images/products/kufta.jpg", "Кюфта" },
                    { new Guid("31b3b010-a18a-4f10-b334-ff29e6376fc4"), 90m, "Узбекский пирожок", "/images/products/samsa.jpg", "Самса" },
                    { new Guid("455ef917-bb65-44bc-856e-36a45f788e26"), 90m, "Очень вкусная булочка ver2.0", "/images/products/eachpochmak.jpg", "Эчпочмак" },
                    { new Guid("50d4c0c8-247b-4725-9dcc-4b7d40d200d9"), 300m, "Вьетнамские фаршированные рулетики", "/images/products/nams.jpeg", "Нэмы" },
                    { new Guid("6844a9af-cf36-42c8-9ab6-694e2d91fbf1"), 200m, "Традиционный татарский десерт", "/images/products/chuck-chuck.jpg", "Чак-чак" },
                    { new Guid("9a8e4885-d9e9-4a27-bc0e-444d034b7276"), 250m, "Густой грузинский суп", "/images/products/chihirtma.jpg", "Чихиртма" },
                    { new Guid("a8126b52-7a71-4604-8b51-8ed1e78fe115"), 450m, "Горячий, очень сытный и жирный суп из говяжьих ног", "/images/products/hash.jpg", "Хаш" },
                    { new Guid("b1747d04-5529-4b07-bd2f-de95656d6a48"), 125m, "Очень вкусная лепёшка ver2.0", "/images/products/hachapury.jpg", "Хачапури" },
                    { new Guid("c04c74b4-25ee-483c-b63c-e7c62ddfb2f4"), 400m, "Рагу из птицы, тушенной с овощами, зеленью и специями", "/images/products/chahohbili.jpg", "Чахохбили" },
                    { new Guid("df650d29-4749-430f-b5ae-9a464b402cd8"), 350m, "Вкусный, ароматный, острый", "/images/products/soup_harcho.jpg", "Суп «Харчо»" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1ee09a09-d3fa-464d-a81c-28907cbae5e0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2ad860b1-90b1-41ba-b703-350a4af09585"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2ee38d36-d843-4540-a6c1-e76c4c7337f1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("31b3b010-a18a-4f10-b334-ff29e6376fc4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("455ef917-bb65-44bc-856e-36a45f788e26"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("50d4c0c8-247b-4725-9dcc-4b7d40d200d9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6844a9af-cf36-42c8-9ab6-694e2d91fbf1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9a8e4885-d9e9-4a27-bc0e-444d034b7276"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a8126b52-7a71-4604-8b51-8ed1e78fe115"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b1747d04-5529-4b07-bd2f-de95656d6a48"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c04c74b4-25ee-483c-b63c-e7c62ddfb2f4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("df650d29-4749-430f-b5ae-9a464b402cd8"));
        }
    }
}
