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
                return RedirectToAction(nameof(Index));

            }
            if(roomNumberExists)
            {
                TempData["error"]="The Villa Number already Exists.";
               
            }
            objVilla.VillaList= _context.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value=u.Id.ToString(),
            });
            return View(objVilla);
        }

        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList=_context.Villas.ToList().Select(u => new SelectListItem
                {
                    Text=u.Name,
                    Value=u.Id.ToString(),
                }),
                VillaNumber=_context.VillaNumbers.FirstOrDefault(u => u.Villa_Number==villaNumberId)
            };
           
            if (villaNumberVM.VillaNumber == null)
            {

                return RedirectToAction("Error","Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Update(VillaNumberVM objVilla)
        {
            if (ModelState.IsValid && objVilla.VillaNumber.Villa_Number >0)
            {
                _context.Update(objVilla.VillaNumber);
                _context.SaveChanges();
                TempData["success"]="The Villa Number is updated successfully.";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                TempData["error"]="The Villa Number Could not be updated.";
               
            }
            objVilla.VillaList= _context.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value=u.Id.ToString(),
            });
            return View(objVilla);
        }
        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList=_context.Villas.ToList().Select(u => new SelectListItem
                {
                    Text=u.Name,
                    Value=u.Id.ToString(),
                }),
                VillaNumber=_context.VillaNumbers.FirstOrDefault(u => u.Villa_Number==villaNumberId)
            };

            if (villaNumberVM.VillaNumber == null)
            {

                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Delete(VillaNumberVM objVilla)
        {
            VillaNumber? villaNumber = _context.VillaNumbers.FirstOrDefault(u=>u.Villa_Number==objVilla.VillaNumber.Villa_Number);
            if (villaNumber is not null)
            {
                _context.VillaNumbers.Remove(villaNumber);
                _context.SaveChanges();
                TempData["success"]="The Villa Number is deleted successfully.";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                TempData["error"]="The Villa Number Could not be deleted.";

            }
           
            return View();
        }
    }
}
