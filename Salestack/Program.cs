using Microsoft.EntityFrameworkCore;
using Salestack.Data.Context;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<SalestackDbContext>(options =>
{
    string host = Environment.GetEnvironmentVariable("HOST")!;
    string port = Environment.GetEnvironmentVariable("DB_PORT")!;
    string user = Environment.GetEnvironmentVariable("USER")!;
    string password = Environment.GetEnvironmentVariable("PASSWORD")!;
    string database = Environment.GetEnvironmentVariable("DATABASE")!;

    var connectionString = $"Host={host}:{port};userid={user};password={password};Database={database}";

    options.UseNpgsql(connectionString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsProduction())
{
    builder.WebHost.UseUrls("https://[::]:7001", "http://[::]:7000");
}

if (app.Environment.IsDevelopment())
{
    var connectionString = builder.Configuration.GetSection("ConnectionStrings")
                                            .GetValue<string>("SalestackDbConnectionString");

    builder.Services.AddDbContext<SalestackDbContext>(options =>
    {
        options.UseNpgsql(connectionString);
    });

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("api/test", () => $"API IS RUNNING! {DateTime.UtcNow}.").WithTags("Api");

app.Run();
