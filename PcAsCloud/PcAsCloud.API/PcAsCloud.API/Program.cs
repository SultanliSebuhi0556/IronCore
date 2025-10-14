using PcAsCloud.API;
using PcAsCloud.API.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomServices(builder.Configuration);
builder.Services.ConfigureSwaggerAuthentication();

var app = builder.Build();

app.UseStaticFiles();
await app.AddSeedData();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.EnablePersistAuthorization();
        opt.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
