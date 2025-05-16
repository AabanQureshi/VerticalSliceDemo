using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerticalSliceDemo.Features.Product.Get;

namespace Tests.Tests.Features.Products
{
    public class GetAllProductsTest
    {
        private readonly DbContextOptions<ApplicationDBContext> _dbOptions;

        public GetAllProductsTest()
        {
            _dbOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                            .UseInMemoryDatabase(databaseName: new Guid().ToString())
                            .Options;
        }

        [Fact]
        public async Task Handle_ShouldReturnAllProducts_WhenProductsExist()
        {
            await using var context = new ApplicationDBContext(_dbOptions);
            var product1 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Test Product 1",
                Description = "Test Description 1",
                Price = 10.5m,
                Stock = 100
            };
            var product2 = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Test Product 2",
                Description = "Test Description 2",
                Price = 20.5m,
                Stock = 200
            };
            context.Products.AddRange(product1, product2);
            await context.SaveChangesAsync();
            var handler = new GetProductsQueryHandler(context);
            var query = new GetProductsQuery();
            // Act
            var result = await handler.HandleAsync(query, CancellationToken.None);
            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
    }
}
