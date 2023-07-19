using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TowerLocatorApp.DataAccess.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TowerLocatorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase {
        public RouteService routeService { get; set; }
        public RouteController(RouteService routeService)   //Instance RouteService
        {
            this.routeService = routeService;
        }
        // GET: api/<RouteController>
        [HttpGet]
        public async Task<ActionResult<string>> Get() {
            string test = "testovaci hodnota";
            return JsonConvert.SerializeObject(test);
        }

        // GET api/<RouteController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<RouteController>
        [HttpPost]
        public async Task<IActionResult> SaveRoute([FromForm] IFormFile gpxFile, [FromForm] IFormFile csvFile, [FromForm] string routeName) {
            await routeService.SaveRouteAsync(gpxFile, csvFile, routeName);
            return Ok(routeName);
        }

        // PUT api/<RouteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<RouteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
