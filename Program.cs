using TestAPI.DB;
using Microsoft.EntityFrameworkCore;
using TestAPI.Services;
using TestAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NSwag;
using NSwag.Generation.Processors.Security;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<TestAPIContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// NSwag
builder.Services.AddOpenApiDocument(c =>
{
    c.Title = "Mi API .NET 10";
    c.Version = "v1";

    c.AddSecurity("Bearer", new NSwag.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = OpenApiSecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Token JWT"
    });

    c.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("TOKEN INVÁLIDO: " + context.Exception.Message);
            return Task.CompletedTask;
        }
    };
});

// CORS
builder.Services.AddCors(option =>
{
    option.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});

// Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<TokenService>();

// Repositories
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<ProductRepository>();

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(c =>
    {
        c.Path = "";
    });
}

app.UseHttpsRedirection();
app.UseCors("NuevaPolitica");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();