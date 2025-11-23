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
    public partial class ManageCountriesWindow : Window
    {
        private readonly AppDbContext _context;
        private List<Country> _countries = new();

        public ManageCountriesWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
            LoadData();
        }

        private async void LoadData()
        {
            _countries = await _context.Countries.ToListAsync();
            CountriesGrid.ItemsSource = _countries;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditCountryWindow(_context);
            if (editWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (CountriesGrid.SelectedItem is Country selectedCountry)
            {
                var editWindow = new EditCountryWindow(_context, selectedCountry);
                if (editWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите страну для редактирования.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CountriesGrid.SelectedItem is Country selectedCountry)
            {
                var result = MessageBox.Show($"Удалить страну '{selectedCountry.Name}'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Countries.Remove(selectedCountry);
                    await _context.SaveChangesAsync();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите страну для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
