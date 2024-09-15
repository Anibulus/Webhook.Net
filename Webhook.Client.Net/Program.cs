using Microsoft.AspNetCore.Mvc;

string server = "http://localhost:5278";
string callback = "http://localhost:5180/wh/item/new";
string topic = "item.new";

var client = new HttpClient();  
Console.WriteLine($"Subscribing to topic {topic} with callback {callback}");
await client.PostAsJsonAsync($"{server}/subscribe", new {topic, callback});

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();
var app = builder.Build();


app.MapPost("/wh/item/new", async (HttpContext context, ILogger<Program> logger) =>
{
    // Read the request body
    var payload = await new StreamReader(context.Request.Body).ReadToEndAsync();
    // Log the payload
    logger.LogInformation("Received payload: {payload}", payload);
});

app.Run();
