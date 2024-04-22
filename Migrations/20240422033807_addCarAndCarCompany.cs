using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebCar.Migrations
{
    /// <inheritdoc />
    public partial class addCarAndCarCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    logo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ten = table.Column<string>(type: "text", nullable: false),
                    hinh = table.Column<List<string>>(type: "text[]", nullable: false),
                    phienBan = table.Column<string>(type: "text", nullable: false),
                    namSanXuat = table.Column<int>(type: "integer", nullable: false),
                    dungTich = table.Column<double>(type: "double precision", nullable: false),
                    hopSo = table.Column<string>(type: "text", nullable: false),
                    kieuDang = table.Column<string>(type: "text", nullable: false),
                    tinhTrang = table.Column<string>(type: "text", nullable: false),
                    nhienLieu = table.Column<string>(type: "text", nullable: false),
                    kichThuoc = table.Column<int>(type: "integer", nullable: false),
                    soGhe = table.Column<int>(type: "integer", nullable: false),
                    gia = table.Column<decimal>(type: "numeric", nullable: false),
                    ngayTao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CarCompanyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarCompanies_CarCompanyId",
                        column: x => x.CarCompanyId,
                        principalTable: "CarCompanies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarCompanyId",
                table: "Cars",
                column: "CarCompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "CarCompanies");
        }
    }
}
