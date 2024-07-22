using Domin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsFromModule(string module) //module take values equal the mname of Controllers Like {Categories , Books}
        {
            return new List<string>()
            {
                $"Permission.{module}.View",
                $"Permission.{module}.Creat",
                $"Permission.{module}.Edit",
                $"Permission.{module}.Delete"
            };
        }

        public static List<string> PermissionsList()
        {
            var allPermeissions = new List<string>();
            foreach (var module in Enum.GetValues(typeof(Helper.PermissionModuleName)))
            {
                allPermeissions.AddRange(GeneratePermissionsFromModule(module.ToString()));
            }
            return allPermeissions;
        }
    }
}
