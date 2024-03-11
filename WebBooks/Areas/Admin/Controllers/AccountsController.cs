using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebBooks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountsController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Roles()
        {

            return View(new RolesViewModel()
            {
                NewRole = new NewRole(),
                Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList()
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Roles(RolesViewModel model)
        {
            if(ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Id = model.NewRole.Id,
                    Name = model.NewRole.Name,
                };
                // Create
                if(role.Id == null)
                {
                    role.Id = Guid.NewGuid().ToString();
                    var result = await _roleManager.CreateAsync(role);
                    if(result.Succeeded)
                    {
                        //succeeded

                    }
                    else
                    {
                        // Not succeeded
                    }
                }
                //Update
                else
                {

                }
            }
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }
    }
}
