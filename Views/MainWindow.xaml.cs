using CommandProjectUniversal.Data;
using CommandProjectUniversal.Models;
using Microsoft.EntityFrameworkCore;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CommandProjectUniversal.Views
{
    public partial class MainWindow : Window
    {
        private readonly AppDbContext _context;
        private readonly DbContextOptions<AppDbContext> _options; 
        private List<Service> _allServices = new List<Service>();
        private List<Service> _filteredServices = new List<Service>();
        private PlotModel _plotModel;

        public MainWindow()
        {
            InitializeComponent();

         
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=internet_services.db");
            _options = optionsBuilder.Options;

         
            _context = new AppDbContext(_options);


            _plotModel = new PlotModel { Title = "Сумма продаж по провайдерам" };
            PlotView.Model = _plotModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadServices();
        }

        private async Task LoadServices()
        {
            try
            {
                _allServices = await _context.Services
                    .Include(s => s.ServicePlan)
                        .ThenInclude(sp => sp.Provider)
                            .ThenInclude(p => p.Country)
                    .Include(s => s.Sale)
                        .ThenInclude(sale => sale.Customer)
                    .ToListAsync();
                _filteredServices = new List<Service>(_allServices);
                ServicesGrid.ItemsSource = _filteredServices;
                StatusTextBlock.Text = $"Загружено {_allServices.Count} услуг.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(MinPriceTextBox.Text, out decimal minPrice) &&
                decimal.TryParse(MaxPriceTextBox.Text, out decimal maxPrice))
            {
                _filteredServices = _allServices.Where(s => s.ServicePlan != null && s.ServicePlan.PricePerMonth >= minPrice && s.ServicePlan.PricePerMonth <= maxPrice).ToList();
                ServicesGrid.ItemsSource = _filteredServices;
                StatusTextBlock.Text = $"Отфильтровано {_filteredServices.Count} услуг.";
            }
            else
            {
                MessageBox.Show("Введите корректные значения цен.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            
            ServicesGrid.Visibility = Visibility.Visible;
            PlotView.Visibility = Visibility.Collapsed;
            MinPriceTextBox.Text = string.Empty;
            MaxPriceTextBox.Text = string.Empty;
            _filteredServices = new List<Service>(_allServices);
            ServicesGrid.ItemsSource = _filteredServices;
            StatusTextBlock.Text = $"Загружено {_allServices.Count} услуг.";
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditServiceWindow(_context);
            if (editWindow.ShowDialog() == true)
            {
                await LoadServices();
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesGrid.SelectedItem is Service selectedService)
            {
                var editWindow = new EditServiceWindow(_context, selectedService);
                if (editWindow.ShowDialog() == true)
                {
                    await LoadServices();
                }
            }
            else
            {
                MessageBox.Show("Выберите услугу для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesGrid.SelectedItem is Service selectedService)
            {
                var result = MessageBox.Show("Удалить выбранную услугу?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Services.Remove(selectedService);
                        await _context.SaveChangesAsync();
                        await LoadServices();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите услугу для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                
                var salesData = _context.Sales.Include(s => s.Service).ThenInclude(s => s.ServicePlan).ThenInclude(sp => sp.Provider)
                    .AsEnumerable()  // Переключаемся на клиентскую обработку
                    .GroupBy(s => s.Service.ServicePlan.Provider.Name)
                    .Select(g => new { Provider = g.Key, TotalSales = g.Sum(s => (double)s.SalePrice) })  
                    .ToList();

                _plotModel.Series.Clear();
                _plotModel.Axes.Clear();  

                
                var series = new PieSeries { Title = "Сумма продаж по провайдерам" };
                foreach (var data in salesData)
                {
                    
                    series.Slices.Add(new PieSlice($"{data.Provider}: {data.TotalSales:F2}", data.TotalSales));
                }
                
                _plotModel.Series.Add(series);

                _plotModel.InvalidatePlot(true);

                ServicesGrid.Visibility = Visibility.Collapsed;
                PlotView.Visibility = Visibility.Visible;
                StatusTextBlock.Text = "Отчёт сгенерирован.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка генерации отчёта: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчики меню для справочников
        private void ManageCountriesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var manageWindow = new ManageCountriesWindow(_context);
            manageWindow.ShowDialog();
        }

        private void ManageProvidersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var manageWindow = new ManageProvidersWindow(_context);
            manageWindow.ShowDialog();
        }

        private void ManageServicePlansMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var manageWindow = new ManageServicePlansWindow(_context);
            manageWindow.ShowDialog();
        }

        private void ManageCustomersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var manageWindow = new ManageCustomersWindow(_context);
            manageWindow.ShowDialog();
        }

        private void ManageSalesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var manageWindow = new ManageSalesWindow(_context);
            manageWindow.ShowDialog();
        }
    }
}
