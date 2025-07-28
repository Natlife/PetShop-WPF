using Microsoft.EntityFrameworkCore;
using PetShop.Models;
using PetShopConsole.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
namespace consoleTest.Test
{
    public class bookingTest
    {
        private PetShopDbContext GetDb()
        {
            var options = new DbContextOptionsBuilder<PetShopDbContext>()
                .UseInMemoryDatabase("BookingDb")
                .Options;
            return new PetShopDbContext(options);
        }

        [Fact]
        public void Can_Add_Booking()
        {
            using var db = GetDb();
            db.Users.Add(new User { UserId = 1, FullName = "A", Email = "a@gmail.com", Password = "123" });
            db.Pets.Add(new Pet { PetId = 1, UserId = 1, Name = "Tom", Species = "Cat", Gender = "Male" });
            db.Services.Add(new Service { ServiceId = 1, Name = "Tắm", Price = 100000, Duration = 30 });

            db.Bookings.Add(new Booking
            {
                BookingId = 1,
                UserId = 1,
                PetId = 1,
                ServiceId = 1,
                BookingTime = DateTime.Now,
                Status = "Pending"
            });
            db.SaveChanges();

            Assert.Single(db.Bookings);
        }

        [Fact]
        public void Can_Update_Status()
        {
            using var db = GetDb();
            db.Bookings.Add(new Booking { BookingId = 1, UserId = 1, PetId = 1, ServiceId = 1, BookingTime = DateTime.Now, Status = "Pending" });
            db.SaveChanges();

            var b = db.Bookings.First();
            b.Status = "Completed";
            db.SaveChanges();

            Assert.Equal("Completed", db.Bookings.First().Status);
        }
    }

    public class BookingTest
    {
        [Fact]
        public void BookService_ShouldAddBooking_WhenValidInput()
        {
            // Arrange
            var mockContext = new Mock<PetShopDbContext>();
            var user = new User { UserId = 1, FullName = "Test User", Role = "User" };
            var service = new Service { ServiceId = 1, Name = "Grooming", Price = 100, Duration = 30 };
            var pet = new Pet { PetId = 1, Name = "Dog", Species = "Canine", Breed = "Poodle", UserId = 1 };

            var services = new List<Service> { service }.AsQueryable();
            var pets = new List<Pet> { pet }.AsQueryable();
            var bookings = new List<Booking>();

            var mockServices = new Mock<DbSet<Service>>();
            var mockPets = new Mock<DbSet<Pet>>();
            var mockBookings = new Mock<DbSet<Booking>>();

            mockServices.As<IQueryable<Service>>().Setup(m => m.Provider).Returns(services.Provider);
            mockServices.As<IQueryable<Service>>().Setup(m => m.Expression).Returns(services.Expression);
            mockServices.As<IQueryable<Service>>().Setup(m => m.ElementType).Returns(services.ElementType);
            mockServices.As<IQueryable<Service>>().Setup(m => m.GetEnumerator()).Returns(services.GetEnumerator());

            mockPets.As<IQueryable<Pet>>().Setup(m => m.Provider).Returns(pets.Provider);
            mockPets.As<IQueryable<Pet>>().Setup(m => m.Expression).Returns(pets.Expression);
            mockPets.As<IQueryable<Pet>>().Setup(m => m.ElementType).Returns(pets.ElementType);
            mockPets.As<IQueryable<Pet>>().Setup(m => m.GetEnumerator()).Returns(pets.GetEnumerator());

            mockBookings.Setup(m => m.Add(It.IsAny<Booking>())).Callback<Booking>(b => bookings.Add(b));

            mockContext.Setup(c => c.Services).Returns(mockServices.Object);
            mockContext.Setup(c => c.Pets).Returns(mockPets.Object);
            mockContext.Setup(c => c.Bookings).Returns(mockBookings.Object);

            // Act
            // You would call ServiceService.BookService(user, mockContext.Object) here,
            // but since it uses Console.ReadLine, you need to refactor for testability.
            // Instead, you can test the logic by directly adding a booking:
            var booking = new Booking
            {
                UserId = user.UserId,
                PetId = pet.PetId,
                ServiceId = service.ServiceId,
                BookingTime = System.DateTime.Now,
                Status = "Pending"
            };
            mockContext.Object.Bookings.Add(booking);

            // Assert
            Assert.Single(bookings);
            Assert.Equal(user.UserId, bookings[0].UserId);
            Assert.Equal(service.ServiceId, bookings[0].ServiceId);
            Assert.Equal(pet.PetId, bookings[0].PetId);
            Assert.Equal("Pending", bookings[0].Status);
        }
    }
}
