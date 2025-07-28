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
    /// Interaction logic for UserInvoiceView.xaml
    /// </summary>
    public partial class UserInvoiceView : UserControl
    {
        private readonly PetShopDbContext _context = new();
        private readonly User _user;

        public UserInvoiceView(User user)
        {
            InitializeComponent();
            _user = user;
            LoadInvoices();
        }

        private void LoadInvoices()
        {
            var invoices = _context.Invoices.Where(i => i.UserId == _user.UserId).ToList();
            InvoiceGrid.ItemsSource = invoices;
        }
    }
}
