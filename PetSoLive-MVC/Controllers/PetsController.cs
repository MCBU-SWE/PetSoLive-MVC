using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PetSoLive_MVC.Models;
using PetSoLive_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace PetSoLive_MVC.Controllers
{
    public class PetsController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public PetsController(ApplicationContext db, UserManager<ApplicationUser> userManager, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _db = db;
            _userManager = userManager;
            _env = env;
        }

        public IActionResult Index(string sortOrder)
        {
            if (User.Identity.IsAuthenticated)
            {
                var vm = new PetsIndexViewModel
                {
                    AllPets = _db.Pets.Include(s => s.Species).Include(s => s.Breed).OrderByDescending(s => s.Id).ToList()
                };

                if (!vm.AllPets.Any())
                {
                    return RedirectToAction("Add", "Pets");
                }

                if (vm.AllPets.Count() == 1)
                {
                    vm.OnePet = vm.AllPets.First();
                    return View(vm);
                }

                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Add()
        {
            if (User.Identity.IsAuthenticated)
            {
                var vm = new AddPetViewModel
                {
                    Species = _db.Species.Skip(2).Take(2).Select(s => new SelectListItem
                    {
                        Text = s.DisplayName,
                        Value = s.Id.ToString()
                    }),
                    Pet = new Pet
                    {
                        AddedBy = _userManager.GetUserId(HttpContext.User)
                    }
                };

                ViewBag.BreedList = new SelectList(new List<Breed>());
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Add(AddPetViewModel vm)
        {
            if (vm.Pet.ImageKey == null)
            {
                vm.Pet.ImageKey = "../images/defaultpet.png";
            }

            _db.Pets.Add(vm.Pet);
            _db.SaveChanges();
            return RedirectToAction("Index", "Pets");
        }

        public JsonResult PopulateBreedList(string speciesVal)
        {
            var selectedSpecies = _db.Species.FirstOrDefault(s => s.Id == int.Parse(speciesVal));
            var breedsList = _db.Breeds.Where(b => b.SpeciesId == selectedSpecies.Id).ToList();
            return Json(breedsList);
        }

        public IActionResult Details(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var thisPet = _db.Pets.Include(s => s.Species).Include(s => s.Breed).FirstOrDefault(p => p.Id == id);
                var vm = new PetDetailsViewModel
                {
                    Pet = thisPet
                };

                return View(vm);
            }

            return RedirectToAction("Home", "Index");
        }

        [HttpPost]
        public IActionResult AddBreed(int speciesId, string name)
        {
            var newBreed = new Breed { SpeciesId = speciesId, Name = name };
            _db.Breeds.Add(newBreed);
            _db.SaveChanges();
            return Json(newBreed);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var vm = new EditPetViewModel
                {
                    Pet = _db.Pets.Include(p => p.Species).Include(p => p.Breed).FirstOrDefault(p => p.Id == id)
                };

                var breedList = _db.Breeds.Where(s => s.SpeciesId == vm.Pet.SpeciesId).ToList();
                vm.BreedSelectList = breedList.Select(b => new SelectListItem
                {
                    Text = b.Name,
                    Value = b.Id.ToString()
                });

                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Edit(EditPetViewModel vm)
        {
            _db.Entry(vm.Pet).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index", "Pets");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var thisPet = _db.Pets.FirstOrDefault(p => p.Id == id);
            _db.Pets.Remove(thisPet);
            _db.SaveChanges();
            return RedirectToAction("Index", "Pets");
        }
    }
}