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
    public partial class ManageProvidersWindow : Window
    {
        private readonly AppDbContext _context;
        private List<Provider> _providers = new();

        public ManageProvidersWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
            LoadData();
        }

        private async void LoadData()
        {
            _providers = await _context.Providers.Include(p => p.Country).ToListAsync();
            ProvidersGrid.ItemsSource = _providers;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditProviderWindow(_context);
            if (editWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProvidersGrid.SelectedItem is Provider selectedProvider)
            {
                var editWindow = new EditProviderWindow(_context, selectedProvider);
                if (editWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите провайдера для редактирования.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProvidersGrid.SelectedItem is Provider selectedProvider)
            {
                var result = MessageBox.Show($"Удалить провайдера '{selectedProvider.Name}'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Providers.Remove(selectedProvider);
                    await _context.SaveChangesAsync();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите провайдера для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
