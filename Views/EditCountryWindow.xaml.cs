using CommandProjectUniversal.Data;
using CommandProjectUniversal.Models;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CommandProjectUniversal.Views
{
    public partial class EditCountryWindow : Window
    {
        private readonly AppDbContext _context;
        private Country _country;
        private bool _isNew;

        public EditCountryWindow(AppDbContext context, Country country = null)
        {
            InitializeComponent();
            _context = context;
            _country = country ?? new Country();
            _isNew = country == null;

            // Динамический заголовок
            Title = _isNew ? "Добавить страну" : "Редактировать страну";

            LoadData();
        }

        private void LoadData()
        {
            NameTextBox.Text = _country.Name ?? "";
            CodeTextBox.Text = _country.Code ?? "";
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _country.Name = NameTextBox.Text;
                _country.Code = CodeTextBox.Text;

                if (string.IsNullOrWhiteSpace(_country.Name))
                {
                    MessageBox.Show("Введите название страны.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(_country.Code))
                {
                    MessageBox.Show("Введите код страны.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_isNew)
                {
                    _context.Countries.Add(_country);
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
