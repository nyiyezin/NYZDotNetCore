using NYZDotNetCore.ConsoleApp.AdoDotNetExamples;
using NYZDotNetCore.ConsoleApp.DapperExamples;
using NYZDotNetCore.ConsoleApp.EFCoreExamples;
using NYZDotNetCore.ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;


// AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
// adoDotNetExample.Read();
// adoDotNetExample.Create("title", "author", "content");
// adoDotNetExample.Update(1003, "title 1", "author 1", "content 1");
// adoDotNetExample.Delete(1003);
// adoDotNetExample.Update(1002, "test title", "test author", "test content");
// adoDotNetExample.Delete(1002);
// adoDotNetExample.Edit(1002);
// adoDotNetExample.Edit(1);

// DapperExample dapperExample = new DapperExample();
// dapperExample.Run();

// EFCoreExample eFCoreExample = new EFCoreExample();
// eFCoreExample.Run();

var connectionString = ConnectionStrings.sqlConnectionStringBuilder.ConnectionString;
var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);

var serviceProvider = new ServiceCollection()
    .AddScoped(n => new AdoDotNetExample(sqlConnectionStringBuilder))
    .AddScoped(n => new DapperExample(sqlConnectionStringBuilder))
    .AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(connectionString);
    })
    .AddScoped<EFCoreExample>()
    .BuildServiceProvider();

//AppDbContext db = serviceProvider.GetRequiredService<AppDbContext>();

var adoDotNetExample = serviceProvider.GetRequiredService<AdoDotNetExample>();
adoDotNetExample.Read();

//var dapperExample = serviceProvider.GetRequiredService<DapperExample>();
//dapperExample.Run();

Console.ReadLine();