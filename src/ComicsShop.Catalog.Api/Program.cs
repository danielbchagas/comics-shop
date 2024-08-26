using ComicsShop.Catalog.Api;
using ComicsShop.Catalog.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region

builder.DependencyInjectionConfirgure();
builder.OptionsConfigurationConfigure();
builder.RestClientConfigure();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region

app.EndpointsConfigure();

#endregion

app.Run();