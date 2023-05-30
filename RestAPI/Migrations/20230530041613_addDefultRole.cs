using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestAPI.Migrations
{
    /// <inheritdoc />
    public partial class addDefultRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "guid", "created_date", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), new DateTime(2023, 5, 30, 11, 16, 13, 178, DateTimeKind.Local).AddTicks(7689), new DateTime(2023, 5, 30, 11, 16, 13, 178, DateTimeKind.Local).AddTicks(7690), "Admin" },
                    { new Guid("c22a20c5-0149-41fd-bffd-08db60bf618f"), new DateTime(2023, 5, 30, 11, 16, 13, 178, DateTimeKind.Local).AddTicks(7685), new DateTime(2023, 5, 30, 11, 16, 13, 178, DateTimeKind.Local).AddTicks(7686), "Manager" },
                    { new Guid("f147a695-1a4f-4960-bffc-08db60bf618f"), new DateTime(2023, 5, 30, 11, 16, 13, 178, DateTimeKind.Local).AddTicks(7660), new DateTime(2023, 5, 30, 11, 16, 13, 178, DateTimeKind.Local).AddTicks(7679), "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("c22a20c5-0149-41fd-bffd-08db60bf618f"));

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("f147a695-1a4f-4960-bffc-08db60bf618f"));
        }
    }
}
