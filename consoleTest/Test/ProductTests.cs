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
    public class ProductTests
    {
        private PetShopDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<PetShopDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            return new PetShopDbContext(options);
        }

        [Fact]
        public void Can_Add_Product()
        {
            // Arrange
            using var context = GetInMemoryDb();
            var product = new Product { Name = "Xương", Price = 10000, Description = "Ngon", Stock = 10 };

            // Act
            context.Products.Add(product);
            context.SaveChanges();

            // Assert
            var saved = context.Products.FirstOrDefault(p => p.Name == "Xương");
            Assert.NotNull(saved);
            Assert.Equal(10000, saved.Price);
        }
    }
}
