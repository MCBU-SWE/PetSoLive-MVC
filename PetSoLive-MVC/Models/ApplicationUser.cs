using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PetSoLive_MVC.ViewModels;

namespace PetSoLive_MVC.Models
{
    //Uses EntityFramework IdentityUser class
    public class ApplicationUser : IdentityUser
    {
    }
}
