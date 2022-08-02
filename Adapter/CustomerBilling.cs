using K_STEM_LAB.Models.business.response;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using ChekCus = K_STEM_LAB.Models.Request.reqCheck;
using RespCheckCus = K_STEM_LAB.Models.Response.respCheck.Root;
using Pay = K_STEM_LAB.Models.Request.reqPay;
using RespPayCus = K_STEM_LAB.Models.Response.respPay.Root;

using TokenAuth = K_STEM_LAB.Models.Response.Root;

namespace K_STEM_LAB.Adapter
{
  public static class CustomerBilling
  {
    public static string Pay(string userName, string apiKey, string branch, int id_dogovor, decimal amount,string payer_name)
    {
      Logger _logger = LogManager.GetCurrentClassLogger();

      var client = new RestClient("https://stemlabfaf4.s20.online");

      var request = new RestRequest($"/v2api/{branch}/pay/create", Method.Post);

      TokenAuth TA = GetTokenAuth(userName, apiKey);

      AddHeader(TA, ref request);

      string body = BodyPayRequest(branch, id_dogovor, amount, payer_name);

      request.AddParameter("application/json", body, ParameterType.RequestBody);

      var response = client.Execute(request);

      IsNullException(response);

      RespPayCus RPC = GetPaymentCustomerFromJSON(response);

      BusPayResp BPR = ConvertPaymentCustomer(RPC);

      var res = JsonConvert.SerializeObject(BPR);

      return res;
    }
    public static string Check(string userName, string apiKey, string branch, int id)
    {
      Logger _logger = LogManager.GetCurrentClassLogger();

      TokenAuth TA = GetTokenAuth(userName, apiKey);

      var client = new RestClient("https://stemlabfaf4.s20.online");

      var request = new RestRequest($"/v2api/{branch}/customer", Method.Post);

      AddHeader(TA, ref request);

      string body = BodyCheckRequest(id);

      request.AddParameter("application/json", body, ParameterType.RequestBody);

      var response = client.Execute(request);

      IsNullException(response);

      RespCheckCus RCC = GetCustomerFromJSON(response);

      BusCheckResp BCR = ConvertCustomer(RCC);

      var res = JsonConvert.SerializeObject(BCR);

      return res;
    }
    private static BusPayResp ConvertPaymentCustomer(RespPayCus RPC)
    {
      BusPayResp BPR = new BusPayResp();



      BPR.Success = RPC.success;
      BPR.idRecepient = RPC.model.id;
      BPR.income = RPC.model.income;
      BPR.is_confirmed = RPC.model.is_confirmed;
      return BPR;
    }

    private static string BodyPayRequest(string branch, int id_dogovor, decimal amount,string payer_name)
    {
      Pay pay = new Pay();

      pay.branch_id = branch;
      pay.customer_id = id_dogovor;
      pay.pay_type_id = 1;
      pay.pay_account_id = 4;
      pay.pay_item_id = 1;
      pay.document_date = DateTimeString();
      pay.income = amount;
      pay.payer_name = payer_name;
      pay.note = $"Оплата с {pay.payer_name}";
      pay.is_confirmed = 1;


      var body = JsonConvert.SerializeObject(pay);
      return body;
    }

    private static string DateTimeString()
    {
      return DateTime.Now.ToString("dd.MM.yyyy");
    }


    private static TokenAuth GetTokenAuth(string userName, string apiKey)
    {
      var token = Authorization.GetToken(userName, apiKey);
      TokenAuth TA = JsonConvert.DeserializeObject<TokenAuth>(token);
      return TA;
    }

    private static void AddHeader(TokenAuth TA, ref RestRequest request)
    {
      request.AddHeader("X-ALFACRM-TOKEN", TA.token);
      request.AddHeader("Content-Type", "application/json");
      request.AddHeader("Cookie", "PHPSESSID=kmkljlrrj4kui7phuetpn1tedj");
    }

    private static BusCheckResp ConvertCustomer(RespCheckCus RCC)
    {
      BusCheckResp BCR = new BusCheckResp();
      BCR.Student = RCC.items[0].legal_name;
      BCR.Balance = RCC.items[0].balance;
      return BCR;
    }

    private static RespCheckCus GetCustomerFromJSON(RestResponse? response)
    {
      if (response.StatusCode != System.Net.HttpStatusCode.OK)
        throw new Exception(response.ErrorException.ToString());


      RespCheckCus RCC = JsonConvert.DeserializeObject<RespCheckCus>(response.Content);

      if (RCC.count == 0 || RCC.items.Count == 0)
        throw new Exception("Клиент не найден в базе СТЕМ");
      return RCC;
    }
    private static RespPayCus GetPaymentCustomerFromJSON(RestResponse? response)
    {
      if (response.StatusCode != System.Net.HttpStatusCode.OK)
        throw new Exception(response.ErrorException.ToString());


      RespPayCus RPC = JsonConvert.DeserializeObject<RespPayCus>(response.Content);

      if (RPC.errors.Count != 0 || RPC.success != true)
        throw new Exception($"платеж не успешный, ошибка провайдера:{String.Join(", ", RPC.errors.ToArray())}");
      return RPC;
    }

    private static void IsNullException(RestResponse? response)
    {
      if (response == null)
        throw new Exception("Ответ запроса на проверку равен NULL ");
    }

    private static string BodyCheckRequest(int id)
    {
      ChekCus cus = new ChekCus();
      cus.is_study = true;
      cus.id = id;
      var body = JsonConvert.SerializeObject(cus);
      return body;
    }
  }
}