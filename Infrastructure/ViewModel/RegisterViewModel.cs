using Domin.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModel
{
    public class RegisterViewModel
    {
    }
    public class NewRegister
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public string ImageUser { get; set; }
        public bool ActiveUser { get; set; }
        public string Password { get; set; }
        public string ComparePassword { get; set; }
    }

}
