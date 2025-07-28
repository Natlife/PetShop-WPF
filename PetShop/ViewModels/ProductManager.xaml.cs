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
    /// Interaction logic for ProductManager.xaml
    /// </summary>
    public partial class ProductManager : UserControl
    {
        private readonly PetShopDbContext _context = new();

        public ProductManager()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            ProductGrid.ItemsSource = _context.Products.ToList();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var window = new ProductForm();
            if (window.ShowDialog() == true)
            {
                _context.Products.Add(window.Product);
                _context.SaveChanges();
                LoadData();
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductGrid.SelectedItem is Product selected)
            {
                var window = new ProductForm(selected);
                if (window.ShowDialog() == true)
                {
                    _context.Products.Update(window.Product);
                    _context.SaveChanges();
                    LoadData();
                }
            }
            else MessageBox.Show("Chọn sản phẩm cần sửa!");
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductGrid.SelectedItem is Product selected)
            {
                var result = MessageBox.Show($"Xác nhận xoá sản phẩm \"{selected.Name}\"?", "Xoá", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _context.Products.Remove(selected);
                    _context.SaveChanges();
                    LoadData();
                }
            }
            else MessageBox.Show("Chọn sản phẩm cần xoá!");
        }
    }
}
