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
    /// Interaction logic for BookingForm.xaml
    /// </summary>
    public partial class BookingForm : UserControl
    {
        private readonly PetShopDbContext _context = new();
        private readonly User _currentUser;

        public BookingForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            LoadData();
        }

        private void LoadData()
        {
            PetComboBox.ItemsSource = _context.Pets.Where(p => p.User.UserId == _currentUser.UserId).ToList();
            ServiceComboBox.ItemsSource = _context.Services.ToList();
        }

        private void Book_Click(object sender, RoutedEventArgs e)
        {
            if (PetComboBox.SelectedValue == null || ServiceComboBox.SelectedValue == null || DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thú cưng, dịch vụ và thời gian.");
                return;
            }

            if (!TimeSpan.TryParse(TimeTextBox.Text, out TimeSpan time))
            {
                MessageBox.Show("Giờ không hợp lệ. Nhập theo định dạng HH:mm (ví dụ: 14:30)");
                return;
            }

            var booking = new Booking
            {
                UserId = _currentUser.UserId, // Corrected property name
                PetId = (int)PetComboBox.SelectedValue,
                ServiceId = (int)ServiceComboBox.SelectedValue,
                BookingTime = DatePicker.SelectedDate.Value.Date + time,
                Notes = NotesBox.Text,
                Status = "Pending"
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            MessageBox.Show("Đặt lịch thành công! Vui lòng chờ xác nhận.");
        }
    }
}
