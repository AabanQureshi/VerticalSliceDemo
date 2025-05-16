using Domain.Entities;
using Domain.ValueTypes;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Tests.Tests.Features.Products
{
    public class UpdateProductTest
    {
        private readonly DbContextOptions<ApplicationDBContext> _dbOptions;

        public UpdateProductTest()
        {
            _dbOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                            .UseInMemoryDatabase(databaseName: new Guid().ToString())
                            .Options;
        }

        [Fact]
        public async Task Handle_ShouldUpdateProduct_WhenProductExits()
        {
            await using var context = new ApplicationDBContext(_dbOptions);
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Test Product",
                Description = "Test Description",
                Price = new Price(10.5m, "USD"),
                Stock = 100
            };
            context.Products.Add(product);
            await context.SaveChangesAsync();
            var command = new VerticalSliceDemo.Features.Product.Update.UpdateProductCommand
            {
                Id = product.Id,
                Name = "Updated Product",
                Description = "Updated Description",
                Price = new Price(20.5m, "PKR"),
                Stock = 200
            };
            var handler = new VerticalSliceDemo.Features.Product.Update.UpdateProductCommandHandler(context);
            // Act
            await handler.HandleAsync(command, CancellationToken.None);
            // Assert
            var updatedProduct = await context.Products.FindAsync(product.Id);
            updatedProduct.Should().NotBeNull();
            updatedProduct.Name.Should().Be(command.Name);
        }
    }
}
