﻿using PetSoLive_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSoLive_MVC.ViewModels
{
    public class PetDetailsViewModel
    {
        public Pet Pet { get; set; }

        //Needs to run asynchronously
        //public string ModifyImageKey()
        //{
        //    string newKey;
        //    if (Pet != null)
        //    {

        //        string currentKey = Pet.ImageKey;
        //        currentKey = Pet.ImageKey.Substring(1, currentKey.Length-1);
        //        newKey = "../.." + currentKey;
        //    }
        //    return newKey;
        //}
    }
}
