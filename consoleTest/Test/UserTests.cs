using Microsoft.EntityFrameworkCore;
using PetShop.Models;
using PetShopConsole.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleTest.Test
{
    public class UserTests
    {
        private PetShopDbContext GetDb()
        {
            var options = new DbContextOptionsBuilder<PetShopDbContext>()
                .UseInMemoryDatabase("UserDb")
                .Options;
            return new PetShopDbContext(options);
        }

        [Fact]
        public void Can_Register_User()
        {
            using var db = GetDb();
            db.Users.Add(new User { FullName = "Nguyen Van A", Email = "a@example.com", Password = "123456" });
            db.SaveChanges();

            var user = db.Users.FirstOrDefault(u => u.Email == "a@example.com");
            Assert.NotNull(user);
        }
    }
}
