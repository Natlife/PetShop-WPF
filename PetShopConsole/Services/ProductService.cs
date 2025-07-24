using PetShop.Models;
using PetShopConsole.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopConsole.Services
{
    public class ProductService
    {
        public static void ShowProducts(PetShopDbContext context)
        {
            Console.WriteLine("=== DANH SÁCH SẢN PHẨM ===");
            foreach (var p in context.Products)
            {
                Console.WriteLine($"[{p.ProductId}] {p.Name} - {p.Price}đ (Tồn kho: {p.Stock})");
            }
        }

        public static void BuyProduct(User user, PetShopDbContext context)
        {
            ShowProducts(context);

            Console.Write("Nhập ID sản phẩm muốn mua: ");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("ID không hợp lệ.");
                return;
            }

            var product = context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null || product.Stock <= 0)
            {
                Console.WriteLine("Sản phẩm không tồn tại hoặc đã hết hàng.");
                return;
            }

            Console.Write("Nhập số lượng: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Số lượng không hợp lệ.");
                return;
            }

            if (quantity > product.Stock)
            {
                Console.WriteLine("Không đủ hàng.");
                return;
            }

            product.Stock -= quantity;
            context.SaveChanges();

            Console.WriteLine($"Mua {quantity} x {product.Name} thành công!");
        }
    }
}
