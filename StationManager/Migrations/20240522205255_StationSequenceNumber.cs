using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StationManager.Migrations
{
    /// <inheritdoc />
    public partial class StationSequenceNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SequenceNumber",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentValue = table.Column<int>(type: "integer", nullable: false),
                    StationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SequenceNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SequenceNumber_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SequenceNumber_StationId",
                table: "SequenceNumber",
                column: "StationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SequenceNumber");
        }
    }
}
