using Microsoft.EntityFrameworkCore;
using OpenPay.Data.DataModels;

namespace OpenPay.Data;
public class AppDbContext : DbContext
{
    public DbSet<ItemDataModel> Items { get; set; }
    public AppDbContext(DbContextOptions options) : base(options) { }
    public AppDbContext() { }
}
