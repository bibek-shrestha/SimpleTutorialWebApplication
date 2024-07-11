using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimpleTutorialWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Most popular city in the whole New South Wales.", "Sydney" },
                    { 2, "A major hub including china town.", "Burwood" },
                    { 3, "A quite suburb with waterfronts to Parammatta River.", "Rhodes" }
                });

            migrationBuilder.InsertData(
                table: "PointOfInterests",
                columns: new[] { "Id", "CityId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "A view of harbor illuminated with lights and other attractions.", "Darling Harbor" },
                    { 2, 1, "A location for food and enjoyment with access to Ferries.", "Barangaroo" },
                    { 3, 2, "A collection of shops and entertainment from asian culture.", "China town" },
                    { 4, 2, "A green park to carry out some fitness routine and to get your body moving with some extra curricular activities.", "Burwood Park" },
                    { 5, 3, "A great water side view of the Paramatta river.", "Water side" },
                    { 6, 3, "A park for casual workout and enjoy the Paramatta river.", "Mill Park" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PointOfInterests",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
