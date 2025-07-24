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
    public class UserMenu
    {
        public static void Show(PetShopDbContext context, User user)
        {
            while (true)
            {
                Console.WriteLine("\n=== MENU NGƯỜI DÙNG ===");
                Console.WriteLine("1. Xem sản phẩm");
                Console.WriteLine("2. Mua sản phẩm");
                Console.WriteLine("3. Xem dịch vụ");
                Console.WriteLine("4. Đặt lịch dịch vụ");
                Console.WriteLine("5. Xem thú cưng");
                Console.WriteLine("6. Thêm thú cưng");
                Console.WriteLine("0. Thoát");

                Console.Write("Chọn: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ProductService.ShowProducts(context);
                        break;
                    case "2":
                        ProductService.BuyProduct(user, context);
                        break;
                    case "3":
                        ServiceService.ShowServices(context);
                        break;
                    case "4":
                        ServiceService.BookService(user, context);
                        break;
                    case "5":
                        PetService.ShowPets(user, context);
                        break;
                    case "6":
                        PetService.AddPet(user, context);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }
    }
}
