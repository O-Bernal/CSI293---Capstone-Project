using Microsoft.EntityFrameworkCore;
using RhythmGameWebApplication.Data;
using System.Net.WebSockets;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 32))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// WebSocket middleware
app.UseWebSockets();
// Map WebSocket endpoint
app.Map("/ws", HandleWebSocketConnection);

app.Run();


// WebSocket handler
async Task HandleWebSocketConnection(HttpContext context)
{
    if (!context.WebSockets.IsWebSocketRequest)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        return;
    }

    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

    // You can now send and receive messages using the `webSocket` object.
    // This is a simple example that echoes received messages back to the client.
    var buffer = new byte[1024 * 4];
    while (true)
    {
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        if (result.MessageType == WebSocketMessageType.Text)
        {
            await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), WebSocketMessageType.Text, result.EndOfMessage, CancellationToken.None);
        }
        else if (result.MessageType == WebSocketMessageType.Close)
        {
            break;
        }
    }
}
