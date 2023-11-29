using Microsoft.EntityFrameworkCore;
using Suppliers.Domain;
using Suppliers.Persistence;

namespace Suppliers.Tests.Common
{
    public class SuppliersDbContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid ProductIdForDelete = Guid.NewGuid();
        public static Guid ProductIdForUpdate = Guid.NewGuid();

        public static Guid TenderIdForDelete = Guid.NewGuid();
        public static Guid TenderIdForUpdate = Guid.NewGuid();
        public static Guid TenderIdForRegister = Guid.NewGuid();

        public static SuppliersDbContext Create()
        {
            var options = new DbContextOptionsBuilder<SuppliersDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new SuppliersDbContext(options);
            context.Database.EnsureCreated();
            context.Products.AddRange(
                new Product
                {
                    UserId = UserAId,
                    Id = Guid.Parse("A6BB65BB-5AC2-4AFA-8A28-2616F675B825"),
                    Name = "Name1",
                    Price = 1,
                    Quantity = 1,
                    ExpirationDate = DateTime.Today
                },
                new Product
                {
                    UserId = UserBId,
                    Id = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084"),
                    Name = "Name2",
                    Price = 2,
                    Quantity = 2,
                    ExpirationDate = DateTime.Today
                },
                new Product
                {
                    UserId = UserAId,
                    Id = ProductIdForDelete,
                    Name = "Name3",
                    Price = 3,
                    Quantity = 3,
                    ExpirationDate = DateTime.Today
                },
                new Product
                {
                    UserId = UserBId,
                    Id = ProductIdForUpdate,
                    Name = "Name4",
                    Price = 4,
                    Quantity = 4,
                    ExpirationDate = DateTime.Today
                }
            );

            context.Tenders.AddRange(
                new Tender
                {
                    Id = TenderIdForUpdate,
                    Title = "Title1",
                    Description = "Description1",
                    Beginning = DateTime.Today,
                    Ending = DateTime.Today,
                    IsOpen = true
                },
                new Tender
                {
                    Id = TenderIdForDelete,
                    Title = "Title2",
                    Description = "Description2",
                    Beginning = DateTime.Today,
                    Ending = DateTime.Today,
                    IsOpen = true
                },
                new Tender
                {
                    Id = TenderIdForRegister,
                    Title = "Title3",
                    Description = "Description3",
                    Beginning = DateTime.Today,
                    Ending = DateTime.Today,
                    IsOpen = true
                },
                new Tender
                {
                    Id = Guid.Parse("4EEE8331-2CD6-4D29-BE0F-515C28A85A98"),
                    Title = "Title4",
                    Description = "Description4",
                    Beginning = DateTime.Today,
                    Ending = DateTime.Today,
                    IsOpen = true
                }
            );

            context.SaveChanges();

            return context;
        }

        public static void Destroy(SuppliersDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
