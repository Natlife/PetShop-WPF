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
            var form = new ProductForm();
            form.OnSave = (newProduct) =>
            {
                if(newProduct.Name is null)
                {
                    LoadData();
                    MainContent.Content = null;
                    
                }
                else
                {
                    _context.Products.Add(newProduct);
                    _context.SaveChanges();
                    LoadData();
                    MainContent.Content = null;
                }
            };
            form.OnCancel = () => MainContent.Content = null;

            MainContent.Content = form;

        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductGrid.SelectedItem is Product selected)
            {
                var form = new ProductForm(new Product
                {
                    ProductId = selected.ProductId,
                    Name = selected.Name,
                    Description = selected.Description,
                    Price = selected.Price,
                    Stock = selected.Stock
                });

                form.OnSave = (updatedProduct) =>
                {
                    selected.Name = updatedProduct.Name;
                    selected.Description = updatedProduct.Description;
                    selected.Price = updatedProduct.Price;
                    selected.Stock = updatedProduct.Stock;

                    _context.Products.Update(selected);
                    _context.SaveChanges();
                    LoadData();
                    MainContent.Content = null;
                };

                form.OnCancel = () => MainContent.Content = null;

                MainContent.Content = form;
            }
            else MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!");
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
