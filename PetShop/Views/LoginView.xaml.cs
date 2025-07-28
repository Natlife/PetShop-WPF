using Microsoft.VisualBasic.ApplicationServices;
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
                    var adminWindow = new AdminDashboard();
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
        private void ShowRegister_Click(object sender, RoutedEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Collapsed;
            RegisterPanel.Visibility = Visibility.Visible;
        }

        private void ShowLogin_Click(object sender, RoutedEventArgs e)
        {
            RegisterPanel.Visibility = Visibility.Collapsed;
            LoginPanel.Visibility = Visibility.Visible;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameBox.Text;
            string email = RegisterEmailBox.Text;
            string password = RegisterPasswordBox.Password;
            var newUser = new Models.User
            {
                FullName = fullName,
                Email = email,
                Password = password,
                Role = "Customer"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();


            MessageBox.Show("Đăng ký thành công!");
            var userWindow = new UserHome(newUser);
            userWindow.Show();
            this.Close();
        }
    }
}
