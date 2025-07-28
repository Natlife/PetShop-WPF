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
    /// Interaction logic for BookingManager.xaml
    /// </summary>
    public partial class BookingManager : UserControl
    {
        private readonly PetShopDbContext _context = new();

        public BookingManager()
        {
            InitializeComponent();
            LoadBookings();
        }

        private void LoadBookings()
        {
            BookingGrid.ItemsSource = _context.Bookings
                .OrderByDescending(b => b.BookingTime)
                .Select(b => new
                {
                    b.BookingId,
                    b.BookingTime,
                    b.Status,
                    User = b.User,
                    Pet = b.Pet,
                    Service = b.Service
                }).ToList();
        }

        private void UpdateStatus(dynamic bookingItem, string newStatus)
        {
            if (bookingItem == null) return;

            int bookingId = bookingItem.BookingId;
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking != null)
            {
                booking.Status = newStatus;
                _context.SaveChanges();
                LoadBookings();
            }
        }

        private void ApproveRow_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag != null)
            {
                var bookingItem = btn.Tag;
                UpdateStatus(bookingItem, "Approved");
            }
        }

        private void CompleteRow_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag != null)
            {
                var bookingItem = btn.Tag;
                UpdateStatus(bookingItem, "Completed");
            }
        }

        private void CancelRow_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag != null)
            {
                var bookingItem = btn.Tag;
                var result = MessageBox.Show("Bạn có chắc muốn huỷ lịch này?", "Xác nhận", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    UpdateStatus(bookingItem, "Cancelled");
                }
            }
        }
    }
}
