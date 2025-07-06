using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QLDT.Data;
using QLDT.Mapper;
using QLDT.Repository;
using QLDT.Repository.impl;
using QLDT.Service;
using QLDT.Service.impl;

var builder = WebApplication.CreateBuilder(args);

// 1. CORS: cho phép React App gọi API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 2. DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// 4. Repository registration
//    → luôn đăng ký interface ⇒ implementation
builder.Services.AddScoped<ITrainingFormatRepo, TrainingFormatRepoImpl>();
builder.Services.AddScoped<IUserRepo, UserRepoImpl>();
// TODO: AddScoped cho các repo khác: ITrainingUnitRepo, IClassRepo, IUserRepo, IRoleRepo, …

// 5. Service registration
builder.Services.AddScoped<ITrainingFormatSer, TrainingFormatSerImpl>();
builder.Services.AddScoped<IAuthSer, AuthSerImpl>();
builder.Services.AddScoped<IDashboardSer, DashboardSerImpl>();
// TODO: AddScoped cho các service khác tương tự…

// 6. JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"];
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);
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
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

// 7. Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 8. Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Nếu bạn cần tự migrate DB khi chạy:
// using var scope = app.Services.CreateScope();
// var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
// db.Database.Migrate();

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
