﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dciSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBankEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Banks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Banks");
        }
    }
}
