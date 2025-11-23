using CommandProjectUniversal.Data;
using CommandProjectUniversal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CommandProjectUniversal.Views
{
    public partial class EditSaleWindow : Window
    {
        private readonly AppDbContext _context;
        private Sale _sale;
        private bool _isNew;

        public EditSaleWindow(AppDbContext context, Sale sale = null)
        {
            InitializeComponent();
            _context = context;
            _sale = sale ?? new Sale();
            _isNew = sale == null;

            // Динамический заголовок
            Title = _isNew ? "Добавить продажу" : "Редактировать продажу";

            LoadData();
        }

        private async void LoadData()
        {
            var customers = await _context.Customers.ToListAsync();
            CustomerComboBox.ItemsSource = customers;
            if (_sale.CustomerId != 0 && customers.Any(c => c.Id == _sale.CustomerId))
            {
                CustomerComboBox.SelectedValue = _sale.CustomerId;
            }

            var services = await _context.Services.Include(s => s.ServicePlan).ToListAsync();
            ServiceComboBox.ItemsSource = services;
            if (_sale.ServiceId != 0 && services.Any(s => s.Id == _sale.ServiceId))
            {
                ServiceComboBox.SelectedValue = _sale.ServiceId;
            }

            SaleDatePicker.SelectedDate = _sale.SaleDate;
            PriceTextBox.Text = _sale.SalePrice.ToString();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SaleDatePicker.SelectedDate == null)
                {
                    MessageBox.Show("Выберите дату продажи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _sale.SaleDate = SaleDatePicker.SelectedDate.Value;
                _sale.CustomerId = (int)CustomerComboBox.SelectedValue;
                _sale.ServiceId = (int)ServiceComboBox.SelectedValue;

                if (!decimal.TryParse(PriceTextBox.Text, out decimal price) || price <= 0)
                {
                    MessageBox.Show("Введите корректную цену.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                _sale.SalePrice = price;

                if (CustomerComboBox.SelectedValue == null || ServiceComboBox.SelectedValue == null)
                {
                    MessageBox.Show("Выберите клиента и услугу.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_isNew)
                {
                    _context.Sales.Add(_sale);
                }
                await _context.SaveChangesAsync();

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
