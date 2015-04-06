﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _3DMapleSystem.Data.Models;
using _3DMapleSystem.Web.ViewModels.PolyModels;

namespace _3DMapleSystem.Web.ViewModels.ComplexModels
{
    public class DetailsComplexModel
    {
        public PolyModelDetailsViewModel PolyModel { get; set; }

        public UserViewModel User { get; set; }

        public string SizeOfFileModel { get; set; }
    }
}