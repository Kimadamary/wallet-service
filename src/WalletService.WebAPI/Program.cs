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


app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers(); 
app.Run();