using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZalohovacServer.Models; 

namespace ZalohovacServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        [HttpGet]

        public List<Report> Get()
        {
            return new MyContext().Reports.ToList(); 
        }
    }
}
