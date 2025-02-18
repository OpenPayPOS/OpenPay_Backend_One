using Microsoft.EntityFrameworkCore;

namespace Tests.Data.ItemRepositoryTests;
public class GetAllAsync
{
    [Fact]
    public async Task GetAllAsync_Returns_All_Items()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("tasdflkajsdfln")
            .Options;
        var dbContext = new AppDbContext(options);
        
        var logger = Substitute.For<ILogger<ItemRepository>>();
        var repository = new ItemRepository(dbContext, logger);

        await dbContext.Items.AddAsync(new ItemDataModel { Id = Guid.NewGuid(), Name = "Item1", Price = 10.5m, TaxPercentage = 5m });
        await dbContext.Items.AddAsync(new ItemDataModel { Id = Guid.NewGuid(), Name = "Item2", Price = 20m, TaxPercentage = 10m });
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync().ToListAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, i => i.Name == "Item1");
        Assert.Contains(result, i => i.Name == "Item2");
    }

    [Fact]
    public async Task GetAllAsync_ReturnsEmptyEnumerable_OnEmpty()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("zxclvjapwen")
            .Options;
        var dbContext = new AppDbContext(options);
        var logger = Substitute.For<ILogger<ItemRepository>>();
        var repository = new ItemRepository(dbContext, logger);

        // Act
        var result = await repository.GetAllAsync().ToListAsync();

        // Assert
        Assert.Empty(result);
    }
}