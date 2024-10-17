using BusinessCard_Core.Interfaces.IRepositories;
using BusinessCard_Core.Interfaces.UnitOfWorkInterface;
using BusinessCard_Infrastructure.DBContext;
using BusinessCard_Infrastructure.Repositories;
using BusinessCard_Infrastructure.Repositories.UnitOfWork;
using BusinessCard_Services.IServices;
using BusinessCard_Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBusinessCardRepository, BusinessCardRepository>();
builder.Services.AddScoped<IBusinessCardService, BusinessCardService>();
builder.Services.AddScoped<IFileService, FileService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();
