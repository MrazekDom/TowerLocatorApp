using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TowerLocatorApp.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase {
        // GET: api/<RouteController>
        [HttpGet]
        public async Task<string> Get() {
            return  "Testovaci hodnota";
        }

        // GET api/<RouteController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<RouteController>
        [HttpPost]
        public void Post([FromBody] string value) {
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
