using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Serilog;

namespace ElasticSearchSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILoggerService _loggerService;

        public WeatherForecastController(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            StringValues companyName = Request.Headers["Company"];
            string company = "System";

            if (companyName.Any())
                company = companyName.ToString();

            LoggerRequest? log = new()
            {
                ApplicationName = "ElasticSearchSample",
                Company = company,
                Message = "kkk"
            };

            await _loggerService.LogInformation(log);

            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray());
        }
    }
}
