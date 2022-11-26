using Invoice.Applicaion.Interface;
using Invoice.Applicaion.Interfaces;
using Invoice.Applicaion.Services;
using Invoice.Infra.Data;
using Invoice.Infra.Data.Interfaces;
using Invoice.Infra.Data.Repository;
using Invoice.Services.API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Option-1: Serilog on top of MEL(Microsoft Etension logging)
//builder.Host.UseSerilog((ctx, lc) => lc
//    .WriteTo.Console()
//    //.WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
//    .ReadFrom.Configuration(ctx.Configuration)
//    );

//Option-2: It will use pure serilog API without MEL
//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();
//Log.Information("Ah, there you are!");


//Option-3:var logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();
//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logger);

//Option-4: Create logger with read configuration
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IInvoiceInfoRepository, InvoiceInfoRepository>();
builder.Services.AddScoped<IInvoiceDetailsRepository, InvoiceDetailsRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddSingleton<ITokenService, TokenService>();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InvoiceConnection"))
);

var Configuration = builder.Configuration;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidAudience = Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseSerilogRequestLogging(); //use this with option-1

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
