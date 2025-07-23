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
    /// Interaction logic for PetList.xaml
    /// </summary>
    public partial class PetList : UserControl
    {
        private readonly PetShopDbContext _context = new();
        private readonly User _user;

        public PetList(User user)
        {
            InitializeComponent();
            _user = user;
            LoadPets();
        }

        private void LoadPets()
        {
            PetGrid.ItemsSource = _context.Pets.Where(p => p.UserId == _user.UserId).ToList();
        }

        private void AddPet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NameBox.Text;
                string species = SpeciesBox.Text;
                string breed = BreedBox.Text;
                string color = ColorBox.Text;
                string gender = (GenderBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                double weight = double.TryParse(WeightBox.Text, out var w) ? w : 0;

                var newPet = new Pet
                {
                    UserId = _user.UserId,
                    Name = name,
                    Species = species,
                    Breed = breed,
                    Gender = gender,
                    Color = color,
                    Weight = weight,
                    CreatedAt = DateTime.Now
                };

                _context.Pets.Add(newPet);
                _context.SaveChanges();
                MessageBox.Show("Đã thêm thú cưng!");

                LoadPets();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm thú cưng: " + ex.Message);
            }
        }
    }
}
