using NLog;
using RestSharp;
using Sentry;

namespace K_STEM_LAB.Adapter
{
  public static class Authorization
  {
    /// <summary>
    /// Получаем токен для дальнейших запросов
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="apiKey"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string GetToken(string userName, string apiKey)
    {
      Logger _logger = LogManager.GetCurrentClassLogger();
      RestResponse response = null;

      using (SentrySdk.Init(o =>
      {
              //Сделать в виде настройки либо отдельный класс конст https://beac10ea51a34a7db141d4d5ce880070@sentry.asar.studio/21
        o.Dsn = "https://beac10ea51a34a7db141d4d5ce880070@sentry.asar.studio/21";
        o.Debug = true;
        o.TracesSampleRate = 1.0;
      }))

        try
        {
          //Сделать в виде настройки либо отдельный класс конст
          var client = new RestClient("https://stemlabfaf4.s20.online");
          var request = new RestRequest("/v2api/auth/login", Method.Post);

          request.AddHeader("Content-Type", "application/json");

          var body = @"{
                    " + "\n" +
                              $@"    ""email"": ""{userName}"",
                    " + "\n" +
                              $@"    ""api_key"": ""{apiKey}""
                    " + "\n" +
                              @"}";




          request.AddParameter("application/json", body, ParameterType.RequestBody);
          response = client.Execute(request);
          _logger.Info("Авторизовались");

        }
        catch (Exception ex)
        {
          _logger.Error("Произошла ошибка авторизации" + ex.ToString());
          SentrySdk.CaptureException(new Exception("Возникла ошибка: " + ex.ToString()));
          throw new Exception("Uh oh!");
        }

      var transaction = SentrySdk.StartTransaction(
        "test-transaction-name",
        "test-transaction-operation"
      );

      var span = transaction.StartChild("test-child-operation");


      span.Finish();
      transaction.Finish();

      return response.Content;
    }
  }
}