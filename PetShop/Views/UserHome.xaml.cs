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
        private readonly User _currentUser;
        public UserHome(User user)
        {
            InitializeComponent();
            _currentUser = user;
            MainContent.Content = new ProductList(); // reuse ProductList view
        }

        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ProductList();
        }

        private void BtnService_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ServiceList(currentUser: _currentUser);
        }
    }
}
