using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using software_engineering_devops_qa;
using software_engineering_devops_qa.Dal;
using software_engineering_devops_qa.Models;
using software_engineering_devops_qa.Util;

var jwtEnvName = "JWT_SECRET_TOKEN";
var pwdEnvName = "ADMIN_DEFAULT_PASSWORD";

// Check JWT secret is supplied and is a valid 256-bit string, else panic
var jwtSecret = Environment.GetEnvironmentVariable(jwtEnvName)
	.ExpectValue($"The envrionment variable ${jwtEnvName} was not set");
if (jwtSecret.Length < 32)
{
	throw new Exception("The supplied $JWT_SECRET_TOKEN was not greater than or equal to 256 bits (32 bytes)");
}

// Check default admin password is set
var adminPassword = Environment.GetEnvironmentVariable(pwdEnvName)
	.ExpectValue($"The environment variable ${pwdEnvName} was not set");

if (!PasswordUtil.FitsPasswordPolicy(adminPassword))
{
	throw new InvalidDataException($"Admin password does not satisfy the password policy.\n\t{PasswordUtil.passwordPolicyError}");
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

builder.Services.AddAuthorizationBuilder()
	.AddPolicy(Idenity.adminPolicy, p =>
		p.RequireClaim(Idenity.roleClaimName, ((int)Role.Admin).ToString()));

builder.Services.AddControllersWithViews();

// App configuration
Config.LmsDbConnection = builder.Configuration.GetConnectionString("LmsDb").ExpectValue("LmsDb connection string not supplied");
Config.HttpResponseHeaders = builder.Configuration.GetRequiredSection("HttpResponseHeaders").GetChildren().ToDictionary(x => x.Key, x => x.Value)!;

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

// Apply security headers
app.Use(async (context, next) =>
{
	foreach (var kvp in Config.HttpResponseHeaders)
	{
		context.Response.Headers[kvp.Key] = kvp.Value;
	}
	await next();
});

app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
