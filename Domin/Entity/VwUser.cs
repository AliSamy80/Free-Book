using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class VwUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; }
    }
}
