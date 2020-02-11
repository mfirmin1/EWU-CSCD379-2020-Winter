using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Controllers
{
    public class UserController : Controller
    {
        IHttpClientFactory ClientFactory { get; }
        public UserController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            ClientFactory = clientFactory;
        }

        public async Task<ActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SantaApi");

            UserClient client = new UserClient(httpClient);
            ICollection<User> group = await client.GetAllAsync();
            return View(client);
        }
    }
}
