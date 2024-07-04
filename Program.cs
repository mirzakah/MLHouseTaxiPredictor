using MLHouseTaxiPredictor.Services;
using Microsoft.ML;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .WriteTo.Console()
    .WriteTo.File("Logs/MLLogs-.txt", rollingInterval: RollingInterval.Day));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MLContext>(new MLContext(builder.Configuration.GetValue<int>("MLModel:Seed")));
builder.Services.AddSingleton<IHousingModelService, HousingModelService>();
builder.Services.AddSingleton<ITaxiModelService, TaxiModelService>();


var app = builder.Build();

// Load models at startup
var housingModelService = app.Services.GetRequiredService<IHousingModelService>();
var housingModelPath = builder.Configuration.GetValue<string>("MLHousingModel:Path");
var housingDataPath = builder.Configuration.GetValue<string>("DataPaths:BostonHousing");

if (!File.Exists(housingModelPath))
{
    housingModelService.TrainModel(housingDataPath);
}

var taxiModelService = app.Services.GetRequiredService<ITaxiModelService>();
var taxiModelPath = builder.Configuration.GetValue<string>("MLTaxiModel:Path");
var taxiDataPath = builder.Configuration.GetValue<string>("DataPaths:NYCTaxi");

if (!File.Exists(taxiModelPath))
{
    taxiModelService.TrainModel(taxiDataPath);
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
