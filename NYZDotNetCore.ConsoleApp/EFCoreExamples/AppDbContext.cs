using Microsoft.EntityFrameworkCore;
using NYZDotNetCore.ConsoleApp.Dtos;
using NYZDotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYZDotNetCore.ConsoleApp.EFCoreExamples
{
    internal class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        // }

        public DbSet<BlogDto> Blogs { get; set; }
    }
}
