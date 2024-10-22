using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("ConnectionStrings")
                                            .GetValue<string>("SalestackDbConnectionString");

builder.Services.AddDbContext<SalestackDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("api/test", () => $"API IS RUNNING! {DateTime.UtcNow}.");

app.Run();
