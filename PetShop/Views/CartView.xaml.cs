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
    /// Interaction logic for CartView.xaml
    /// </summary>
    public partial class CartView : UserControl
    {
        private readonly PetShopDbContext _context = new();
        private readonly User _user;

        public static List<CartItem> CartItems = new();

        public CartView(User user)
        {
            InitializeComponent();
            _user = user;
            LoadCart();
        }

        private void LoadCart()
        {
            CartGrid.ItemsSource = null;
            CartGrid.ItemsSource = CartItems;
            TotalText.Text = $"{CartItems.Sum(i => i.Total):N0} đ";
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            if (!CartItems.Any())
            {
                MessageBox.Show("Giỏ hàng trống!");
                return;
            }

            var order = new Order
            {
                UserId = _user.UserId,
                TotalAmount = CartItems.Sum(i => i.Total),
                CreatedAt = DateTime.Now
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var item in CartItems)
            {
                _context.OrderDetails.Add(new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = item.Product.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                });

                // Trừ tồn kho
                var product = _context.Products.Find(item.Product.ProductId);
                if (product != null) product.Stock -= item.Quantity;
            }

            _context.SaveChanges();
            CartItems.Clear();
            LoadCart();

            MessageBox.Show("Đặt hàng thành công!");
        }
    }
}
