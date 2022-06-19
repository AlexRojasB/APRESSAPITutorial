using ApressSolution.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
var npgsqlBuilder = new NpgsqlConnectionStringBuilder();
npgsqlBuilder.ConnectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
npgsqlBuilder.Username = builder.Configuration["UserID"];
npgsqlBuilder.Password = builder.Configuration["Password"];
builder.Services.AddDbContext<CommandContext>(opt => opt.UseNpgsql(npgsqlBuilder.ConnectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
