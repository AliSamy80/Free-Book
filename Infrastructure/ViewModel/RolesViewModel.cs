﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel
{
    public class RolesViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public NewRole NewRole { get; set; }
    }
    public class NewRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}