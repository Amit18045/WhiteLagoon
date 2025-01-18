using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.Web.Models.ViewModel;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VillaNumberController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //here include use for get villa name
            var villaNumber = _context.VillaNumbers.Include(u=>u.Villa).ToList();
            return View(villaNumber);
        }

        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
                {
                VillaList= _context.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value=u.Id.ToString(),
                })
            };
          
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Create(VillaNumberVM objVilla)
        {
           // ModelState.Remove("Villa");
           bool roomNumberExists=_context.VillaNumbers.Any(u=>u.Villa_Number==objVilla.VillaNumber.Villa_Number);

            if (ModelState.IsValid && !roomNumberExists)
            {
                _context.Add(objVilla.VillaNumber);
                _context.SaveChanges();
                TempData["success"]="The Villa Number is created successfully.";
                return RedirectToAction("Index");

            }
            if(roomNumberExists)
            {
                TempData["error"]="The Villa Number already Exists.";
               
            }
            return View(objVilla);
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
                TempData["success"]="The Villa Number is updated successfully.";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["error"]="The Villa Number Could not be updated.";
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
                TempData["success"]="The Villa Number is deleted successfully.";
                return RedirectToAction("Index");

            }
            else
            {
                TempData["error"]="The Villa Number Could not be Deleted.";
                return View(objVilla);
            }
        }
    }
}
