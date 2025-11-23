using CommandProjectUniversal.Data;
using CommandProjectUniversal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CommandProjectUniversal.Views
{
    public partial class EditServicePlanWindow : Window
    {
        private readonly AppDbContext _context;
        private ServicePlan _servicePlan;
        private bool _isNew;

        public EditServicePlanWindow(AppDbContext context, ServicePlan servicePlan = null)
        {
            InitializeComponent();
            _context = context;
            _servicePlan = servicePlan ?? new ServicePlan();
            _isNew = servicePlan == null;

            // Динамический заголовок
            Title = _isNew ? "Добавить план услуги" : "Редактировать план услуги";

            LoadData();
        }

        private async void LoadData()
        {
            var providers = await _context.Providers.ToListAsync();
            ProviderComboBox.ItemsSource = providers;
            if (_servicePlan.ProviderId != 0 && providers.Any(p => p.Id == _servicePlan.ProviderId))
            {
                ProviderComboBox.SelectedValue = _servicePlan.ProviderId;
            }

            var countries = await _context.Countries.ToListAsync();
            CountryComboBox.ItemsSource = countries;
            if (_servicePlan.CountryId != 0 && countries.Any(c => c.Id == _servicePlan.CountryId))
            {
                CountryComboBox.SelectedValue = _servicePlan.CountryId;
            }

            NameTextBox.Text = _servicePlan.Name;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _servicePlan.Name = NameTextBox.Text;
                _servicePlan.ProviderId = (int)ProviderComboBox.SelectedValue;
                _servicePlan.CountryId = (int)CountryComboBox.SelectedValue;

                if (string.IsNullOrWhiteSpace(_servicePlan.Name))
                {
                    MessageBox.Show("Введите название плана услуги.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (ProviderComboBox.SelectedValue == null || CountryComboBox.SelectedValue == null)
                {
                    MessageBox.Show("Выберите провайдера и страну.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_isNew)
                {
                    _context.ServicePlans.Add(_servicePlan);
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
