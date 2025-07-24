using PetShop.Models;
using PetShopConsole.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopConsole.Services
{
    public  class PetService
    {
        public static void ShowPets(User user, PetShopDbContext context)
        {
            var pets = context.Pets.Where(p => p.UserId == user.UserId).ToList();
            Console.WriteLine("=== Thú cưng của bạn ===");
            foreach (var pet in pets)
            {
                Console.WriteLine($"[{pet.PetId}] {pet.Name} - {pet.Species} ({pet.Breed}) - {pet.Gender}");
            }
        }

        public static void AddPet(User user, PetShopDbContext context)
        {
            Console.Write("Tên thú cưng: ");
            var name = Console.ReadLine();
            Console.Write("Loài: ");
            var species = Console.ReadLine();
            Console.Write("Giống: ");
            var breed = Console.ReadLine();
            Console.Write("Giới tính (Male/Female): ");
            var gender = Console.ReadLine();

            var pet = new Pet
            {
                UserId = user.UserId,
                Name = name,
                Species = species,
                Breed = breed,
                Gender = gender
            };

            context.Pets.Add(pet);
            context.SaveChanges();

            Console.WriteLine("✔️ Thêm thú cưng thành công.");
        }
    }
}
