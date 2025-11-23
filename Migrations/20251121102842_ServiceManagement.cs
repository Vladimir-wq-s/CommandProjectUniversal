using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CommandProjectUniversal.Migrations
{
    /// <inheritdoc />
    public partial class ServiceManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Providers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProviderId = table.Column<int>(type: "INTEGER", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    SpeedMbps = table.Column<int>(type: "INTEGER", nullable: false),
                    PricePerMonth = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataLimitGB = table.Column<int>(type: "INTEGER", nullable: false),
                    LaunchYear = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePlans_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicePlans_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServicePlanId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ContractNumber = table.Column<string>(type: "TEXT", nullable: true),
                    InstallationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_ServicePlans_ServicePlanId",
                        column: x => x.ServicePlanId,
                        principalTable: "ServicePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SalePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "RU", "Россия" },
                    { 2, "UA", "Украина" },
                    { 3, "BY", "Беларусь" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "Delfina.Lindgren8@hotmail.com", "Noelia Monahan", "888.872.5597" },
                    { 2, "Piper99@gmail.com", "Guillermo Ortiz", "1-568-214-0760" },
                    { 3, "Mia54@yahoo.com", "Dorian Rath", "841-421-2861" },
                    { 4, "Bryce.Douglas@yahoo.com", "Carmelo Heidenreich", "1-666-404-9404 x09892" },
                    { 5, "Jayce_Kshlerin@yahoo.com", "Sandra Lindgren", "581-379-7176 x19954" },
                    { 6, "Green38@gmail.com", "Pat Terry", "(738) 256-5325 x43826" },
                    { 7, "Nona.Doyle16@yahoo.com", "Sonia Balistreri", "(556) 769-4182" },
                    { 8, "Dessie29@yahoo.com", "Verla Nolan", "366.428.6509" },
                    { 9, "Olaf.Dicki61@yahoo.com", "Destin Kerluke", "1-956-989-0138 x152" },
                    { 10, "Claud.Bradtke@yahoo.com", "Lauryn Cormier", "1-408-932-8719 x49535" }
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "Id", "Address", "CountryId", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "ул. Гончарная, д. 30, Москва, 115172", 1, "info@rt.ru", "Ростелеком", "+7 (495) 771-11-11" },
                    { 2, "ул. Марксистская, д. 4, Москва, 109147", 1, "info@mts.ru", "МТС", "+7 (495) 788-00-00" },
                    { 3, "ул. 2-я Хуторская, д. 38А, стр. 26, Москва, 127287", 1, "info@beeline.ru", "Билайн", "+7 (495) 797-27-27" }
                });

            migrationBuilder.InsertData(
                table: "ServicePlans",
                columns: new[] { "Id", "CountryId", "DataLimitGB", "LaunchYear", "Name", "PricePerMonth", "ProviderId", "SpeedMbps" },
                values: new object[,]
                {
                    { 1, 1, 0, 2020, "Онлайн 100", 550m, 1, 100 },
                    { 2, 1, 0, 2019, "Онлайн 200", 750m, 1, 200 },
                    { 3, 1, 0, 2021, "Онлайн 500", 1200m, 1, 500 },
                    { 4, 1, 0, 2018, "Интернет 100", 500m, 2, 100 },
                    { 5, 1, 0, 2020, "Интернет 300", 700m, 2, 300 },
                    { 6, 1, 0, 2022, "Интернет 500", 1000m, 2, 500 },
                    { 7, 1, 0, 2017, "Интернет 50", 400m, 3, 50 },
                    { 8, 1, 0, 2019, "Интернет 100", 600m, 3, 100 },
                    { 9, 1, 0, 2021, "Интернет 300", 850m, 3, 300 }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Address", "ContractNumber", "InstallationDate", "IsActive", "Name", "ServicePlanId" },
                values: new object[,]
                {
                    { 1, "929 Skiles Forks, Kemmerborough, Venezuela", "adenokebh3", new DateTime(2022, 6, 3, 4, 37, 36, 248, DateTimeKind.Local).AddTicks(1779), false, "", 9 },
                    { 2, "0933 Schowalter Fields, Port Ulices, China", "suikkxlmnk", new DateTime(2025, 1, 5, 2, 12, 30, 386, DateTimeKind.Local).AddTicks(9451), false, "", 3 },
                    { 3, "176 Deckow Way, West Carolyneview, China", "wl7kkohzmk", new DateTime(2021, 9, 20, 22, 49, 19, 509, DateTimeKind.Local).AddTicks(6480), false, "", 7 },
                    { 4, "28987 Schuppe Avenue, Bernhardchester, Northern Mariana Islands", "2a7e0ugkdp", new DateTime(2022, 3, 17, 5, 17, 26, 50, DateTimeKind.Local).AddTicks(8348), false, "", 8 },
                    { 5, "5150 Hoeger Pines, Charleymouth, Ukraine", "ndavtc2a6o", new DateTime(2022, 2, 21, 11, 44, 20, 653, DateTimeKind.Local).AddTicks(3352), false, "", 6 },
                    { 6, "69075 Miller Mountains, Prudenceland, Netherlands Antilles", "tibbrvi3ts", new DateTime(2022, 6, 18, 18, 37, 20, 561, DateTimeKind.Local).AddTicks(269), true, "", 8 },
                    { 7, "04113 Alejandra Mountains, Krajcikhaven, Honduras", "b98kogxjhl", new DateTime(2025, 1, 1, 16, 7, 8, 818, DateTimeKind.Local).AddTicks(2213), true, "", 6 },
                    { 8, "416 Thelma Trafficway, West Lincoln, Comoros", "vdml3fdyy9", new DateTime(2022, 7, 1, 11, 35, 39, 432, DateTimeKind.Local).AddTicks(7768), true, "", 5 },
                    { 9, "04851 Champlin Rest, New Murlmouth, Syrian Arab Republic", "0a30fxoalc", new DateTime(2024, 11, 6, 19, 34, 44, 907, DateTimeKind.Local).AddTicks(2667), false, "", 7 },
                    { 10, "910 Vada Alley, West Neldaland, Egypt", "f236nfhfnj", new DateTime(2023, 4, 21, 4, 0, 34, 717, DateTimeKind.Local).AddTicks(7800), false, "", 3 },
                    { 11, "432 Howell Motorway, Binsmouth, French Guiana", "bi804op5qf", new DateTime(2024, 11, 7, 12, 21, 44, 956, DateTimeKind.Local).AddTicks(5057), false, "", 2 },
                    { 12, "256 Stiedemann Court, Justineland, Cote d'Ivoire", "g6gx418v12", new DateTime(2022, 4, 3, 10, 41, 47, 777, DateTimeKind.Local).AddTicks(8656), false, "", 5 },
                    { 13, "8564 Guy Falls, Port Clara, Nauru", "kvq49sda5q", new DateTime(2022, 7, 20, 7, 38, 9, 948, DateTimeKind.Local).AddTicks(5622), false, "", 8 },
                    { 14, "1008 Emmitt Brooks, Freidatown, Falkland Islands (Malvinas)", "mgly9vm1a6", new DateTime(2023, 1, 9, 21, 47, 3, 749, DateTimeKind.Local).AddTicks(6913), true, "", 9 },
                    { 15, "6973 Zackary Circles, Port Cliffordchester, Ecuador", "ql090xcw48", new DateTime(2023, 5, 21, 16, 6, 58, 681, DateTimeKind.Local).AddTicks(3140), false, "", 2 }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "CustomerId", "Description", "SaleDate", "SalePrice", "ServiceId" },
                values: new object[,]
                {
                    { 1, 7, "Et omnis est suscipit aut.", new DateTime(2024, 8, 25, 16, 5, 23, 597, DateTimeKind.Local).AddTicks(8466), 1866.103417644505800m, 1 },
                    { 2, 2, "Laboriosam facilis est dolor qui.", new DateTime(2025, 4, 18, 2, 17, 3, 39, DateTimeKind.Local).AddTicks(5100), 2054.94295971066400m, 2 },
                    { 3, 5, "Ut optio dolorum repudiandae sequi nihil commodi.", new DateTime(2025, 4, 4, 1, 8, 24, 140, DateTimeKind.Local).AddTicks(7845), 3231.595655948372200m, 3 },
                    { 4, 6, "Quo earum eos odio occaecati cupiditate sit nihil.", new DateTime(2024, 3, 18, 6, 43, 49, 824, DateTimeKind.Local).AddTicks(1016), 3624.166352970587800m, 4 },
                    { 5, 9, "Odio quia dolorem accusantium sit.", new DateTime(2024, 3, 22, 4, 54, 17, 167, DateTimeKind.Local).AddTicks(1658), 1150.566391279122200m, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Providers_CountryId",
                table: "Providers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ServiceId",
                table: "Sales",
                column: "ServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServicePlans_CountryId",
                table: "ServicePlans",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePlans_ProviderId",
                table: "ServicePlans",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServicePlanId",
                table: "Services",
                column: "ServicePlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "ServicePlans");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
