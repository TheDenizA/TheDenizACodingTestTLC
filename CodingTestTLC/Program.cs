using CodingTestTLC.Partners;
using CodingTestTLC.Repositories;
using CodingTestTLC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<LotteryService>();
builder.Services.AddSingleton<IThirdPartyService, ThirdPartyService>();
builder.Services.AddSingleton<IDatabase, Database>();
builder.Services.AddSingleton<IPurchaseRepo, PurchaseRepo>();

builder.Services.AddControllers();
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

app.MapControllers();

app.Run();
