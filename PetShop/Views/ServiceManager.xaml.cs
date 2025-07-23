using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PetShop.Views
{
    /// <summary>
    /// Interaction logic for ServiceManager.xaml
    /// </summary>
    public partial class ServiceManager : UserControl
    {
        private readonly PetShopDbContext _context = new();

        public ServiceManager()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            ServiceGrid.ItemsSource = _context.Services.ToList();
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            var window = new ServiceForm();
            if (window.ShowDialog() == true)
            {
                _context.Services.Add(window.Service);
                _context.SaveChanges();
                LoadData();
            }
        }

        private void EditService_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceGrid.SelectedItem is Service selected)
            {
                var window = new ServiceForm(selected);
                if (window.ShowDialog() == true)
                {
                    _context.Services.Update(window.Service);
                    _context.SaveChanges();
                    LoadData();
                }
            }
            else MessageBox.Show("Chọn dịch vụ cần sửa!");
        }

        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceGrid.SelectedItem is Service selected)
            {
                var result = MessageBox.Show($"Xác nhận xoá dịch vụ \"{selected.Name}\"?", "Xoá", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Services.Remove(selected);
                    _context.SaveChanges();
                    LoadData();
                }
            }
            else MessageBox.Show("Chọn dịch vụ cần xoá!");
        }
    }
}
