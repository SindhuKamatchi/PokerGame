using PokerGame.Application;
using PokerGame.Application.Interfaces;
using PokerGame.Infrastructure.Evaluators;
using PokerGame.Infrastructure.Validators;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddScoped<IHandEvaluator, HandEvaluator>();
builder.Services.AddScoped<IHandValidator, HandValidator>();
builder.Services.AddScoped<IPokerGameEngine, PokerGameEngine>();

builder.Services.AddScoped<PokerGameEngine>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();