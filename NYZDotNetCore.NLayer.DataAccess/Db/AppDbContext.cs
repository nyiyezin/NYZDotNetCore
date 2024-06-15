using Microsoft.EntityFrameworkCore;
using NYZDotNetCore.NLayer.DataAccess.Models;

namespace NYZDotNetCore.NLayer.DataAccess.Db;

public class AppDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    }

    public DbSet<BlogModel> Blogs { get; set; }
}
