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
        public string GetPay(string userName, string apiKey , string branch , int id_dogovor , decimal amount,string payer_name)
        {            
           return CustomerBilling.Pay(userName, apiKey , branch , id_dogovor , amount, payer_name);
        }
    }
}
