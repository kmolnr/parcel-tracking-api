using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ParcelTracking.API.Services.Authentication;
using ParcelTracking.API.Services.ParcelTracking;
using ParcelTracking.EFCore;

var builder = WebApplication.CreateBuilder(args);

OptionsConfigurationServiceCollectionExtensions.Configure<IdentitySettings>(builder.Services, builder.Configuration.GetSection("IdentitySettings"));

builder.Services.AddDbContext<ParcelTrackingDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("ParcelTrackingDatabase")));

var identitySettings = new IdentitySettings();

var identitySettingsConfiguration = builder.Configuration.GetSection("IdentitySettings");

builder.Services.Configure<IdentitySettings>(identitySettingsConfiguration);

identitySettingsConfiguration.Bind(identitySettings);

var secretKey = Encoding.ASCII.GetBytes(identitySettings.SecretKey);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.RequireHttpsMetadata = false;
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(secretKey),
		ValidateIssuer = false,
		ValidateAudience = false
	};
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("User", policy =>
	{
		policy.RequireClaim(ClaimTypes.Role, "user");
	});
});

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IParcelTrackingService, ParcelTrackingService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "JWT token",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtSecurityScheme, Array.Empty<string>() } });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
