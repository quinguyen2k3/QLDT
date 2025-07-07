using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Mapper;

// namespace chứa các interface repo
using QLDT.Repository;
// namespace chứa các impl repo
using QLDT.Repository.impl;

// namespace chứa các interface service
using QLDT.Service;
// namespace chứa các impl service
using QLDT.Service.impl;

var builder = WebApplication.CreateBuilder(args);

// 1. CORS – cho phép React gọi API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 2. DbContext – kết nối SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// 4. Repository — chỉ đăng ký những interface⇒impl đã xong
//builder.Services.AddScoped<ITrainingFormatRepo, TrainingFormatRepoImpl>();
builder.Services.AddScoped<ITrainingUnitRepo, TrainingUnitRepoImpl>();
builder.Services.AddScoped<IEducationLevelRepo, EducationLevelRepoImpl>();
builder.Services.AddScoped<IRoleRepo, RoleRepoImpl>();
builder.Services.AddScoped<IPartRepo, PartRepoImpl>();

// 5. Service — chỉ đăng ký những interface⇒impl đã xong
//builder.Services.AddScoped<ITrainingFormatSer, TrainingFormatSerImpl>();
builder.Services.AddScoped<ITrainingUnitSer, TrainingUnitSerImpl>();
builder.Services.AddScoped<IEducationLevelSer, EducationLevelSerImpl>();
builder.Services.AddScoped<IRoleSer, RoleSerImpl>();
builder.Services.AddScoped<IPartSer, PartSerImpl>();

// (Các repo/service khác như IUserRepo, IAuthSer, IPermissionSer, IDashboardSer sẽ đăng ký sau, khi đã xong)

// 6. Controllers & Swagger/OpenAPI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 7. Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();
