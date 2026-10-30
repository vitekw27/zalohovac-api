using ZalohovacServer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZalohovacServer.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        
        ConfigJob configJob = new ConfigJob(new MyContext());
        MyContext context = new MyContext();
        // GET: api/<ConfigController>
        
        [HttpGet]
        public IEnumerable<Config> Get()
        {
            return configJob.GetConfigs();
        }

        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        // GET api/<ConfigController>/5
        [HttpGet("{id}")]
        public Config Get(int id)
        {
            foreach (var config in configJob.GetConfigs())
            {
                if (config.id == id)
                {
                    return config;
                }
            }
            return null;
        }
        

        // POST api/<ConfigController>
        [HttpPost]
        public void Post([FromBody] Config value)
        {
            List<Config> temp = new List<Config>();
            temp.Add(value);

            configJob.AddConifgs(temp);
        }



        // PUT api/<ConfigController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Config value)
        {

            configJob.UpdateConfig(id, value);
            
        }

        // DELETE api/<ConfigController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            configJob.DeleteConfig(id); 
        }
    }
}
