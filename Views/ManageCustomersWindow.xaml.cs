using CommandProjectUniversal.Data;
using CommandProjectUniversal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CommandProjectUniversal.Views
{
    public partial class ManageCustomersWindow : Window
    {
        private readonly AppDbContext _context;
        private List<Customer> _customers = new();

        public ManageCustomersWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
            LoadData();
        }

        private async void LoadData()
        {
            _customers = await _context.Customers.ToListAsync();
            CustomersGrid.ItemsSource = _customers;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditCustomerWindow(_context);
            if (editWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersGrid.SelectedItem is Customer selectedCustomer)
            {
                var editWindow = new EditCustomerWindow(_context, selectedCustomer);
                if (editWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента для редактирования.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersGrid.SelectedItem is Customer selectedCustomer)
            {
                var result = MessageBox.Show($"Удалить клиента '{selectedCustomer.Name}'?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Customers.Remove(selectedCustomer);
                    await _context.SaveChangesAsync();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
