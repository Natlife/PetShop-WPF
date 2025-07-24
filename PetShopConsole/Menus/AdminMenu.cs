using PetShop.Models;
using PetShopConsole.Data;
using PetShopConsole.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace PetShopConsole.Menus
{
    public class AdminMenu
    {
        public static void Show(PetShopDbContext context, User user)
        {
            while (true)
            {
                Console.WriteLine("\n=== MENU ADMIN ===");
                Console.WriteLine("1. Xem danh sách sản phẩm");
                Console.WriteLine("2. Xem danh sách dịch vụ");
                Console.WriteLine("3. Xem tất cả lịch đặt");
                Console.WriteLine("0. Thoát");

                Console.Write("Chọn: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ProductService.ShowProducts(context);
                        break;
                    case "2":
                        ServiceService.ShowServices(context);
                        break;
                    case "3":
                        Common.ShowAllBookings(context);
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
