using Microsoft.AspNetCore.Mvc;
using ZalohovacServer.Models; 

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZalohovacServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsConfigDataController : ControllerBase
    {
        CCDjob job = new CCDjob();
        [HttpGet]
        public IEnumerable<ClientsConfigData> Get()
        {
            return this.job.GetCCDs(); 
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ClientsConfigData Get(int id)
        {
            return this.job.GetCCD(id); 
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] ClientsConfigData value)
        {
            this.job.AddCCD(value);
        }

  
       

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.job.DeleteCCD(id); 
        }
    }
}
