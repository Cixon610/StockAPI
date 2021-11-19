using Microsoft.Extensions.Options;
using Stock.Api.Model;
using Stock.Api.Services;

//Add json file
IConfiguration appsettings = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

Environment.GetEnvironmentVariable("DATABASE_URL");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => options.UseMemberCasing());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<BookstoreDatabaseSettings>(appsettings.GetSection(nameof(BookstoreDatabaseSettings)));
builder.Services.AddSingleton<IBookstoreDatabaseSettings>(sp =>  sp.GetRequiredService<IOptions<BookstoreDatabaseSettings>>().Value);
builder.Services.AddSingleton<BookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
