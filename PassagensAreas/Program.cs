using Microsoft.EntityFrameworkCore;
using Infraestrutura;
using PassagensAreas.Infraestrutura;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClienteContext>(opt =>
    opt.UseInMemoryDatabase("ClienteList"));
builder.Services.AddDbContext<VooContext>(opt =>
    opt.UseInMemoryDatabase("VooList"));
builder.Services.AddDbContext<BilheteContext>(opt =>
    opt.UseInMemoryDatabase("BilheteList"));
builder.Services.AddDbContext<ReservaDePassagemContext>(opt =>
    opt.UseInMemoryDatabase("ReservaDePassagemList"));
builder.Services.AddDbContext<AssentoContext>(opt =>
    opt.UseInMemoryDatabase("AssentoList"));
builder.Services.AddDbContext<VendaContext>(opt =>
    opt.UseInMemoryDatabase("VendaList"));

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
