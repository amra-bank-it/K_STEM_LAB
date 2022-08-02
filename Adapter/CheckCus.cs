using Newtonsoft.Json;
using NLog;
using RestSharp;
using TokenAuth = K_STEM_LAB.Models.Response.Root;
using ChekCus = K_STEM_LAB.Models.Request.reqCheck;
using RespCheckCus = K_STEM_LAB.Models.Response.respCheck.Root;
using K_STEM_LAB.Models.business.response;

namespace K_STEM_LAB.Adapter
{
  public static class CheckCus
  {

    public static string CheckRequestCus(string userName, string apiKey, string branch , int id)
    {
      Logger _logger = LogManager.GetCurrentClassLogger();
      string token = "";

     
      var client = new RestClient("https://stemlabfaf4.s20.online");
      var request = new RestRequest($"/v2api/{branch}/customer", Method.Post);
      //Получаем токен для дальнейших запросов
      token = Authorization.GetToken(userName, apiKey);
      _logger.Info("Получаем токен для проверки клиента");
      //Проводим десериализацию json 
      TokenAuth TA = JsonConvert.DeserializeObject<TokenAuth>(token);
      request.AddHeader("X-ALFACRM-TOKEN", TA.token);
      request.AddHeader("Content-Type", "application/json");
      request.AddHeader("Cookie", "PHPSESSID=kmkljlrrj4kui7phuetpn1tedj");
      ChekCus cus = new ChekCus();
      cus.is_study = true;
      cus.id = id;
      var body = JsonConvert.SerializeObject(cus);
    
          //  @"{
          //  " + "\n" +
          //@"    ""is_study"": 1, 
          //  " + "\n" +
          //@"    ""id"": 209
          //  " + "\n" +
          //@"}";

      RestResponse response;

      try
      {
        request.AddParameter("application/json", body, ParameterType.RequestBody);
        
        response = client.Execute(request);
                
                if (response != null)
                {
                    _logger.Info("Клиент найден");
                }
                else
                {
                    _logger.Info("Клиент не найден");
                }
      }
      catch (Exception ex)
      {
        _logger.Error("Произошла ошибка при проверке клиента");
        throw new Exception(ex.ToString());
      }

      //Ответ в модель
      RespCheckCus RCC = JsonConvert.DeserializeObject<RespCheckCus>(response.Content);
      
      BusCheckResp BCR = new BusCheckResp();
      BCR.Student = RCC.items[0].legal_name;
      BCR.Balance = RCC.items[0].balance;


      var res = JsonConvert.SerializeObject(BCR);


      return res;
    }
  }
}