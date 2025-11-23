using CommandProjectUniversal.Data;
using CommandProjectUniversal.Models;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CommandProjectUniversal.Views
{
    public partial class EditCustomerWindow : Window
    {
        private readonly AppDbContext _context;
        private Customer _customer;
        private bool _isNew;

        public EditCustomerWindow(AppDbContext context, Customer customer = null)
        {
            InitializeComponent();
            _context = context;
            _customer = customer ?? new Customer();
            _isNew = customer == null;

            // Динамический заголовок
            Title = _isNew ? "Добавить клиента" : "Редактировать клиента";

            LoadData();
        }

        private void LoadData()
        {
            NameTextBox.Text = _customer.Name;
            EmailTextBox.Text = _customer.Email;
            PhoneTextBox.Text = _customer.Phone;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _customer.Name = NameTextBox.Text;
                _customer.Email = EmailTextBox.Text;
                _customer.Phone = PhoneTextBox.Text;

                if (string.IsNullOrWhiteSpace(_customer.Name))
                {
                    MessageBox.Show("Введите имя клиента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_isNew)
                {
                    _context.Customers.Add(_customer);
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
