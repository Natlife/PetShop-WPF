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
using System.Windows.Shapes;

namespace PetShop.Views
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        private readonly User _currentUser;

        public AdminDashboard(User user)
        {
            InitializeComponent();
            _currentUser = user;
            ShowDashboard();
        }

        public AdminDashboard()
        {
            InitializeComponent();
            ShowDashboard(); 
        }

        private void ShowDashboard()
        {
            MainContent.Content = new AdminDashboardControl(); 
        }

        private void ShowProducts()
        {
            MainContent.Content = new ProductManager();
        }

        private void ShowServices()
        {
            MainContent.Content = new ServiceManager();
        }

        private void ShowOrders()
        {
            MainContent.Content = new OrderManager();
        }

        private void ShowBookings()
        {
            MainContent.Content = new BookingManager();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var userWindow = new LoginView();
            userWindow.Show();
            this.Close();
        }
        private void ShowReport_Click(object sender, RoutedEventArgs e) => ShowDashboard();
        private void ShowProducts_Click(object sender, RoutedEventArgs e) => ShowProducts();
        private void ShowServices_Click(object sender, RoutedEventArgs e) => ShowServices();
        private void ShowOrders_Click(object sender, RoutedEventArgs e) => ShowOrders();
        private void ShowBookings_Click(object sender, RoutedEventArgs e) => ShowBookings();
    }
}
