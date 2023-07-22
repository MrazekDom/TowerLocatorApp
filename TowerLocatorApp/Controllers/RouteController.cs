using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using TowerLocatorApp.DataAccess.Services;
using TowerLocatorApp.Models;

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
        public async Task<IActionResult> GetRoutes() {
            var AllRoutes = await routeService.getAllRoutesAsync();
            return Ok(AllRoutes);
        } 

        // GET api/<RouteController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleRoute(int id) {
            var geoJson = await routeService.getRouteAsync(id);
            return Ok(geoJson);
        }

        // POST api/<RouteController>
        [HttpPost]
        public async Task<IActionResult> SaveRoute([FromForm] IFormFile gpxFile, [FromForm] IFormFile csvFile, [FromForm] string routeName) {
            await routeService.SaveRouteAsync(gpxFile, csvFile, routeName);
            return Ok(new { RouteName = routeName });
        }

        // PUT api/<RouteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<RouteController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            string deletedRoute = await routeService.deleteRouteAsync(id);    /*nejdrive vymazu "Route" a potom znovu nahraju seznam*/
            return Ok(new { DeletedRoute = deletedRoute });
            //var AllRoutes = await routeService.getAllRoutesAsync();
            
            
        }
    }
}
