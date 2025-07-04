using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Mapper;
using QLDT.Repository;
using QLDT.Repository.impl;
using QLDT.Service;
using QLDT.Service.impl;

var builder = WebApplication.CreateBuilder(args);

// Cho phép app React gọi API
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

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<TrainingFormatRepo, TrainingFormatRepoIpml>();
builder.Services.AddScoped<TrainingFormatSer, TrainingFormatSerImpl>();

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

//app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

// Nếu cần tự migrate database khi khởi động
// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//     db.Database.Migrate();
// }

app.Run();
