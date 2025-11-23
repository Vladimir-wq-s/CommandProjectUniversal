using CommandProjectUniversal.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandProjectUniversal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<ServicePlan> ServicePlans { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Россия", Code = "RU" },
                new Country { Id = 2, Name = "Украина", Code = "UA" },
                new Country { Id = 3, Name = "Беларусь", Code = "BY" }
            );

            modelBuilder.Entity<Provider>().HasData(
                new Provider { Id = 1, Name = "Ростелеком", CountryId = 1, Address = "ул. Гончарная, д. 30, Москва, 115172", Phone = "+7 (495) 771-11-11", Email = "info@rt.ru" },
                new Provider { Id = 2, Name = "МТС", CountryId = 1, Address = "ул. Марксистская, д. 4, Москва, 109147", Phone = "+7 (495) 788-00-00", Email = "info@mts.ru" },
                new Provider { Id = 3, Name = "Билайн", CountryId = 1, Address = "ул. 2-я Хуторская, д. 38А, стр. 26, Москва, 127287", Phone = "+7 (495) 797-27-27", Email = "info@beeline.ru" }
            );

            modelBuilder.Entity<ServicePlan>().HasData(
                new ServicePlan { Id = 1, Name = "Онлайн 100", PricePerMonth = 550m, ProviderId = 1, CountryId = 1, SpeedMbps = 100, DataLimitGB = 0, LaunchYear = 2020 },
                new ServicePlan { Id = 2, Name = "Онлайн 200", PricePerMonth = 750m, ProviderId = 1, CountryId = 1, SpeedMbps = 200, DataLimitGB = 0, LaunchYear = 2019 },
                new ServicePlan { Id = 3, Name = "Онлайн 500", PricePerMonth = 1200m, ProviderId = 1, CountryId = 1, SpeedMbps = 500, DataLimitGB = 0, LaunchYear = 2021 },
                new ServicePlan { Id = 4, Name = "Интернет 100", PricePerMonth = 500m, ProviderId = 2, CountryId = 1, SpeedMbps = 100, DataLimitGB = 0, LaunchYear = 2018 },
                new ServicePlan { Id = 5, Name = "Интернет 300", PricePerMonth = 700m, ProviderId = 2, CountryId = 1, SpeedMbps = 300, DataLimitGB = 0, LaunchYear = 2020 },
                new ServicePlan { Id = 6, Name = "Интернет 500", PricePerMonth = 1000m, ProviderId = 2, CountryId = 1, SpeedMbps = 500, DataLimitGB = 0, LaunchYear = 2022 },
                new ServicePlan { Id = 7, Name = "Интернет 50", PricePerMonth = 400m, ProviderId = 3, CountryId = 1, SpeedMbps = 50, DataLimitGB = 0, LaunchYear = 2017 },
                new ServicePlan { Id = 8, Name = "Интернет 100", PricePerMonth = 600m, ProviderId = 3, CountryId = 1, SpeedMbps = 100, DataLimitGB = 0, LaunchYear = 2019 },
                new ServicePlan { Id = 9, Name = "Интернет 300", PricePerMonth = 850m, ProviderId = 3, CountryId = 1, SpeedMbps = 300, DataLimitGB = 0, LaunchYear = 2021 }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Noelia Monahan", Email = "Delfina.Lindgren8@hotmail.com", Phone = "888.872.5597" },
                new Customer { Id = 2, Name = "Guillermo Ortiz", Email = "Piper99@gmail.com", Phone = "1-568-214-0760" },
                new Customer { Id = 3, Name = "Dorian Rath", Email = "Mia54@yahoo.com", Phone = "841-421-2861" },
                new Customer { Id = 4, Name = "Carmelo Heidenreich", Email = "Bryce.Douglas@yahoo.com", Phone = "1-666-404-9404 x09892" },
                new Customer { Id = 5, Name = "Sandra Lindgren", Email = "Jayce_Kshlerin@yahoo.com", Phone = "581-379-7176 x19954" },
                new Customer { Id = 6, Name = "Pat Terry", Email = "Green38@gmail.com", Phone = "(738) 256-5325 x43826" },
                new Customer { Id = 7, Name = "Sonia Balistreri", Email = "Nona.Doyle16@yahoo.com", Phone = "(556) 769-4182" },
                new Customer { Id = 8, Name = "Verla Nolan", Email = "Dessie29@yahoo.com", Phone = "366.428.6509" },
                new Customer { Id = 9, Name = "Destin Kerluke", Email = "Olaf.Dicki61@yahoo.com", Phone = "1-956-989-0138 x152" },
                new Customer { Id = 10, Name = "Lauryn Cormier", Email = "Claud.Bradtke@yahoo.com", Phone = "1-408-932-8719 x49535" }
            );

            modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, ServicePlanId = 9, Name = "", ContractNumber = "adenokebh3", InstallationDate = new DateTime(2022, 6, 3, 4, 37, 36, 248, DateTimeKind.Local).AddTicks(1779), Address = "929 Skiles Forks, Kemmerborough, Venezuela", IsActive = false },
                new Service { Id = 2, ServicePlanId = 3, Name = "", ContractNumber = "suikkxlmnk", InstallationDate = new DateTime(2025, 1, 5, 2, 12, 30, 386, DateTimeKind.Local).AddTicks(9451), Address = "0933 Schowalter Fields, Port Ulices, China", IsActive = false },
                new Service { Id = 3, ServicePlanId = 7, Name = "", ContractNumber = "wl7kkohzmk", InstallationDate = new DateTime(2021, 9, 20, 22, 49, 19, 509, DateTimeKind.Local).AddTicks(6480), Address = "176 Deckow Way, West Carolyneview, China", IsActive = false },
                new Service { Id = 4, ServicePlanId = 8, Name = "", ContractNumber = "2a7e0ugkdp", InstallationDate = new DateTime(2022, 3, 17, 5, 17, 26, 50, DateTimeKind.Local).AddTicks(8348), Address = "28987 Schuppe Avenue, Bernhardchester, Northern Mariana Islands", IsActive = false },
                new Service { Id = 5, ServicePlanId = 6, Name = "", ContractNumber = "ndavtc2a6o", InstallationDate = new DateTime(2022, 2, 21, 11, 44, 20, 653, DateTimeKind.Local).AddTicks(3352), Address = "5150 Hoeger Pines, Charleymouth, Ukraine", IsActive = false },
                new Service { Id = 6, ServicePlanId = 8, Name = "", ContractNumber = "tibbrvi3ts", InstallationDate = new DateTime(2022, 6, 18, 18, 37, 20, 561, DateTimeKind.Local).AddTicks(269), Address = "69075 Miller Mountains, Prudenceland, Netherlands Antilles", IsActive = true },
                new Service { Id = 7, ServicePlanId = 6, Name = "", ContractNumber = "b98kogxjhl", InstallationDate = new DateTime(2025, 1, 1, 16, 7, 8, 818, DateTimeKind.Local).AddTicks(2213), Address = "04113 Alejandra Mountains, Krajcikhaven, Honduras", IsActive = true },
                new Service { Id = 8, ServicePlanId = 5, Name = "", ContractNumber = "vdml3fdyy9", InstallationDate = new DateTime(2022, 7, 1, 11, 35, 39, 432, DateTimeKind.Local).AddTicks(7768), Address = "416 Thelma Trafficway, West Lincoln, Comoros", IsActive = true },
                new Service { Id = 9, ServicePlanId = 7, Name = "", ContractNumber = "0a30fxoalc", InstallationDate = new DateTime(2024, 11, 6, 19, 34, 44, 907, DateTimeKind.Local).AddTicks(2667), Address = "04851 Champlin Rest, New Murlmouth, Syrian Arab Republic", IsActive = false },
                new Service { Id = 10, ServicePlanId = 3, Name = "", ContractNumber = "f236nfhfnj", InstallationDate = new DateTime(2023, 4, 21, 4, 0, 34, 717, DateTimeKind.Local).AddTicks(7800), Address = "910 Vada Alley, West Neldaland, Egypt", IsActive = false },
                new Service { Id = 11, ServicePlanId = 2, Name = "", ContractNumber = "bi804op5qf", InstallationDate = new DateTime(2024, 11, 7, 12, 21, 44, 956, DateTimeKind.Local).AddTicks(5057), Address = "432 Howell Motorway, Binsmouth, French Guiana", IsActive = false },
                new Service { Id = 12, ServicePlanId = 5, Name = "", ContractNumber = "g6gx418v12", InstallationDate = new DateTime(2022, 4, 3, 10, 41, 47, 777, DateTimeKind.Local).AddTicks(8656), Address = "256 Stiedemann Court, Justineland, Cote d'Ivoire", IsActive = false },
                new Service { Id = 13, ServicePlanId = 8, Name = "", ContractNumber = "kvq49sda5q", InstallationDate = new DateTime(2022, 7, 20, 7, 38, 9, 948, DateTimeKind.Local).AddTicks(5622), Address = "8564 Guy Falls, Port Clara, Nauru", IsActive = false },
                new Service { Id = 14, ServicePlanId = 9, Name = "", ContractNumber = "mgly9vm1a6", InstallationDate = new DateTime(2023, 8, 25, 10, 15, 30, 123, DateTimeKind.Local).AddTicks(4567), Address = "123 Example Street, Example City, Example Country", IsActive = true }
            );

            modelBuilder.Entity<Sale>().HasData(
                new Sale { Id = 1, ServiceId = 1, CustomerId = 1, SaleDate = new DateTime(2022, 6, 3, 4, 37, 36, 248, DateTimeKind.Local).AddTicks(1779), SalePrice = 850m, Description = "Sale of internet service" },
                new Sale { Id = 2, ServiceId = 2, CustomerId = 2, SaleDate = new DateTime(2025, 1, 5, 2, 12, 30, 386, DateTimeKind.Local).AddTicks(9451), SalePrice = 1200m, Description = "High-speed internet plan sale" },
                new Sale { Id = 3, ServiceId = 3, CustomerId = 3, SaleDate = new DateTime(2021, 9, 20, 22, 49, 19, 509, DateTimeKind.Local).AddTicks(6480), SalePrice = 400m, Description = "Basic internet service" },
                new Sale { Id = 4, ServiceId = 4, CustomerId = 4, SaleDate = new DateTime(2022, 3, 17, 5, 17, 26, 50, DateTimeKind.Local).AddTicks(8348), SalePrice = 600m, Description = "Internet package deal" },
                new Sale { Id = 5, ServiceId = 5, CustomerId = 5, SaleDate = new DateTime(2022, 2, 21, 11, 44, 20, 653, DateTimeKind.Local).AddTicks(3352), SalePrice = 1000m, Description = "Premium internet service" },
                new Sale { Id = 6, ServiceId = 6, CustomerId = 6, SaleDate = new DateTime(2022, 6, 18, 18, 37, 20, 561, DateTimeKind.Local).AddTicks(269), SalePrice = 600m, Description = "Active service sale" },
                new Sale { Id = 7, ServiceId = 7, CustomerId = 7, SaleDate = new DateTime(2025, 1, 1, 16, 7, 8, 818, DateTimeKind.Local).AddTicks(2213), SalePrice = 1000m, Description = "New year promotion" },
                new Sale { Id = 8, ServiceId = 8, CustomerId = 8, SaleDate = new DateTime(2022, 7, 1, 11, 35, 39, 432, DateTimeKind.Local).AddTicks(7768), SalePrice = 700m, Description = "Summer discount" },
                new Sale { Id = 9, ServiceId = 9, CustomerId = 9, SaleDate = new DateTime(2024, 11, 6, 19, 34, 44, 907, DateTimeKind.Local).AddTicks(2667), SalePrice = 400m, Description = "Late year offer" },
                new Sale { Id = 10, ServiceId = 10, CustomerId = 10, SaleDate = new DateTime(2023, 4, 21, 4, 0, 34, 717, DateTimeKind.Local).AddTicks(7800), SalePrice = 1200m, Description = "Spring sale" }
            );
        }
    }
}
