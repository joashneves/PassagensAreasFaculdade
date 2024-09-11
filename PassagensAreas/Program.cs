using Microsoft.EntityFrameworkCore;
using Infraestrutura;
using PassagensAreas.Infraestrutura;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ClienteContext>();
builder.Services.AddDbContext<VooContext>();
builder.Services.AddDbContext<BilheteContext>();
builder.Services.AddDbContext<ReservaDePassagemContext>();
builder.Services.AddDbContext<VendaContext>();
builder.Services.AddDbContext<CheckInContext>();
builder.Services.AddDbContext<RelatorioOcupacaoContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var contextVoo = scope.ServiceProvider.GetRequiredService<VooContext>();
    var contextCLiente = scope.ServiceProvider.GetRequiredService<ClienteContext>();
    contextVoo.InitializeData();
    contextCLiente.InitializeData();
}

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
