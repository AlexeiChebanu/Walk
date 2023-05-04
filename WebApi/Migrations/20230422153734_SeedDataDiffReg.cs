using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataDiffReg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("8298bd60-08d1-4a81-8aab-deb0c91ac08f"), "Medium" },
                    { new Guid("9e6ce577-1469-4137-8fcd-45dc5049ad1d"), "Easy" },
                    { new Guid("a97f3970-0261-4678-8387-39535c8802e6"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "ImageRegionUri", "Name" },
                values: new object[,]
                {
                    { new Guid("510c9613-7f9a-420f-9ec3-42254fee2fa0"), "LV", "world.jpg", "Lviv" },
                    { new Guid("e022f144-e5ad-45ae-84c5-40dfa3f08f9d"), "KV", "hello-world.jpg", "Kyiv" },
                    { new Guid("e6546eb9-0c71-4c3a-8519-3acbea6eb313"), "ODS", "bb.jpg", "Odesa" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8298bd60-08d1-4a81-8aab-deb0c91ac08f"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("9e6ce577-1469-4137-8fcd-45dc5049ad1d"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a97f3970-0261-4678-8387-39535c8802e6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("510c9613-7f9a-420f-9ec3-42254fee2fa0"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e022f144-e5ad-45ae-84c5-40dfa3f08f9d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e6546eb9-0c71-4c3a-8519-3acbea6eb313"));
        }
    }
}
