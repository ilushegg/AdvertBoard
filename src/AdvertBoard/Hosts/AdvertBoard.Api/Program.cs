using Microsoft.OpenApi.Models;
using AdvertBoard.Contracts;
using AdvertBoard.Registrar;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AdvertBoard.Domain;
using Microsoft.AspNetCore.Identity;
using AdvertBoard.DataAccess;
using AdvertBoard.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerServices();


/*
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AdvertBoardContext>()
    .AddDefaultTokenProviders();*/

builder.Services.AddAuthenticationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();