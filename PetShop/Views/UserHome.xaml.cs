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
    /// Interaction logic for UserHome.xaml
    /// </summary>
    public partial class UserHome : Window
    {
        private readonly User _user;

        public UserHome(User user)
        {
            InitializeComponent();
            _user = user;
            ShowProduct();
        }

        private void ShowProduct()
        {
            MainContent.Content = new UserProductView();
        }

        private void ShowService()
        {
            MainContent.Content = new UserServiceBookingView(_user);
        }

        private void ShowPets()
        {
            MainContent.Content = new PetList(_user);
        }

        private void ShowCart()
        {
            MainContent.Content = new CartView(_user);
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ShowInvoices()
        {
            MainContent.Content = new UserInvoiceView(_user);
        }
        private void ShowInvoices_Click(object sender, RoutedEventArgs e) => ShowInvoices();
        private void ShowProduct_Click(object sender, RoutedEventArgs e) => ShowProduct();
        private void ShowService_Click(object sender, RoutedEventArgs e) => ShowService();
        private void ShowPets_Click(object sender, RoutedEventArgs e) => ShowPets();
        private void ShowCart_Click(object sender, RoutedEventArgs e) => ShowCart();

    }
}
