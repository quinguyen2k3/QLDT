using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QLDT.Data;
using QLDT.Manager;
using QLDT.Mapper;
using QLDT.Middlewares;
using QLDT.Repository;
using QLDT.Repository.impl;
using QLDT.Service;
using QLDT.Service.impl;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSec");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();


//Allow connect with Front End 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
//Add helper for app
builder.Services.AddScoped<JwtManager>();
builder.Services.AddScoped<TransactionManager>();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//Add DI for Repository
builder.Services.AddScoped<TrainingFormatRepo, TrainingFormatRepoIpml>();
builder.Services.AddScoped<TrainingUnitRepo, TrainingUnitRepoImpl>();
builder.Services.AddScoped<EducationLevelRepo, EducationLevelRepoImpl>();
builder.Services.AddScoped<DepartmentRepo, DepartmentRepoImpl>();
builder.Services.AddScoped<PartRepo, PartRepoImpl>();
builder.Services.AddScoped<UserRepo, UserRepoImpl>();
builder.Services.AddScoped<RefreshTokenRepo, RefreshTokenRepoImpl>();
builder.Services.AddScoped<InvalidTokenRepo, InvalidTokenRepoImpl>();


//Add DI for Service
builder.Services.AddScoped<TrainingFormatSer, TrainingFormatSerImpl>();
builder.Services.AddScoped<TrainingUnitSer, TrainingUnitSerImpl>();
builder.Services.AddScoped<EducationLevelSer, EducationLevelSerImpl>();
builder.Services.AddScoped<PartSer, PartSerImpl>();
builder.Services.AddScoped<DepartmentSer, DepartmentSerImpl>();
builder.Services.AddScoped<AuthenticationSer, AuthenticationSerImpl>();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "QLDT API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                        Enter 'Bearer' [space] and then your token.
                        Example: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...'",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

//Run seed data for generate default account
await SeedData.InitializeAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

//Add Middleware
app.UseMiddleware<InvalidTokenMiddleware>();
app.UseMiddleware<CheckUserActiveMiddleware>();

app.UseAuthorization();

app.MapControllers();

// Nếu cần tự migrate database khi khởi động
// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//     db.Database.Migrate();
// }

app.Run();
