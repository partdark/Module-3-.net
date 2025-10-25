using Hello_world_API.Interfaces;
using Hello_world_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hello_world_API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class Hellocontroller : ControllerBase
    {
        private readonly ITimeSevice _timesevice;

        public Hellocontroller (ITimeSevice timesevice)
        {
            _timesevice = timesevice;
        }

        [HttpGet("Hello")]
        public async Task<ActionResult<string>> Get()
        {
            return "Hello World API";
        }

        [HttpGet("CurrentTime")]
        public async Task<ActionResult<string>> GetTime()
        {
            return _timesevice.GetCurentTime();
        }
    }
}
    //public class WeatherForecastController : ControllerBase
    //{
    //    private static readonly string[] Summaries = new[]
    //    {
    //        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //    };

//    private readonly ILogger<WeatherForecastController> _logger;

//    public WeatherForecastController(ILogger<WeatherForecastController> logger)
//    {
//        _logger = logger;
//    }

//    [HttpGet(Name = "GetWeatherForecast")]
//    public IEnumerable<WeatherForecast> Get()
//    {
//        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//        {
//            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            TemperatureC = Random.Shared.Next(-20, 55),
//            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//        })
//        .ToArray();
//    //    }
//    //}
//}
