using Microsoft.EntityFrameworkCore;
using OtpProject;
using OtpProject.Models;
using System.Globalization;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var strings = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.Configure<SmsAero>(builder.Configuration.GetSection("SmsAero"));
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(strings));
builder.Services.AddScoped<IDbEditor, DbEditor>();
builder.Services.AddScoped<IOtpSender, OtpSender>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
