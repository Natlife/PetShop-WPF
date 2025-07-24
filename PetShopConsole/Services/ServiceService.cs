using PetShop.Models;
using PetShopConsole.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopConsole.Services
{
    public class ServiceService
    {
        public static void ShowServices(PetShopDbContext context)
        {
            Console.WriteLine("=== DANH SÁCH DỊCH VỤ ===");
            foreach (var s in context.Services)
            {
                Console.WriteLine($"[{s.ServiceId}] {s.Name} - {s.Price}đ ({s.Duration} phút)");
            }
        }

        public static void BookService(User user, PetShopDbContext context)
        {
            ShowServices(context);
            Console.Write("Chọn ID dịch vụ: ");
            if (!int.TryParse(Console.ReadLine(), out int serviceId)) return;

            var service = context.Services.FirstOrDefault(s => s.ServiceId == serviceId);
            if (service == null)
            {
                Console.WriteLine("Dịch vụ không tồn tại.");
                return;
            }

            var pets = context.Pets.Where(p => p.UserId == user.UserId).ToList();
            if (pets.Count == 0)
            {
                Console.WriteLine("Bạn chưa có thú cưng nào.");
                return;
            }

            Console.WriteLine("=== Danh sách thú cưng của bạn ===");
            foreach (var pet in pets)
            {
                Console.WriteLine($"[{pet.PetId}] {pet.Name} ({pet.Species} - {pet.Breed})");
            }

            Console.Write("Chọn ID thú cưng: ");
            if (!int.TryParse(Console.ReadLine(), out int petId)) return;
            if (!pets.Any(p => p.PetId == petId))
            {
                Console.WriteLine("Không tìm thấy thú cưng.");
                return;
            }

            Console.Write("Chọn ngày (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Ngày không hợp lệ.");
                return;
            }

            Console.Write("Chọn giờ (hh:mm): ");
            if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan time))
            {
                Console.WriteLine("Giờ không hợp lệ.");
                return;
            }

            var booking = new Booking
            {
                UserId = user.UserId,
                PetId = petId,
                ServiceId = service.ServiceId,
                BookingTime = date.Date + time,
                Status = "Pending"
            };

            context.Bookings.Add(booking);
            context.SaveChanges();

            Console.WriteLine("✔️ Đặt lịch thành công.");
        }
    }
}
