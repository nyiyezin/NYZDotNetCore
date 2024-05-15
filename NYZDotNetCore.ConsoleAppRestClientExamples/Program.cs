using NYZDotNetCore.ConsoleAppRestClientExamples;

Console.WriteLine("Hello, World!");
RestClientExample restClientExample = new RestClientExample();
await restClientExample.RunAsync();
Console.ReadLine();