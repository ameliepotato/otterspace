using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APV.Console.Pages
{
    public class ConfigurationModel : PageModel
    {
        private readonly ILogger<ConfigurationModel> _logger;

        public ConfigurationModel(ILogger<ConfigurationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}