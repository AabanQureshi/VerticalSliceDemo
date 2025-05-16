using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerticalSliceDemo.Features.Product.GetById;

namespace Tests.Tests.Features.Products
{
    public class GetProductByIdTest
    {
        private readonly DbContextOptions<ApplicationDBContext> _dbOptions;

        public GetProductByIdTest()
        {
            _dbOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                            .UseInMemoryDatabase(databaseName: new Guid().ToString())
                            .Options;
        }
        [Fact]
        public async Task Handle_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            await using var context = new ApplicationDBContext(options);

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

            var handler = new GetProductByIdQueryHandler(context);
            var query = new GetProductByIdQuery { Id = product.Id };

            // Act
            var result = await handler.HandleAsync(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(product.Name);
            result.Stock.Should().Be(product.Stock);
        }

    }
}
