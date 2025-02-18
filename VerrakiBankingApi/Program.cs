using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VerrakiBanking.Business.Services.Implemetation;
using VerrakiBanking.Business.Services.Interface;
using VerrakiBanking.Data.DbContext;
using VerrakiBanking.Data.Entity;

var builder = WebApplication.CreateBuilder(args);

// Set up the ApplicationDbContext with a connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT Bearer Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
})
.AddJwtBearer(options =>
{
    // Use the configuration values from appsettings.json
    options.Authority = builder.Configuration["Jwt:Issuer"];
    options.Audience = builder.Configuration["Jwt:Audience"];
    options.RequireHttpsMetadata = false;  // Set to true in production
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,  // Optional: to adjust for clock skew if needed
    };
});

// Register application services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthservice, AuthService>();

// Add controllers and Swagger
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

// Enable Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
