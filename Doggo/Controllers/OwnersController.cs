using Doggo.Models;
using Doggo.Models.ViewModels;
using Doggo.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// URL: /owners/create


namespace Doggo.Controllers
{
    public class OwnersController : Controller
    {
        private IOwnerRepository _ownerRepo;
        private IDogRepository _dogRepo;
        private IWalkerRepository _walkerRepo;
        private INeighborhoodRepository _neighborhoodRepo;

        public OwnersController(
            IOwnerRepository ownerRepository,
            IDogRepository dogRepo,
            IWalkerRepository walkerRepo,
            INeighborhoodRepository neighborhoodRepo)
        {
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepo;
            _walkerRepo = walkerRepo;
            _neighborhoodRepo = neighborhoodRepo;
        }

        // GET: OwnerController
        public ActionResult Index()
        {
            int currentUserId = GetCurrentUserId();

            return RedirectToAction("Details", new { id = currentUserId });
        }

        // GET: OwnerController/Details/5
        public ActionResult Details(int id)
        {
            int currentUserId = GetCurrentUserId();

            if (currentUserId != id)
            {
                return NotFound();
            }

            Owner owner = _ownerRepo.GetById(id);
            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(owner.Id);
            List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(owner.NeighborhoodId);

            OwnerProfileViewModel vm = new OwnerProfileViewModel()
            {
                Owner = owner,
                Dogs = dogs,
                WalkersInNeighbordhood = walkers
            };

            if (owner == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // GET: OwnerController/Create
        public ActionResult Create()
        {
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

            OwnerFormViewModel vm = new OwnerFormViewModel()
            {
                NeighborhoodOptions = neighborhoods,
                Owner = new Owner()
            };

            return View(vm);
        }

        // POST: OwnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OwnerFormViewModel vm)
        {
            try
            {
                _ownerRepo.AddOwner(vm.Owner);

                return RedirectToAction("Details", new { id = vm.Owner.Id });
            }
            catch (Exception ex)
            {
                vm.ErrorMessage = "Woops! Something went wrong while saving this owner";
                vm.NeighborhoodOptions = _neighborhoodRepo.GetAll();

                return View(vm);
            }


        }

        // GET: OwnerController/Edit/5
        // owners/edit/5
        public ActionResult Edit(int id)
        {
            Owner owner = _ownerRepo.GetById(id);
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

            OwnerFormViewModel vm = new OwnerFormViewModel()
            {
                NeighborhoodOptions = neighborhoods,
                Owner = owner
            };

            return View(vm);
        }

        // POST: OwnerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, OwnerFormViewModel vm)
        {
            try
            {
                _ownerRepo.UpdateOwner(vm.Owner);

                return RedirectToAction("Index");
            }
            catch
            {
                vm.ErrorMessage = "Woops! Something went wrong while saving this owner";
                vm.NeighborhoodOptions = _neighborhoodRepo.GetAll();
                return View(vm);
            }
        }

        // GET: OwnerController/Delete/5
        public ActionResult Delete(int id)
        {
            Owner owner = _ownerRepo.GetById(id);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: OwnerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Owner owner)
        {
            try
            {
                _ownerRepo.DeleteOwner(id);

                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel viewModel)
        {
            Owner owner = _ownerRepo.GetOwnerByEmail(viewModel.Email);

            if (owner == null)
            {
                return Unauthorized();
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, owner.Id.ToString()),
                new Claim(ClaimTypes.Email, owner.Email),
                new Claim(ClaimTypes.Role, "DogOwner"),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Dog");
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
