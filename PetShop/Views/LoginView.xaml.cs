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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly PetShopDbContext _context = new();

        public LoginView()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var email = EmailBox.Text.Trim();
            var password = PasswordBox.Password;

            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                MessageBox.Show($"Xin chào {user.FullName} ({user.Role})");

                if (user.Role == "Admin")
                {
                    var adminWindow = new AdminDashboard(user);
                    adminWindow.Show();
                }
                else
                {
                    var userWindow = new UserHome(user);
                    userWindow.Show();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Sai email hoặc mật khẩu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
