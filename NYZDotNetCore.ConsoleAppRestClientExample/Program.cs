using NYZDotNetCore.ConsoleAppRestClientExample;

Console.WriteLine("Hello, World!");
RestClientExample restClientExample = new RestClientExample();
await restClientExample.RunAsync();
Console.ReadLine();