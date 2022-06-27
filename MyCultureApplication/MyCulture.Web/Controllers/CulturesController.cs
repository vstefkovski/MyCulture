using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCulture.Domain.DomainModels;
using MyCulture.Domain.DTO;
using MyCulture.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyCulture.Web.Controllers
{
    public class CulturesController : Controller
    {
        private readonly ICultureService _cultureService;

        public CulturesController(ICultureService cultureService)
        {
            _cultureService = cultureService;
        }

        // GET: Products
        public IActionResult Index()
        {
            var allCultures = this._cultureService.GetAllCultures();
            return View(allCultures);
        }
        public IActionResult AddCultureToCart(Guid? id)
        {
            var model = this._cultureService.GetCultureCartInfo(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCultureToCart([Bind("CultureId", "Quantity")] AddToCultureCartDto item)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._cultureService.AddToCultureCart(item, userId);

            if (result)
            {
                return RedirectToAction("Index", "Cultures");
            }

            return View(item);
        }


        // GET: Products/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var culture = this._cultureService.GetDetailsForCulture(id);
            if (culture == null)
            {
                return NotFound();
            }

            return View(culture);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CultureName,CultureImage,CultureDescription,CulturePrice,Rating")] Culture culture)
        {
            if (ModelState.IsValid)
            {

                this._cultureService.CreateNewCulture(culture);
                return RedirectToAction(nameof(Index));

            }
            return View(culture);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var culture = this._cultureService.GetDetailsForCulture(id);
            if (culture == null)
            {
                return NotFound();
            }
            return View(culture);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,CultureName,CultureImage,CultureDescription,CulturePrice,Rating")] Culture culture)
        {
            if (id != culture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._cultureService.UpdateExistingCulture(culture);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(culture.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(culture);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var culture = this._cultureService.GetDetailsForCulture(id);
            if (culture == null)
            {
                return NotFound();
            }

            return View(culture);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._cultureService.DeleteCulture(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            return this._cultureService.GetDetailsForCulture(id) != null;

        }
    }
}
