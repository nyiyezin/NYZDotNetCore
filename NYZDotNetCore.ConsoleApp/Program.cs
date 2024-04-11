using NYZDotNetCore.ConsoleApp;

Console.WriteLine("Hello, World!");

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
// adoDotNetExample.Read();
// adoDotNetExample.Create("title", "author", "content");
// adoDotNetExample.Update(1002, "update title", "update author", "update content");
// adoDotNetExample.Delete(1002);
adoDotNetExample.Edit(1002);
adoDotNetExample.Edit(1);

Console.ReadKey();