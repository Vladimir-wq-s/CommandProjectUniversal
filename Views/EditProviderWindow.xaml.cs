using CommandProjectUniversal.Data;
using CommandProjectUniversal.Models;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CommandProjectUniversal.Views
{
    public partial class EditProviderWindow : Window
    {
        private readonly AppDbContext _context;
        private Provider _provider;
        private bool _isNew;

        public EditProviderWindow(AppDbContext context, Provider provider = null)
        {
            InitializeComponent();
            _context = context;
            _provider = provider ?? new Provider();
            _isNew = provider == null;

            // Динамический заголовок
            Title = _isNew ? "Добавить провайдера" : "Редактировать провайдера";

            LoadData();
        }

        private void LoadData()
        {
            NameTextBox.Text = _provider.Name ?? "";
            AddressTextBox.Text = _provider.Address ?? "";
            PhoneTextBox.Text = _provider.Phone ?? "";
            EmailTextBox.Text = _provider.Email ?? "";
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _provider.Name = NameTextBox.Text;
                _provider.Address = AddressTextBox.Text;
                _provider.Phone = PhoneTextBox.Text;
                _provider.Email = EmailTextBox.Text;

                if (string.IsNullOrWhiteSpace(_provider.Name))
                {
                    MessageBox.Show("Введите название провайдера.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_isNew)
                {
                    _context.Providers.Add(_provider);
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
