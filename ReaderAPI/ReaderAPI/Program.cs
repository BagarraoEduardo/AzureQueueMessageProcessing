using ReaderAPI.Business;
using ReaderAPI.Integration;
using ReaderAPI.Mapper;
using ReaderAPI.Utility;
using FileOptions = ReaderAPI.Utility.FileOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(typeof(ReaderAPIMapper));

builder.Services.Configure<ParserAPIOptions>(options => builder.Configuration.GetSection("ParserAPI").Bind(options));
builder.Services.Configure<FileOptions>(options => builder.Configuration.GetSection("FileOptions").Bind(options));

builder.Services.AddHttpClient<IParserAPIClient, ParserAPIClient>(http => http.BaseAddress = new Uri(builder.Configuration.GetSection("ParserAPI:Url").Value));

builder.Services.AddScoped<IFileHandler, FileHandler>();

builder.Services.AddScoped<IReaderService, ReaderService>();

builder.Services.AddControllers();
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
