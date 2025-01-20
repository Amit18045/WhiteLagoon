using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaRepository _villaRepository;
        public VillaController(IVillaRepository villaRepository)
        {
            _villaRepository = villaRepository;
        }
        public IActionResult Index()
        {
            var villas = _villaRepository.GetAll();
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
               _villaRepository.Add(objVilla);
                _villaRepository.save();
                TempData["success"]="The Villa is created successfully.";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                TempData["error"]="The Villa Could not be created.";
                return View(objVilla);
            }
        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _villaRepository.Get(u => u.Id==villaId);
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
                _villaRepository.Update(objVilla);
                _villaRepository.save();
                TempData["success"]="The Villa is updated successfully.";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                TempData["error"]="The Villa Could not be updated.";
                return View(objVilla);
            }
        }
        public IActionResult Delete(int villaId)
        {
            Villa? obj = _villaRepository.Get(u => u.Id==villaId);
            if (obj == null)
            {

                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa objVilla)
        {
            Villa? objVillaFromDb = _villaRepository.Get(u => u.Id==objVilla.Id);
            if (objVillaFromDb is not null)
            {
                _villaRepository.Remove(objVillaFromDb);
                _villaRepository.save();
                TempData["success"]="The Villa is deleted successfully.";
                return RedirectToAction(nameof(Index));

            }
            else
            {
                TempData["error"]="The Villa Could not be Deleted.";
                return View(objVilla);
            }
        }
    }
}
