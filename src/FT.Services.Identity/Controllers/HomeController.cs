﻿using Microsoft.AspNetCore.Mvc;
using System;
namespace FT.Services.Identity.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Ok("Distribued Frequent Flyer Identity Service");
    }
}
