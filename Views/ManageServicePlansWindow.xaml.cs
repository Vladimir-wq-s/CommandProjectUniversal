using CommandProjectUniversal.Data;
using CommandProjectUniversal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CommandProjectUniversal.Views
{
    public partial class ManageServicePlansWindow : Window
    {
        private readonly AppDbContext _context;
        private List<ServicePlan> _servicePlans;

        public ManageServicePlansWindow(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
            LoadData();
        }

        private async void LoadData()
        {
            _servicePlans = await _context.ServicePlans.Include(sp => sp.Provider).Include(sp => sp.Country).ToListAsync();
            ServicePlansGrid.ItemsSource = _servicePlans;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditServicePlanWindow(_context);
            if (editWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServicePlansGrid.SelectedItem is ServicePlan selectedServicePlan)
            {
                var editWindow = new EditServicePlanWindow(_context, selectedServicePlan);
                if (editWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите план услуг для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ServicePlansGrid.SelectedItem is ServicePlan selectedServicePlan)
            {
                var result = MessageBox.Show("Удалить выбранный план услуг?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _context.ServicePlans.Remove(selectedServicePlan);
                    await _context.SaveChangesAsync();
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Выберите план услуг для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
