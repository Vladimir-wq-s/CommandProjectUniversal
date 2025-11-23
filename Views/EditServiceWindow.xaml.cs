using CommandProjectUniversal.Data;
using CommandProjectUniversal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CommandProjectUniversal.Views
{
    public partial class EditServiceWindow : Window
    {
        private readonly AppDbContext _context;
        private Service _service;
        private bool _isNew;

        public EditServiceWindow(AppDbContext context, Service service = null)
        {
            InitializeComponent();
            _context = context;
            _service = service ?? new Service();
            _isNew = service == null;

            // Динамический заголовок
            Title = _isNew ? "Добавить услугу" : "Редактировать услугу";

            LoadData();
        }

        private async void LoadData()
        {
            var servicePlans = await _context.ServicePlans.ToListAsync();
            ServicePlanComboBox.ItemsSource = servicePlans;
            if (_service.ServicePlanId != 0 && servicePlans.Any(sp => sp.Id == _service.ServicePlanId))
            {
                ServicePlanComboBox.SelectedValue = _service.ServicePlanId;
            }

            NameTextBox.Text = _service.Name ?? "";
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _service.Name = NameTextBox.Text;
                _service.ServicePlanId = (int)ServicePlanComboBox.SelectedValue;

                if (string.IsNullOrWhiteSpace(_service.Name))
                {
                    MessageBox.Show("Введите название услуги.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (ServicePlanComboBox.SelectedValue == null)
                {
                    MessageBox.Show("Выберите план услуги.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_isNew)
                {
                    _context.Services.Add(_service);
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
