


using Tsetmc.Application.Interfaces.IUnitOfWork;
using Tsetmc.Application.Interfaces.Repository;
using Tsetmc.Application.Services.ShareHolders;
using Tsetmc.Application.Services.StockHistory;
using Tsetmc.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Iunitofwork, TSETMCContext>();
builder.Services.AddScoped<IStocksHistoryRepo, StocksHistoryRepo>();
builder.Services.AddScoped<IShareHoldersRepo, ShareHoldersRepo>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();

app.MapControllers();

app.Run();
