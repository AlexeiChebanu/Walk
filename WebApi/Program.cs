using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using WebApi.ConfigureServices;
using WebApi.Data;
using WebApi.Mappings;
using WebApi.Middlewares;
using WebApi.Repository;
using WebApi.Services.RegionServices;
using WebApi.Services.WalkServices;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File($"logs/Walks_Log.txt", rollingInterval: RollingInterval.Day).MinimumLevel.Information().CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.SwaggerConfiguration();

builder.Services.AddDbContext<DbContextWalks>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WalksConnectionString"));
});

builder.Services.AddDbContext<WalksAuthDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("WaklsAuthConnectionString")));

builder.Services.ConfigureServicesRepAndServices();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Walks")
    .AddEntityFrameworkStores<WalksAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureIdentity();
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.MapControllers();

app.Run();
