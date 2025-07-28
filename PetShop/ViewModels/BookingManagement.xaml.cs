using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for BookingManagement.xaml
    /// </summary>
    public partial class BookingManagement : UserControl
    {
        private readonly PetShopDbContext _context = new();     

        public BookingManagement()
        {
            InitializeComponent();
            LoadBookings();
        }

        private void LoadBookings()
        {
            var bookings = _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Service)
                .Include(b => b.Pet)
                .OrderByDescending(b => b.BookingTime)
                .ToList();

            BookingGrid.ItemsSource = bookings;
        }
    }
}
