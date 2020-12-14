using Doggo.Models;
using Doggo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doggo.Controllers
{
    public class DogController : Controller
    {
        private IDogRepository _dogRepo;

        public DogController(IDogRepository dogRepo)
        {
            _dogRepo = dogRepo;
        }

        // GET: DogController
        // /dog/index

        public ActionResult Index()
        {
            List<Dog> dogs = _dogRepo.GetDogs();

            return View(dogs);
        }

        // GET: DogController/Details/5
        // /dog/details/{id}
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DogController/Create
        // /dog/create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogController/Create
        // /dog/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DogController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
