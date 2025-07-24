using PetShop.Models;
using PetShopConsole.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopConsole.Services
{
    public class AuthService
    {
        public static User Login(PetShopDbContext context)
        {
            Console.Write("Nhập email: ");
            string email = Console.ReadLine();

            Console.Write("Nhập mật khẩu: ");
            string password = Console.ReadLine();

            var user = context.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
            {
                Console.WriteLine("Sai tài khoản hoặc mật khẩu.");
                return null;
            }

            Console.WriteLine($"Đăng nhập thành công. Xin chào {user.FullName}!");
            return user;
        }
    }
}
