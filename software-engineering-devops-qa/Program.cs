using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using software_engineering_devops_qa;
using software_engineering_devops_qa.Dal;

// Check JWT secret is supplied and is a valid 256-bit string, else panic
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET_TOKEN");
if (string.IsNullOrEmpty(jwtSecret))
{
    throw new ArgumentException("The environment variable $JWT_SECRET_TOKEN was not set");
}
else if (jwtSecret.Length < 32)
{
    throw new Exception("The supplied $JWT_SECRET_TOKEN was not greater than or equal to 256 bits (32 bytes)");
}

// Check default admin password is set
var adminPassword = Environment.GetEnvironmentVariable("ADMIN_DEFAULT_PASSWORD");
if (string.IsNullOrEmpty(adminPassword))
{
    throw new ArgumentException("The environment variable $ADMIN_DEFAULT_PASSWORD was not set");
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services
    .AddAuthentication(x => 
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x => 
    {
        x.TokenValidationParameters = new()
        {
            ValidIssuer = "LMS-API",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();

Config.LmsDbConnection = builder.Configuration.GetConnectionString("LmsDb")!;

// Initialise tables if not created
CourseDal.Init(Config.LmsDbConnection);
EnrolmentDal.Init(Config.LmsDbConnection);
UserDal.Init(Config.LmsDbConnection, adminPassword);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();
