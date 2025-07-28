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
    /// Interaction logic for AdminDashboardControl.xaml
    /// </summary>
    public partial class AdminDashboardControl : UserControl
    {
        private readonly PetShopDbContext _context = new();

        public AdminDashboardControl()
        {
            InitializeComponent();
            LoadStats();
        }

        private void LoadStats()
        {

            int customerCount = _context.Users.Count(u => u.Role == "Customer");


            int orderCount = _context.Orders.Count();


            float totalRevenue = _context.Orders.Sum(o => o.TotalAmount);


            int pendingBookings = _context.Bookings.Count(b => b.Status == "Pending");


            CustomerCountText.Text = customerCount.ToString();
            OrderCountText.Text = orderCount.ToString();
            TotalRevenueText.Text = $"{totalRevenue:n0} đ";
            PendingBookingText.Text = pendingBookings.ToString();
        }
    }
}
