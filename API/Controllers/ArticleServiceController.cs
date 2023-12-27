using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleServiceController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<ArticleServiceController> _logger;

        public ArticleServiceController(ILogger<ArticleServiceController> logger)
        {
            _logger = logger;
        }

        // TODO
        // Implement for article CRUD
       
    }
}