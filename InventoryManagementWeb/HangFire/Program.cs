using Hangfire;
using Hangfire.SqlServer;
using HangFire.Models;
using HangFire.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<TransactionServices>();

// config db
var hangfireDb = builder.Configuration.GetConnectionString("HangfireDB");
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HangfireDB")));

// hangfire service

builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseDefaultTypeSerializer()
    .UseSqlServerStorage(hangfireDb, new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.Zero,
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    })
);

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseHangfireDashboard();

app.UseAuthorization();

app.MapControllers();

// Endpoint untuk POST data
app.MapPost("/insertdata-product", async (Transaction transaction, TransactionServices transactionServices) =>
{
    try
    {
        BackgroundJob.Enqueue(() => transactionServices.DailySummaryService());

        BackgroundJob.Enqueue(() =>
            Console.WriteLine("Fire-and-forget job has been executed"));

        return Results.Created($"/insertdata-product/{transaction.TransactionId}", transaction);
    }
    catch (Exception ex)
    {
        return Results.Problem("An error occurred while processing your request: " + ex.Message);
    }
});

// Menjadwalkan pekerjaan setiap 5 detik
RecurringJob.AddOrUpdate<TransactionServices>(
    "daily-summary-job",
    service => service.DailySummaryService(),
    "*/5 * * * * *");

app.Run();
