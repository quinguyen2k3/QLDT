using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Mapper;
using QLDT.Repository;
using QLDT.Repository.impl;
using QLDT.Service;
using QLDT.Service.impl;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(o =>
    o.AddPolicy("AllowReactApp", p =>
        p.WithOrigins("http://localhost:5000")
         .AllowAnyHeader()
         .AllowAnyMethod()));

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Repository
builder.Services.AddScoped<ITrainingFormatRepo, TrainingFormatRepoImpl>();
builder.Services.AddScoped<ITrainingUnitRepo, TrainingUnitRepoImpl>();
builder.Services.AddScoped<IEducationLevelRepo, EducationLevelRepoImpl>();
builder.Services.AddScoped<IRoleRepo, RoleRepoImpl>();
builder.Services.AddScoped<IPartRepo, PartRepoImpl>();

// Service
builder.Services.AddScoped<ITrainingFormatSer, TrainingFormatSerImpl>();
builder.Services.AddScoped<ITrainingUnitSer, TrainingUnitSerImpl>();
builder.Services.AddScoped<IEducationLevelSer, EducationLevelSerImpl>();
builder.Services.AddScoped<IRoleSer, RoleSerImpl>();
builder.Services.AddScoped<IPartSer, PartSerImpl>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();
app.Run();
