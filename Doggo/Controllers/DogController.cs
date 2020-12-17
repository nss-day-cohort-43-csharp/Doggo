using Doggo.Models;
using Doggo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Doggo.Controllers
{
    [Authorize]
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
            // refactor to only get dogs for the currently logged in user
            int currentUserId = GetCurrentUserId();

            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(currentUserId);

            return View(dogs);
        }

        // GET: DogController/Details/5
        // /dog/details/{id}
        public ActionResult Details(int id)
        {
            Dog dog = _dogRepo.GetById(id);

            int currentUserId = GetCurrentUserId();

            if (dog.OwnerId != currentUserId)
            {
                return NotFound();
            }

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
                int currentUserId = GetCurrentUserId();
                dog.OwnerId = currentUserId;

                _dogRepo.AddDog(dog);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(dog);
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

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
