using System;
using SimpleMvcSitemap;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TicketStore.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SitemapController : ControllerBase
    {
        public IActionResult Index()
        {
            List<SitemapNode> nodes = new List<SitemapNode>
            {
                new SitemapNode(new UriBuilder("https", "chertopolokh.ru").Uri.ToString())
                {
                    ChangeFrequency = ChangeFrequency.Weekly,
                    Priority = 1M
                }
            };

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}
