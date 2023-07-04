using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using Test.Data;
using Test.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test", Version = "v1" });
});


builder.Services.AddCors(opt => opt.AddDefaultPolicy(p => p.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()));
                

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration
    .GetConnectionString("TestDB"));
});

builder.Services.AddScoped<InvoicesRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// configure request pipeline
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.UseExceptionHandler()
app.Run();

