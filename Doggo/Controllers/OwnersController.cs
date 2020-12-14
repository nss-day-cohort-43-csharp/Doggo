using Doggo.Models;
using Doggo.Models.ViewModels;
using Doggo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public OwnersController(IOwnerRepository ownerRepo, IDogRepository dogRepo, IWalkerRepository walkerRepo, INeighborhoodRepository neighborhoodRepo)
        {
            _ownerRepo = ownerRepo;
            _dogRepo = dogRepo;
            _walkerRepo = walkerRepo;
            _neighborhoodRepo = neighborhoodRepo;
        }

        // GET: OwnerController
        public ActionResult Index()
        {
            List<Owner> allOwners = _ownerRepo.GetOwners();

            return View(allOwners);
        }

        // GET: OwnerController/Details/5
        public ActionResult Details(int id)
        {
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
        public ActionResult Create(OwnerFormViewModel viewModel)
        {
            try
            {
                _ownerRepo.AddOwner(viewModel.Owner);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                viewModel.ErrorMessage = "Woops! Something went wrong while saving this owner";
                viewModel.NeighborhoodOptions = _neighborhoodRepo.GetAll();

                return View(viewModel);
            }


        }

        // GET: OwnerController/Edit/5
        // owners/edit/5
        public ActionResult Edit(int id)
        {
            Owner owner = _ownerRepo.GetById(id);

            return View(owner);
        }

        // POST: OwnerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Owner owner)
        {
            try
            {
                _ownerRepo.UpdateOwner(owner);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(owner);
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
    }
}
