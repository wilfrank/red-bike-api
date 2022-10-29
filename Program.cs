using BIke_Network_bk.Extensions;
using BIke_Network_bk.Options;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Bind database options. Invalid configuration will terminate the application startup.
//builder.Services.ConfigurationDependencies(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
var connectionStringsOptions =
builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStringsOptions>();
var cosmosDbOptions = builder.Configuration.GetSection("CosmosDb").Get<CosmosDbOptions>();
var (serviceEndpoint, authKey) = connectionStringsOptions.ActiveConnectionStringOptions;
var (databaseName, collectionData) = cosmosDbOptions;
var collectionNames = collectionData.Select(c => c.Name).ToList();
// Add services to the container.
//builder.Services.AddCosmosDb(serviceEndpoint, authKey, databaseName, collectionNames);
builder.Services.AddFirestoreDb();
// Add health check by checking CosmosDb connection. Cache the result for 1 minute.
builder.Services.AddHealthChecks()
    .AddCheck("Liveness Check", () => HealthCheckResult.Healthy(), new List<string>(1) { "live" });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigurationDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseCors(x => x
    .AllowAnyOrigin()
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();

//internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}