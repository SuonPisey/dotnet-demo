using Microsoft.EntityFrameworkCore;
using Myapi.Models.product;

namespace MyApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProductModel> Products => Set<ProductModel>();
    public DbSet<UserModel> Users => Set<UserModel>();

    
}