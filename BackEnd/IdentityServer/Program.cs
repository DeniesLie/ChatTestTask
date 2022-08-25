using Application.Services;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using DataAccess;
using DataAccess.Repositories;
using IdentityServer.ServicesInstallers;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddIdentityServerWithConfigurations(builder.Configuration);

var app = builder.Build();

app.UseIdentityServer();

app.Run(builder.Configuration["AppUrl"]);