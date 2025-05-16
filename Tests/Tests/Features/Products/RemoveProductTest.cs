using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using VerticalSliceDemo.Features.Product.Remove;

namespace Tests.Tests.Features.Products
{
    public class RemoveProductTest
    {
        private readonly DbContextOptions<ApplicationDBContext> _dbOptions;

        public RemoveProductTest()
        {
            _dbOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                            .UseInMemoryDatabase(databaseName: "new Guid().ToString()")
                            .Options;
        }

        [Fact]
        public async Task Handle_ShouldRemoveProduct_WhenProductExists()
        {
            // Arrange

            await using var context = new ApplicationDBContext(_dbOptions);
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Test Product",
                Description = "Test Description",
                Price = 10.5m,
                Stock = 100
            };
            context.Products.Add(product);
            await context.SaveChangesAsync();
            var handler = new RemoveProductCommandHandler(context);
            var command = new RemoveProductCommand { Id = product.Id };
            // Act
            await handler.HandleAsync(command, CancellationToken.None);
            // Assert
            var removedProduct = await context.Products.FindAsync(product.Id);
            removedProduct.Should().BeNull();
        }
    }
}
