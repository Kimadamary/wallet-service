using Microsoft.EntityFrameworkCore;
using WalletService.Infrastructure.Persistence;
using WalletService.Application.Abstractions;
using WalletService.Application.Services;
using WalletService.WebAPI.Middlewares;





var builder = WebApplication.CreateBuilder(args);





builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

builder.Services.AddScoped<IWalletService, WalletService.Application.Services.WalletService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();


// Apply migrations and seed initial data.

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    if (!context.Clients.Any())
    {
        context.Clients.AddRange(
            new WalletService.Domain.Entities.Client("MID-001", "Иванов Иван Иванович", "PART-101"),
            new WalletService.Domain.Entities.Client("MID-002", "Петров Петр Петрович"),
            new WalletService.Domain.Entities.Client("MID-003", "Сидорова Анна Сергеевна")

        );
        context.SaveChanges();
    }
}

app.Run();