﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ekklesia.Controlers
{
    public class TransactionController : Controller
    {
        public IActionResult List()
        {

            return View();
        }
    }
}