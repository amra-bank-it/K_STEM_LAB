using Microsoft.AspNetCore.Mvc;
using K_STEM_LAB.Models;
using K_STEM_LAB.Adapter;
using K_STEM_LAB.Models.Request;
using RestSharp;
using NLog;
using Sentry;
using System.Net.Mime;

namespace K_STEM_LAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CheckController : ControllerBase
    {
   
        [HttpGet(Name = "GetCheck")]
        public string GetCheck(string userName, string apiKey , string branch , int id_dogovor)
        {
            return CheckCus.CheckRequestCus(userName , apiKey , branch , id_dogovor);
        }
    }
}