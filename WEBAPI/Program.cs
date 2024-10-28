using WEBAPI.Data;
using WEBAPI.Data.Interfaces;
using WEBAPI.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

PostgressqlConfiguration postgressqlConfiguration = new PostgressqlConfiguration(Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "");
builder.Services.AddSingleton(postgressqlConfiguration);

builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
