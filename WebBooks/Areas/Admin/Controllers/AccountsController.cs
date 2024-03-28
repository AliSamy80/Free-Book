using Infrastructure.Data;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Http;
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
                        HttpContext.Session.SetString("msgType","success");
                        HttpContext.Session.SetString("title", "تـم الحفــظ");
                        HttpContext.Session.SetString("msg", "تم حفظ مجموعة المستخــدم بنجــاح");

                        return RedirectToAction("Roles");
                    }
                    else
                    {
                        HttpContext.Session.SetString("msgType", "error");
                        HttpContext.Session.SetString("title", "لـم يتم الحفــظ");
                        HttpContext.Session.SetString("msg", "لـم يتم حفظ مجموعة المستخــدم بنجــاح");

                        // Not succeeded
                    }
                }
                //Update
                else
                {
                    var RoleUpdate = await _roleManager.FindByIdAsync(role.Id);
                    RoleUpdate.Id = model.NewRole.Id;
                    RoleUpdate.Name = model.NewRole.Name;
                    var Result = await _roleManager.UpdateAsync(RoleUpdate);
                    if (Result.Succeeded)
                    {
                        //succeeded
                        HttpContext.Session.SetString("msgType", "success");
                        HttpContext.Session.SetString("title", "تـم التعديــل");
                        HttpContext.Session.SetString("msg", "تم تعديل مجموعة المستخــدم بنجــاح");
                    }
                    else
                    {
                        HttpContext.Session.SetString("msgType", "error");
                        HttpContext.Session.SetString("title", "لـم يتم التعديل");
                        HttpContext.Session.SetString("msg", "لـم يتم تعديل مجموعة المستخــدم بنجــاح");
                    }
                }
            }
            //return View(model);
            return View();

        }
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == Id);
            if((await _roleManager.DeleteAsync(role)).Succeeded)
            {
                return RedirectToAction(nameof(Roles));
            }
            return RedirectToAction("Roles");
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
