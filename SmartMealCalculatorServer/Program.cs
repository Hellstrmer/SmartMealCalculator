using Microsoft.EntityFrameworkCore;
using SmartMealCalculatorServer.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IngredientsDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()/*WithOrigins("http://smartmealcalculator:5195")*/
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddLogging();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/
app.UseSwagger();
app.UseSwaggerUI();
/*
app.UseHttpsRedirection();*/

app.UseCors();
/*app.UseAuthorization();*/

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<IngredientsDbContext>();
    dbContext.Database.EnsureCreated();
}   

app.Run();
