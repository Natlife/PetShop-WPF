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
    /// Interaction logic for ServiceList.xaml
    /// </summary>
    public partial class ServiceList : Window
    {
        private readonly PetShopDbContext _context = new();

        public ServiceList()
        {
            InitializeComponent();
            ServiceGrid.ItemsSource = _context.Services.ToList();
        }
    }
}
