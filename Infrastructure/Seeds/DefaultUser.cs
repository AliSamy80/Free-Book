using Domin.Constants;
using Domin.Entity;
using Domin.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Domin.Entity.Helper;

namespace Infrastructure.Seeds
{
    public static class DefaultUser
    {
        public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var DefaultUser = new ApplicationUser
            {
                UserName = Helper.UserNameBasic,
                Email = Helper.EmailBasic,
                Name = Helper.NameBasic,
                Image = "user-1.png",
                Active = true,
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(DefaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(DefaultUser, Helper.PasswordBasic);
                await userManager.AddToRoleAsync(DefaultUser, Helper.Roles.Basic.ToString());
                //await userManager.AddToRolesAsync(DefaultUser, new List<string> { Helper.Roles.SuperAdmin.ToString(), Helper.Roles.Admin.ToString() });
            }
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var DefaultUser = new ApplicationUser
            {
                UserName = Helper.UserName,
                Email = Helper.Email,
                Name = Helper.Name,
                Image = "user-1.png",
                Active = true,
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(DefaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(DefaultUser, Helper.Password);
                await userManager.AddToRoleAsync(DefaultUser, Helper.Roles.SuperAdmin.ToString());
                //await userManager.AddToRolesAsync(DefaultUser, new List<string> { Helper.Roles.SuperAdmin.ToString(), Helper.Roles.Admin.ToString() });
            }

            // Code Seeding Claims

            await roleManager.SeedClaimsAsync();
        }

        //Method Number 2  Used in SeedSuperAdminAsync Method
        public static async Task SeedClaimsAsync( this RoleManager<IdentityRole> roleManager) // reflection Method // this => عشان لما اكتب رول مانجر . يبدأ يعرضلي الميثود دي من ضمن الاختيارات
        {
            var adminRole = await roleManager.FindByNameAsync(Helper.Roles.SuperAdmin.ToString());

            //Code Add Permission Claims
            var modules = Enum.GetValues(typeof(PermissionModuleName));
            foreach (var module in modules)
                await roleManager.AddPermissionClaims(adminRole, module.ToString());
        }


        //Method Number 1 used in Method Number 2
        public static async Task AddPermissionClaims(this RoleManager<IdentityRole> roleManager,IdentityRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsFromModule(module);
            foreach (var permission in allPermissions) 
            {
                if (!allClaims.Any(x => x.Type == Helper.Permission && x.Value == permission))
                    await roleManager.AddClaimAsync(role, new Claim(Helper.Permission, permission));
                
            }
        }

    }
}
