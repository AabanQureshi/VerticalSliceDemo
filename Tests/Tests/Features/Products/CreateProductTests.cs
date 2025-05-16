using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using VerticalSliceDemo.Features.Product.Create;
using FluentAssertions;

namespace Tests.Tests.Features.Products;
public class CreateProductHandlerTests
{
    private readonly DbContextOptions<ApplicationDBContext> _dbOptions;

    public CreateProductHandlerTests()
    {
        _dbOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                        .UseInMemoryDatabase(databaseName: new Guid().ToString())
                        .Options;
    }

    [Fact]
    public async Task Handle_ShouldAddProductAndReturnId_WhenValidCommand()
    {
        // Arrange
        var command = new CreateProductCommand
        {
            Name = "Test Product",
            Description = "Test Desc",
            Price = 100,
            Stock = 10
        };

        await using var context = new ApplicationDBContext(_dbOptions);
        var handler = new CreateProductCommandHandler(context);

        // Act
        var result = await handler.HandleAsync(command, CancellationToken.None);

        // Assert
        result.Id.Should().NotBeEmpty();
        var productInDb = await context.Products.FindAsync(result);
        productInDb.Should().NotBeNull();
        productInDb.Name.Should().Be(command.Name);
    }
}
