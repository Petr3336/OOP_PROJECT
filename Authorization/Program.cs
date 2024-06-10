using Authorization.Data;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Aspire.StackExchange.Redis;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Authorization.Models;
using Authorization.Data;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Loading configuration from appsettings.json
var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

// Connecting to local SQLite db.
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//        options.UseSqlServer(builder.Environment.IsDevelopment() ? configuration.GetConnectionString("DbConnectionDev") : configuration.GetConnectionString("DbConnection")));
builder.Services.AddIdentity<UserModel, IdentityRole>()
    .AddEntityFrameworkStores<AuthorizationContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        //ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});
// Add services to the container.
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddRedisClient(string.Empty);

//Добавление доверенных прокси
if (builder.Environment.IsDevelopment())
{
    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.KnownProxies.Add(IPAddress.Parse("localhost:80"));
    });

}

if (builder.Environment.IsDevelopment())
{
    builder.AddSqlServerDbContext<AuthorizationContext>("NotesDb");
}
else
    builder.Services.AddDbContext<AuthorizationContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

var app = builder.Build();

//app.UseDefaultFiles();
//app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Use(async (context, next) =>
    {
        // Логирование информации о входящем запросе
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

        await next.Invoke();

        // Логирование информации об исходящем ответе
        Console.WriteLine($"Response: {context.Response.StatusCode}");
    });
}
else
{
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
