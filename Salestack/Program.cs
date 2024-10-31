using Microsoft.EntityFrameworkCore;
using Salestack.Data.Config;
using Salestack.Data.Context;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


if (builder.Environment.IsDevelopment())
{
    var settings = builder.Configuration.GetSection("Salestack").Get<SalestackSettings>();

    var connectionString = settings?.SalestackDbConnectionString;

    builder.Services.AddDbContext<SalestackDbContext>(options =>
    {
        options.UseNpgsql(connectionString);
    });

    builder.Services.AddScoped<IDbContextFactory<SalestackDbContext>, SalestackDbContextFactory>();
}

if (builder.Environment.IsProduction())
{
    string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "Some error has occurred.";

    builder.Services.AddDbContext<SalestackDbContext>(options =>
    {
        options.UseNpgsql(connectionString);
    });

    builder.WebHost.UseUrls("http://[::]:7000");
}

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

app.MapGet("api/test", () => $"API IS RUNNING! {DateTime.UtcNow}.").WithTags("Api");

app.Run();
