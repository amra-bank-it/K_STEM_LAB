using K_STEM_LAB.Adapter;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace K_STEM_LAB.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  public class CheckController : ControllerBase
  {

    [HttpGet(Name = "GetCheck")]
    public string GetCheck(string userName, string apiKey, string branch, int id_dogovor)
    {
    
        return CustomerBilling.Check(userName, apiKey, branch, id_dogovor);

     
    }
  }
}