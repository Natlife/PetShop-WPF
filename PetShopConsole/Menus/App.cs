using PetShop.Models;
using PetShopConsole.Data;
using PetShopConsole.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopConsole.Menus
{
    public  class App
    {
        public static void Run()
        {
            var context = new PetShopDbContext();
            User currentUser = null;

            while (currentUser == null)
            {
                Console.WriteLine("=== ĐĂNG NHẬP ===");
                currentUser = AuthService.Login(context);
            }

            if (currentUser.Role == "Admin")
            {
                AdminMenu.Show(context, currentUser);
            }
            else
            {
                UserMenu.Show(context, currentUser);
            }
        }
    }
}
