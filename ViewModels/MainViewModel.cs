using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommandProjectUniversal.Data;
using CommandProjectUniversal.Models;

namespace CommandProjectUniversal.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context;
        public ObservableCollection<Service> Services { get; set; } = new();

        public MainViewModel(AppDbContext context)
        {
            _context = context;
            LoadData();
        }

        private async void LoadData()
        {
            var services = await _context.Services
                .Include(s => s.ServicePlan)
                    .ThenInclude(sp => sp.Provider)
                        .ThenInclude(p => p.Country)
                .Include(s => s.Sale)
                    .ThenInclude(sale => sale.Customer)
                .ToListAsync();
            Services = new ObservableCollection<Service>(services);
            OnPropertyChanged(nameof(Services));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
