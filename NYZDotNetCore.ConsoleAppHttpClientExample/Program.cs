using NYZDotNetCore.ConsoleAppHttpClientExamples;

Console.WriteLine("Hello World");
HttpClientExample httpClientExample = new HttpClientExample();
await httpClientExample.RunAsync();
Console.ReadLine();