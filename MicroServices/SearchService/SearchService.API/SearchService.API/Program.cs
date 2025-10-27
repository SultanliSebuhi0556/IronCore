using SearchService.API;
using SearchService.API.Extensions;
using SwaggerThemes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<RabbitMqConsumerService>();
builder.Services.AddCustomServices(builder.Configuration);
var app = builder.Build();
await app.AddSeedData();
app.Services.GetRequiredService<IHostedService>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(Theme.UniversalDark, null, opt =>
    {
        //opt.EnablePersistAuthorization();
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();