using Microsoft.AspNetCore.Mvc;
using ZalohovacServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZalohovacServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        ClientJob job = new ClientJob();
        // GET: api/<ClientController>
        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return job.GetClients().ToList(); 
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public Client Get(int id)
        {
            return job.GetClient(id); 
        }

        // POST api/<ClientController>
        [HttpPut]
        public void Put([FromBody] Client value)
        {
            this.job.UpdateClient(value); 
        }

        [HttpPost]

        public void Post([FromBody] Client value)
        {
            this.job.AddClient(value);
        }




        
    }
}
