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
    /// Interaction logic for UserProductView.xaml
    /// </summary>
    public partial class UserProductView : UserControl
    {
        private readonly PetShopDbContext _context = new();

        public UserProductView()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            var products = _context.Products.ToList();

            foreach (var product in products)
            {
                var card = new Border
                {
                    Width = 180,
                    Height = 250,
                    Margin = new Thickness(10),
                    BorderBrush = System.Windows.Media.Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(8),
                    Padding = new Thickness(5)
                };

                var stack = new StackPanel();

                var name = new TextBlock { Text = product.Name, FontWeight = FontWeights.Bold, TextWrapping = TextWrapping.Wrap };
                var price = new TextBlock { Text = $"Giá: {product.Price:N0} đ", Margin = new Thickness(0, 5, 0, 5) };
                var btn = new Button { Content = "Thêm vào giỏ", Tag = product, Background = System.Windows.Media.Brushes.LightGreen };

                btn.Click += (s, e) =>
                {
                    var existing = CartView.CartItems.FirstOrDefault(i => i.Product.ProductId == product.ProductId);
                    if (existing != null)
                        existing.Quantity++;
                    else
                        CartView.CartItems.Add(new CartItem { Product = product, Quantity = 1 });
                    MessageBox.Show($"Đã thêm \"{product.Name}\" vào giỏ!", "Thông báo");
                };

                stack.Children.Add(name);
                stack.Children.Add(price);
                stack.Children.Add(btn);

                card.Child = stack;
                ProductPanel.Children.Add(card);
            }
        }
    }
}
