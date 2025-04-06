using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace fiap_fase1_tech_challenge.Database.Migrations
{
    /// <inheritdoc />
    public partial class seed_initial_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7f3fec04-b8f5-4a9a-b8d7-24e15abb6494"), "user" },
                    { new Guid("abbf75f9-53d8-4c80-9d3a-40cc1dd117ab"), "admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "RoleId" },
                values: new object[] { new Guid("c4fa95b4-1d43-4a79-a63e-5c64856cbdf6"), "admin@mail.com", "admin", "Ad123p@ssword", new Guid("abbf75f9-53d8-4c80-9d3a-40cc1dd117ab") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7f3fec04-b8f5-4a9a-b8d7-24e15abb6494"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c4fa95b4-1d43-4a79-a63e-5c64856cbdf6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("abbf75f9-53d8-4c80-9d3a-40cc1dd117ab"));
        }
    }
}
