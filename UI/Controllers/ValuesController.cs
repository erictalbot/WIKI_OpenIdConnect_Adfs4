using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class ValuesController : ApiController
    {

        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];

        // GET: api/Value
        public async Task<string> Get()
        {
            ClientCredential clientCredential = new ClientCredential(clientId, "Ge3yIuoggX1AQZlbv-3bzBN0A1ITXgSTQXTYsrTk");
            string authority = "https://w2k16-tp3-adfs4.adfs4.com/adfs";
            AuthenticationContext authContext = new AuthenticationContext(authority, false);
            var ar = authContext.AcquireToken("https://localhost:44321/", clientCredential);
            string authHeader = ar.CreateAuthorizationHeader();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44321/api/Values");
            request.Headers.TryAddWithoutValidation("Authorization", authHeader);
            HttpResponseMessage response = await client.SendAsync(request);
            String responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        // GET: api/Values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Values
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Values/5
        public void Delete(int id)
        {
        }
    }
}
