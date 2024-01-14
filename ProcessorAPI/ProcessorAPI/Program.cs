using Microsoft.EntityFrameworkCore;
using ProcessorAPI;
using ProcessorAPI.Business;
using ProcessorAPI.Business.Interfaces;
using ProcessorAPI.Business.Mapper;
using ProcessorAPI.DataAccess.Context;
using ProcessorAPI.DataAccess.Interfaces.Repositories;
using ProcessorAPI.DataAccess.Repositories;
using ProcessorAPI.Mapper;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddAutoMapper(typeof(PresentationMapper))
    .AddAutoMapper(typeof(BusinessMapper));

builder.Services.AddDbContext<ProcessorContext>(options => options
    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IProcessorRepository, ProcessorRepository>();

builder.Services.AddScoped<IProcessorService, ProcessorService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
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


