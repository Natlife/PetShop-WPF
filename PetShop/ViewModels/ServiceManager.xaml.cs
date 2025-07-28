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
using System.Windows.Forms;
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
    public partial class ServiceManager : System.Windows.Controls.UserControl
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
            var form = new ServiceForm();
            form.OnSave = (newService) =>
            {
                if (newService.Name is null)
                {
                    LoadData();
                    MainContent.Content = null;

                }
                else
                {
                    _context.Services.Add(newService);
                    _context.SaveChanges();
                    LoadData();
                    MainContent.Content = null;
                }
            };
            form.OnCancel = () => MainContent.Content = null;

            MainContent.Content = form;
        }

        private void EditService_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceGrid.SelectedItem is Service selected)
            {
                var form = new ServiceForm(new Service
                {
                    ServiceId = selected.ServiceId,
                    Name = selected.Name,
                    Description = selected.Description,
                    Price = selected.Price,
                    Duration = selected.Duration,

                });

                form.OnSave = (updatedService) =>
                {
                    selected.Name = updatedService.Name;
                    selected.Description = updatedService.Description;
                    selected.Price = updatedService.Price;
                    selected.Duration = updatedService.Duration;

                    _context.Services.Update(selected);
                    _context.SaveChanges();
                    LoadData();
                    MainContent.Content = null;
                };

                form.OnCancel = () => MainContent.Content = null;

                MainContent.Content = form;
            }
            else System.Windows.MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!");
        }

        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceGrid.SelectedItem is Service selected)
            {
                var result = System.Windows.MessageBox.Show($"Xác nhận xoá dịch vụ \"{selected.Name}\"?", "Xoá", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Services.Remove(selected);
                    _context.SaveChanges();
                    LoadData();
                }
            }
            else System.Windows.MessageBox.Show("Chọn dịch vụ cần xoá!");
        }
    }
}
