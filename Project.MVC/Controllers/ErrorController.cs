using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;



namespace Project.MVC.Controllers
{
    /// <summary>
    /// Kontroler klasa za dohvat grešaka 4xx
    /// </summary>
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpErrorCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Resurs koji tražite nije pronađen";
                    break;

            }

            return View("NotFound");
        }
    }
}
