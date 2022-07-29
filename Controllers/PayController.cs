using K_STEM_LAB.Adapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sentry;

namespace K_STEM_LAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayController : ControllerBase
    {
        
    [HttpGet(Name = "GetPay")]
        public string GetPay(string userName, string apiKey , string branch , int id_Dogovor , decimal amount)
        {            
           return PayCus.RequestPay(userName, apiKey , branch , id_Dogovor, amount);
        }
    }
}
