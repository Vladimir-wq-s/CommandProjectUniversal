using CommandProjectUniversal.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace CommandProjectUniversal
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("Data Source=internet_services.db")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Database.Migrate();
            }
        }
    }
}
