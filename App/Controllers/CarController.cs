using App.Models;
using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class CarController: Controller
    {
        private readonly CarService _service;

        public CarController(CarService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View(_service.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car c)
        {
            if(ModelState.IsValid)
            {
                _service.Create(c);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public ActionResult Edit(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var car = _service.Get(id);
            if(car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (string id, Car c)
        {
            if(c.Id != id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _service.Update(id,c);
                return RedirectToAction(nameof(Index));
            }
            else{
                return View(c);
            }
        }

        public ActionResult Detailes(string id)
        {
            if(id == null)
            {
                return NotFound();
            }   
            var car = _service.Get(id);
            if(car == null)
            {
                return NotFound();
            }
            return View();
        }

         public ActionResult Delete(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var car = _service.Get(id);
            if(car == null)
            {
                return NotFound();
            }
            return View(car);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var car = _service.Get(id);
            if(car == null)
            {
                return NotFound();
            }
            _service.Remove(car.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}