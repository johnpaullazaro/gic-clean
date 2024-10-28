using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GIC.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InterestRuleController : ControllerBase
    {
        // GET: api/<InterestRuleController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<InterestRuleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InterestRuleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<InterestRuleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InterestRuleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
