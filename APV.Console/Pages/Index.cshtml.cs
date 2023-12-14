﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APV.Console.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<ReadingModel> Readings { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            Readings = new List<ReadingModel>();
        }

        public void OnGet()
        {
            Readings = new List<ReadingModel>();
        }
    }
}