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
    /// Interaction logic for OrderManager.xaml
    /// </summary>
    public partial class OrderManager : UserControl
    {
        private readonly PetShopDbContext _context = new();

        public OrderManager()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            OrderGrid.ItemsSource = _context.Orders
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => new
                {
                    o.OrderId,
                    o.TotalAmount,
                    o.CreatedAt,
                    User = o.User
                }).ToList();
        }

        private void OrderGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedOrder = OrderGrid.SelectedItem;
            if (selectedOrder != null)
            {
                var orderIdProperty = selectedOrder.GetType().GetProperty("OrderId");
                if (orderIdProperty != null)
                {
                    int orderId = (int)orderIdProperty.GetValue(selectedOrder);

                    var details = _context.OrderDetails
                        .Where(d => d.OrderId == orderId)
                        .Select(d => new
                        {
                            d.Product,
                            d.Quantity,
                            d.UnitPrice,
                            Total = d.Quantity * d.UnitPrice
                        }).ToList();

                    DetailGrid.ItemsSource = details;
                }
            }
            else
            {
                DetailGrid.ItemsSource = null;
            }
        }
    }
}
