﻿using NewAPIProject.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAPIProject.Models
{
    public class Company:BasicModel
    {
        public String name { get; set;}

        public bool isActive { get; set; }

        public int CompanyUserId { get; set; }

        public ApplicationUser CompanyUser { get; set; }
    }
}