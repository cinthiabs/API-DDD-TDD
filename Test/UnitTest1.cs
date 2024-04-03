using Entities.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        public class Token
        {
            public string validTo { get; set; }
            public string value { get; set; }
        }

        private Token _token;

        [TestMethod]
        public void TestMethod()
        {
            GetToken();
            var result = ChamaApiPost("https://localhost:7124/api/Add").Result;
            var list = JsonConvert.DeserializeObject<Message[]>(result).ToList();
            Assert.IsTrue(list.Any());
        }

        public void GetToken()
        {
            string url = "https://localhost:7124/api/CreateTokenIdentity";

            using (var client = new HttpClient())
            {
                var dados = new
                {
                    email = "cinthia@cinthia.com",
                    senha = "Cinthia123!",
                    cpf = "string"
                };
                string json = JsonConvert.SerializeObject(dados);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var result = client.PostAsync(url, content).Result;
                if (result.IsSuccessStatusCode)
                {
                    var tokenJson = result.Content.ReadAsStringAsync().Result;
                    _token = JsonConvert.DeserializeObject<Token>(tokenJson);
                }
            }
        }

        public string CallApiGet(string url)
        {
            if (_token != null && !string.IsNullOrWhiteSpace(_token.value))
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.value);
                    var response = client.GetStringAsync(url).Result;
                    return response;
                }
            }
            return null;
        }

        public async Task<string> ChamaApiPost(string url, object dados = null)
        {
            string json = dados != null ? JsonConvert.SerializeObject(dados) : "";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            string result = string.Empty;

            if (_token != null && !string.IsNullOrWhiteSpace(_token.value))
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.value);
                    var response = await client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                    else
                    {
                        return "teste";
                    }
                }
            }
            return result;
        }
    }

}