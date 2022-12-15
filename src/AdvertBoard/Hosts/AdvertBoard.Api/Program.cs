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

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerServices();



builder.Services.AddAuthenticationServices(builder.Configuration);
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();



app.UseSession();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("MyPolicy");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();