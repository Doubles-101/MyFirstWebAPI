var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

var forecasts = new List<WeatherForecast>
{
    new WeatherForecast(DateOnly.FromDateTime(DateTime.Now), 25, "Warm"),
    new WeatherForecast(DateOnly.FromDateTime(DateTime.Now.AddDays(1)), 30, "Hot")
};

app.MapGet("/weatherforecast", () =>
{
    return forecasts;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapPost("/weatherforecast", (WeatherForecast newForecast) => 
{
    forecasts.Add(newForecast);
    return Results.Created($"/weatherforecast/{forecasts.Count - 1}", newForecast);
})
.WithName("AddWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
