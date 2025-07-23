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
    /// Interaction logic for UserServiceBookingView.xaml
    /// </summary>
    public partial class UserServiceBookingView : UserControl
    {
        private readonly PetShopDbContext _context = new();
        private readonly User _user;

        public UserServiceBookingView(User user)
        {
            InitializeComponent();
            _user = user;
            LoadServices();
            LoadPets();
        }

        private void LoadServices()
        {
            var services = _context.Services.ToList();
            ServiceListView.ItemsSource = services;
        }

        private void LoadPets()
        {
            var pets = _context.Pets.Where(p => p.UserId == _user.UserId).ToList();
            PetComboBox.ItemsSource = pets;
            PetComboBox.DisplayMemberPath = "Name";
        }

        private void BookNow_Click(object sender, RoutedEventArgs e)
        {
            var selectedService = ServiceListView.SelectedItem as Service;
            var selectedPet = PetComboBox.SelectedItem as Pet;
            var date = DatePicker.SelectedDate;
            var time = TimeBox.Text;

            if (selectedService == null || selectedPet == null || string.IsNullOrWhiteSpace(time) || date == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            if (!TimeSpan.TryParse(time, out TimeSpan parsedTime))
            {
                MessageBox.Show("Thời gian không hợp lệ. Ví dụ: 10:00");
                return;
            }

            var bookingTime = date.Value.Date + parsedTime;

            var booking = new Booking
            {
                UserId = _user.UserId,
                ServiceId = selectedService.ServiceId,
                PetId = selectedPet.PetId,
                BookingTime = bookingTime,
                Notes = NoteBox.Text,
                Status = "Pending"
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            MessageBox.Show("Đặt lịch thành công!");
        }
        private void Book_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = Window.GetWindow(this) as UserHome;
            if (parentWindow != null)
            {
                parentWindow.MainContent.Content = new BookingForm(_user);
            }
        }
    }
}
