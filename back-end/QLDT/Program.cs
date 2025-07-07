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
builder.Services.AddScoped<TrainingFormatRepo, TrainingFormatRepoImpl>();
builder.Services.AddScoped<TrainingUnitRepo, TrainingUnitRepoImpl>();
builder.Services.AddScoped<EducationLevelRepo, EducationLevelRepoImpl>();

builder.Services.AddScoped<PartRepo, PartRepoImpl>();

// Service
builder.Services.AddScoped<TrainingFormatSer, TrainingFormatSerImpl>();
builder.Services.AddScoped<TrainingUnitSer, TrainingUnitSerImpl>();
builder.Services.AddScoped<EducationLevelSer, EducationLevelSerImpl>();
builder.Services.AddScoped<PartSer, PartSerImpl>();

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
