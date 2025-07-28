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
    /// Interaction logic for ProductForm.xaml
    /// </summary>
    public partial class ProductForm : UserControl
    {
        public Product Product { get; private set; }
        private readonly bool _isEdit;
        public Action<Product>? OnSave;
        public Action? OnCancel;

        public ProductForm()
        {
            InitializeComponent();
            Product = new Product();
            _isEdit = false;
        }

        public ProductForm(Product existing) : this()
        {
            Product = existing;
            _isEdit = true;


            NameBox.Text = Product.Name;
            DescriptionBox.Text = Product.Description;
            PriceBox.Text = Product.Price.ToString();
            StockBox.Text = Product.Stock.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(NameBox.Text) || !float.TryParse(PriceBox.Text, out var price) || !int.TryParse(StockBox.Text, out var stock))
            {
                MessageBox.Show("Vui lòng nhập đúng thông tin!");
                return;
            }

            Product.Name = NameBox.Text.Trim();
            Product.Description = DescriptionBox.Text.Trim();
            Product.Price = price;
            Product.Stock = stock;
            OnSave?.Invoke(Product);
            productForm.Visibility = Visibility.Collapsed;

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            OnSave?.Invoke(Product);
            productForm.Visibility = Visibility.Collapsed;
        }
    }
}
