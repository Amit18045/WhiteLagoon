using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VillaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var villas = _context.Villas.ToList();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa objVilla)
        {
            if (ModelState.IsValid)
            {
                _context.Add(objVilla);
                _context.SaveChanges();
                TempData["success"]="The Villa is created successfully.";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["error"]="The Villa Could not be created.";
                return View(objVilla);
            }
        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _context.Villas.FirstOrDefault(u => u.Id==villaId);
            if (obj == null)
            {

                return RedirectToAction("Error","Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Villa objVilla)
        {
            if (ModelState.IsValid && objVilla.Id>0)
            {
                _context.Update(objVilla);
                _context.SaveChanges();
                TempData["success"]="The Villa is updated successfully.";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["error"]="The Villa Could not be updated.";
                return View(objVilla);
            }
        }
        public IActionResult Delete(int villaId)
        {
            Villa? obj = _context.Villas.FirstOrDefault(u => u.Id==villaId);
            if (obj == null)
            {

                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa objVilla)
        {
            Villa? objVillaFromDb = _context.Villas.FirstOrDefault(u => u.Id==objVilla.Id);
            if (objVillaFromDb is not null)
            {
                _context.Remove(objVillaFromDb);
                _context.SaveChanges();
                TempData["success"]="The Villa is deleted successfully.";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["error"]="The Villa Could not be Deleted.";
                return View(objVilla);
            }
        }
    }
}
