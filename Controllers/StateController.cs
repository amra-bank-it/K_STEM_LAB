using K_STEM_LAB.Adapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sentry;

namespace K_STEM_LAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {

        [HttpGet(Name = "GetState")]
        public string GetState(string userName, string apiKey , string branch)
        {
            return StatePay.GetStatePay(userName, apiKey , branch);
        }
    }
}
