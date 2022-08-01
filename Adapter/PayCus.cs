using Newtonsoft.Json;
using NLog;
using RestSharp;
using TokenAuth = K_STEM_LAB.Models.Response.Root;
using Pay = K_STEM_LAB.Models.Request.reqPay;
using RespPayCus = K_STEM_LAB.Models.Response.respPay.Root;
using K_STEM_LAB.Models.business.response;

namespace K_STEM_LAB.Adapter
{
  public static class PayCus
  {
    public static string RequestPay(string userName, string apiKey, string branch , int id_dogovor , decimal amount)
    {
      Logger _logger = LogManager.GetCurrentClassLogger();
      string token = "";
      string dateTime = DateTime.Now.ToString("dd.MM.yyyy");
      var client = new RestClient("https://stemlabfaf4.s20.online");
      var request = new RestRequest($"/v2api/{branch}/pay/create", Method.Post);
      token = Authorization.GetToken(userName, apiKey);
      _logger.Info("Получаем токен для проведения платежа");
      TokenAuth TA = JsonConvert.DeserializeObject<TokenAuth>(token);
      request.AddHeader("X-ALFACRM-TOKEN", TA.token);
      request.AddHeader("Authorization", "Basic ZGFtZXlqb251YUB5YW5kZXgucnU6MWRjZjQxMDItYmRmMi0xMWViLWI3NGMtYWMxZjZiNDc4MmJl");
      request.AddHeader("Content-Type", "application/json");
      request.AddHeader("Cookie", "PHPSESSID=kmkljlrrj4kui7phuetpn1tedj");
            
      Pay pay = new Pay();

            pay.branch_id = branch;
            pay.customer_id = id_dogovor;
            pay.pay_type_id = 1;
            pay.pay_account_id = 4;
            pay.pay_item_id = 1;
            pay.document_date = dateTime;
            pay.income = amount;
            pay.payer_name = "Terminal test";
            pay.note = "Оплата с терминала Амра-банк";
            pay.is_confirmed = 1;


      var body = JsonConvert.SerializeObject(pay);
      

            //var body = @"{
            //" + "\n" +
            //            $@"    ""branch_id"": ""{branch}"",
            //" + "\n" +
            //            @"    ""customer_id"": ""209"",
            //" + "\n" +
            //            @"    ""pay_type_id"": ""1"",
            //" + "\n" +
            //            @"    ""pay_account_id"": ""4"",
            //" + "\n" +
            //            @"    ""pay_item_id"": ""1"",
            //" + "\n" +
            //            $@"    ""document_date"": ""{dateTime}"",
            //" + "\n" +
            //            @"    ""income"": ""10"",
            //" + "\n" +
            //            @"    ""payer_name"": ""Terminal test"",
            //" + "\n" +
            //            @"    ""note"": ""Оплата с терминала Амра-банк"",
            //" + "\n" +
            //            @"    ""is_confirmed"": ""1""
            //" + "\n" +
            //            @"}
            //" + "\n" +
            //            @"";

      RestResponse response = null;
      try
      {
        request.AddParameter("application/json", body, ParameterType.RequestBody);
        response = client.Execute(request);
        _logger.Info("Платеж оформлен");
      }
      catch (Exception ex)
      {
        _logger.Error("Возникла ошибка при попытке платежа" + ex.ToString());
        throw new Exception(ex.ToString());
      }

      //Ответ в модель
      RespPayCus RPC = JsonConvert.DeserializeObject<RespPayCus>(response.Content);

      BusPayResp BPR = new BusPayResp();
      BPR.Success = RPC.success;
      BPR.idRecepient = RPC.model.id;     
      BPR.income = RPC.model.income;
      BPR.note = RPC.model.note;
      BPR.is_confirmed = RPC.model.is_confirmed;



      var res = JsonConvert.SerializeObject(BPR);



      return res;
    }
  }
}