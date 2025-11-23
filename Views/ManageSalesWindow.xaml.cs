using CommandProjectUniversal.Data;
using CommandProjectUniversal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CommandProjectUniversal.Views
{
    public partial class ManageSalesWindow : Window
    {
        private readonly AppDbContext _context;
        private List<Sale> _sales = new();

        public ManageSalesWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
            LoadData();
        }

        private async void LoadData()
        {
            _sales = await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Service)
                    .ThenInclude(s => s.ServicePlan)
                .ToListAsync();
            SalesGrid.ItemsSource = _sales;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditSaleWindow(_context);
            if (editWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SalesGrid.SelectedItem is Sale selectedSale)
            {
                var editWindow = new EditSaleWindow(_context, selectedSale);
                if (editWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите продажу для редактирования.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SalesGrid.SelectedItem is Sale selectedSale)
            {
                var result = MessageBox.Show($"Удалить продажу от {selectedSale.SaleDate.ToShortDateString()}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Sales.Remove(selectedSale);
                    await _context.SaveChangesAsync();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите продажу для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
