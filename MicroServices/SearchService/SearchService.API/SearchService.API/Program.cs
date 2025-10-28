using SearchService.API;
using SearchService.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddHostedService<RabbitMqConsumerBackgroundService>();
builder.Services.AddCustomServices(builder.Configuration);
var app = builder.Build();

await app.AddSeedData();
app.Services.GetRequiredService<IHostedService>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();