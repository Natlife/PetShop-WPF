using PetShopConsole.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopConsole.Menus
{
    public class Common
    {
        public static void ShowAllBookings(PetShopDbContext context)
        {
            Console.WriteLine("=== TẤT CẢ LỊCH ĐẶT ===");
            foreach (var b in context.Bookings)
            {
                var user = context.Users.FirstOrDefault(u => u.UserId == b.UserId);
                var pet = context.Pets.FirstOrDefault(p => p.PetId == b.PetId);
                var service = context.Services.FirstOrDefault(s => s.ServiceId == b.ServiceId);

                Console.WriteLine($"ID: {b.BookingId} - {user?.FullName} | {pet?.Name} | {service?.Name} | {b.BookingTime} | {b.Status}");
            }
        }
    }
}
