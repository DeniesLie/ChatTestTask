using Application.Hubs;
using WebApi.HostConfigurations;
using WebApi.ServicesInstallers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthenticationWithIdentityServer(builder.Configuration);
builder.Services.AddCorsWithConfiguration(builder.Configuration, builder.Environment);
builder.Services.AddFluentValidationWithValidatorsInAssembly();

builder.Services.InstallServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseCors("AngularClientPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<MessageHub>("/hubs/messages");

app.Run();