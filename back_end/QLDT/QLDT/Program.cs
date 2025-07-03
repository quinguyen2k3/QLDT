using Microsoft.EntityFrameworkCore;
using QLDT.Data;
using QLDT.Settings;

var builder = WebApplication.CreateBuilder(args);

// bind connection string
builder.Services.Configure<SettingsApp>(builder.Configuration.GetSection("SettingsApp"));
var settings = builder.Configuration.GetSection("SettingsApp").Get<SettingsApp>();

// register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(settings.DefaultConnection));

// register controllers (or minimal APIs)
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(opt =>  
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
app.MapControllers();
app.Run();