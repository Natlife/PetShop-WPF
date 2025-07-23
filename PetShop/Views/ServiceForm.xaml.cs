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
    /// Interaction logic for ServiceForm.xaml
    /// </summary>
    public partial class ServiceForm : Window
    {
        public Service Service { get; private set; }
        private readonly bool _isEdit;

        public ServiceForm()
        {
            InitializeComponent();
            Service = new Service();
            _isEdit = false;
        }

        public ServiceForm(Service existing) : this()
        {
            Service = existing;
            _isEdit = true;

            NameBox.Text = Service.Name;
            DescriptionBox.Text = Service.Description;
            PriceBox.Text = Service.Price.ToString();
            DurationBox.Text = Service.Duration.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text) ||
                !float.TryParse(PriceBox.Text, out float price) ||
                !int.TryParse(DurationBox.Text, out int duration))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ & đúng định dạng!");
                return;
            }

            Service.Name = NameBox.Text.Trim();
            Service.Description = DescriptionBox.Text.Trim();
            Service.Price = price;
            Service.Duration = duration;

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
