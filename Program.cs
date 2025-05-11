using DataAccessLayer.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReviewApiApp.DataAccessLayer;
using ReviewApiApp.Services;
using Serilog;



var builder = WebApplication.CreateBuilder(args);// design pattern

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console().
            WriteTo.File("C:\\Users\\kh\\Desktop\\maching learning\\khaled.txt", rollingInterval: RollingInterval.Day).
            CreateLogger();



// Add services to the container.

builder.Logging.ClearProviders();// clear logs from console, debug, output 
builder.Logging.AddConsole();// enables logs in console environment.
builder.Logging.AddDebug();// enables logs in Debug environment.

// Registering serilog library
builder.Host.UseSerilog();

// Registering Dbcontext as Service 
builder.Services.AddDbContext<ApiReviewDbContext>(
    Options => Options.UseSqlServer(builder.Configuration["ConnectionStrings:ApiReviewConnectionString"]));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers(Options =>
{
    Options.ReturnHttpNotAcceptable = true; // 406 code
}

).AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ProductionDataStore>();// by the way it's shared for all application.
#if DEBUG
builder.Services.AddTransient<IMailService,LocalMailService>(); // Registering Localmailserivece
#else
builder.Services.AddTransient<IMailService,CloudMailService>(); // Registering CloudMailService
#endif

builder.Services.AddScoped<IProductRepository,ProductRepository>();

var app = builder.Build();// app is a final instance

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
