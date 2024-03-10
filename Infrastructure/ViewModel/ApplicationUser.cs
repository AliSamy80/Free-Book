using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.ViewModel
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
    }
}
