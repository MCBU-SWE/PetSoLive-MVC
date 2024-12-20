using Microsoft.AspNetCore.Mvc.Rendering;
using PetSoLive_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSoLive_MVC.ViewModels
{
    public class AddPetViewModel
    {
        public Pet Pet { get; set; }

        public IEnumerable<SelectListItem> Breeds { get; set; }
        public IEnumerable<SelectListItem> Species { get; set; }

        public IEnumerable<SelectListItem> PetSizes { get; set; }
    }
}
