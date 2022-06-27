using Microsoft.AspNetCore.Mvc;
using MyCulture.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCulture.Web.Controllers
{
    public class CultureCartController : Controller
    {
        private readonly ICultureCartService _cultureCartService;

        public CultureCartController(ICultureCartService cultureCartService)
        {
            _cultureCartService = cultureCartService;
        }


        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(this._cultureCartService.getCultureCartInfo(userId));
        }
        public IActionResult DeleteFromCultureCart(Guid id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._cultureCartService.deleteCultureFromCultureCart(userId, id);

            if (result)
            {
                return RedirectToAction("Index", "CultureCart");
            }
            else
            {
                return RedirectToAction("Index", "CultureCart");
            }
        }

        public IActionResult OrderNow()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._cultureCartService.orderNow(userId);
            if (result)
            {
                return RedirectToAction("Index", "CultureCart");
            }
            else
            {
                return RedirectToAction("Index", "CultureCart");
            }
        }
    }
}
