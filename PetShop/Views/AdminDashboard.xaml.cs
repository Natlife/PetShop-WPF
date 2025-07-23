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
        public AdminDashboard(User currentUser)
        {
            InitializeComponent();
            MainContent.Content = new ProductList();
        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ProductList();
        }

        private void BtnService_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ServiceList();
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new OrderManagement();
        }

        private void BtnBooking_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new BookingManagement();
        }
    }
}
