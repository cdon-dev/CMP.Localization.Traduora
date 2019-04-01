﻿using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using Traduora.Client;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private IStringLocalizer _localizer;

        public HomeController(IConfiguration config, IStringLocalizer localizer, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _localizer = localizer;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index(string locale = "de_DE")
        {
            ViewBag.HelloWorld = "Hello World";

            ViewBag.SomeMessage = _localizer["first"];

            return View();
        }
    }
}