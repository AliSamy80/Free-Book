using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domin.Resources;
namespace Infrastructure.ViewModel
{
    public class RolesViewModel
    {
        public List<IdentityRole> Roles { get; set; }
        public NewRole NewRole { get; set; }
    }
    public class NewRole
    {
        public string Id { get; set; }
        [Required(ErrorMessageResourceType =typeof(ResourceData), ErrorMessageResourceName = "RoleName")]
        public string Name { get; set; }
    }
}
