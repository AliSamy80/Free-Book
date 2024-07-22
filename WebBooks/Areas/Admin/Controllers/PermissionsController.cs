using Domin.Constants;
using Domin.Entity;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebBooks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PermissionsController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionsController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var claims =  _roleManager.GetClaimsAsync(role).Result.Select(x=>x.Value).ToList();
            var allPermissions = Permissions.PermissionsList()
                .Select(x => new RoleClaimsViewModel { Value = x }).ToList();
            foreach (var item in allPermissions)
            {
                if(claims.Any(x=>x == item.Value))
                    item.Selected = true;
            }
            return View(new PermissionViewModel
            {
                roleId = roleId,
                RoleName = role.Name,
                RoleClaims = allPermissions
            });
        }

        public async Task<IActionResult> Update(PermissionViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.roleId);
            var claims = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in claims)
                await _roleManager.RemoveClaimAsync(role, claim);

            var SelectedClaims = model.RoleClaims.Where(x => x.Selected).ToList();

            foreach (var claim in SelectedClaims) 
                await _roleManager.AddClaimAsync(role, new Claim(Helper.Permission,claim.Value) );

            //return RedirectToAction("Roles", "Accounts");
            return RedirectToAction("Index", new { roleId = model.roleId });
        }
    }
}
