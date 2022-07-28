using Newtonsoft.Json;
using NLog;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TokenAuth = K_STEM_LAB.Models.Response.Root;
namespace K_STEM_LAB.Adapter
{
    public static class StatePay
    {
        public static string GetStatePay(string userName , string apiKey , string branch)
        {
            Logger _logger = LogManager.GetCurrentClassLogger();
            string token = "";
            var client = new RestClient("https://stemlabfaf4.s20.online");
            var request = new RestRequest($"/v2api/{branch}/pay/index", Method.Post);


            token = Authorization.GetToken(userName, apiKey);
            _logger.Info("Получаем токен для проверки статуса платежей");
            TokenAuth TA = JsonConvert.DeserializeObject<TokenAuth>(token);

            request.AddHeader("X-ALFACRM-TOKEN", TA.token);
            request.AddHeader("Authorization", "Basic ZGFtZXlqb251YUB5YW5kZXgucnU6MWRjZjQxMDItYmRmMi0xMWViLWI3NGMtYWMxZjZiNDc4MmJl");

            var body = @"";

            RestResponse response = null;
            try
            {
                request.AddParameter("text/plain", body, ParameterType.RequestBody);
                response = client.Execute(request);
                _logger.Info("Проверили платежи");
            }
            catch(Exception ex)
            {
                _logger.Error("Произошла ошибка при проверке списка платежей");
                throw new Exception(ex.ToString());
            }

            return response.Content;

        }
    }
}