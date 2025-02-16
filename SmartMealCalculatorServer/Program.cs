using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using SmartMealCalculatorServer.Helpers;

var builder = WebApplication.CreateBuilder(args);


if (builder.Environment.IsDevelopment())
{
    builder.WebHost.UseKestrel(options =>
    {
        options.ListenAnyIP(5099);
        options.ListenAnyIP(5098, configure => configure.UseHttps());
    });
}
else
{
    builder.WebHost.UseKestrel(options =>
    options.ListenAnyIP(80));
}

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSmartMealApp", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            policy
                .WithOrigins(
                    "http://localhost:5195",
                    "https://localhost:5195",
                    "http://192.168.50.51:5195",
                    "https://192.168.50.51:5195"
                    )
            .AllowAnyMethod()
            .AllowAnyHeader();
        }
        else
        {
             policy
                .WithOrigins(
                    "https://smartmeal.jesperhellstrom.com",
                    "https://apismartmeal.jesperhellstrom.com"
                    )
                .AllowAnyMethod()
                .AllowAnyHeader();
        }
/*        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();*/
    });
});

builder.Services.AddDbContext<IngredientsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddLogging();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowSmartMealApp");
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<IngredientsDbContext>();
    dbContext.Database.EnsureCreated();
}   

app.Run();
