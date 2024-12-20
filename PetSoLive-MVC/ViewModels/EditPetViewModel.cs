using Microsoft.AspNetCore.Mvc.Rendering;
using PetSoLive_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSoLive_MVC.ViewModels
{
    public class EditPetViewModel
    {
        public Pet Pet { get; set; }
        public IEnumerable<SelectListItem> BreedSelectList { get; set; }
    }
}
