﻿using Domin.Entity;
using Domin.ViewModel;
using Infrastructure.Data;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebBooks.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AccountsController : Controller
    {
        #region Declaration

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FreeBookDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        #endregion


        #region Constructor
        public AccountsController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager , FreeBookDbContext context , SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }
        #endregion


        #region Method
        [Authorize(Roles ="Admin,User")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Roles(RolesViewModel model)
        {
            if(ModelState.IsValid)
            {
                //var role = new IdentityRole
                //{
                //    Id = model.NewRole.Id,
                //    Name = model.NewRole.Name,
                //};

                // Create

                if(model.NewRole.Id == null)
                {
                    //role.Id = Guid.NewGuid().ToString();
                    var result = await _roleManager.CreateAsync(new IdentityRole(model.NewRole.Name));
                    if(result.Succeeded) // Succeeded
                        SessionMsg(Helper.Success, Resources.ResourceWeb.lbSave, Resources.ResourceWeb.lbSaveMsgRole);
                    else //Not Succeeded
                        SessionMsg(Helper.Error, Resources.ResourceWeb.lbNotSaved, Resources.ResourceWeb.lbNotSavedMsgRole);
                }
                 
                //Update


                else
                {
                    var RoleUpdate = await _roleManager.FindByIdAsync(model.NewRole.Id);
                    RoleUpdate.Id = model.NewRole.Id;
                    RoleUpdate.Name = model.NewRole.Name;
                    var Result = await _roleManager.UpdateAsync(RoleUpdate);
                    if (Result.Succeeded)
                        //succeeded
                        SessionMsg(Helper.Success, Resources.ResourceWeb.lbUpdate, Resources.ResourceWeb.lbUpdateMsgRole);
                    else
                        SessionMsg(Helper.Error, Resources.ResourceWeb.lbNotUpdate, Resources.ResourceWeb.lbNotUpdateMsgRole);
                }
            }
            //return View(model);
            return RedirectToAction("Roles");

        }

        private void SessionMsg(string msgType, string title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, msgType);
            HttpContext.Session.SetString(Helper.Title, title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                // Log or handle case where role is not found
                return NotFound("Role not found.");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Roles));
            }
            else
            {
                // Log the errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                // Handle failure (e.g., display error message)
            }
            return RedirectToAction("Roles");
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Register()
        {
            var model = new RegisterViewModel
            {
                NewRegister = new NewRegister(),
                Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
                Users = _context.vwUsers.OrderBy(x => x.Role).ToList() //_userManager.Users.OrderBy(x=>x.Name).ToList() 
            };
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
          if (ModelState.IsValid)
          {
                var file = HttpContext.Request.Form.Files;
                if (file.Count() > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                    var fileStream = new FileStream(Path.Combine(@"wwwroot/" , Helper.PathSaveImageuser , ImageName) , FileMode.Create);
                    file[0].CopyTo(fileStream);
                    model.NewRegister.ImageUser = ImageName;
                }
                var user = new ApplicationUser
                {
                    Id = model.NewRegister.Id,
                    Name = model.NewRegister.Name,
                    Email = model.NewRegister.Email,
                    UserName = model.NewRegister.Email,
                    Active = model.NewRegister.ActiveUser,
                    Image = model.NewRegister.ImageUser,
                };
                if(user.Id == null)
                {
                    //Create
                    user.Id = Guid.NewGuid().ToString();
                    var result = await _userManager.CreateAsync(user , model.NewRegister.Password);
                    if (result.Succeeded)
                    {
                        //successeded
                        var Role = await _userManager.AddToRoleAsync(user , model.NewRegister.RoleName);
                        if (Role.Succeeded)
                            SessionMsg(Helper.Success, Resources.ResourceWeb.lbSave, Resources.ResourceWeb.lbSaveMsgUser);
                        else
                            // Not successeded
                            SessionMsg(Helper.Error, Resources.ResourceWeb.lbNotSaved, Resources.ResourceWeb.lbNotSavedMsgUser);
                    }
                    else
                        //Not successeded
                        SessionMsg(Helper.Error, Resources.ResourceWeb.lbNotSaved, Resources.ResourceWeb.lbNotSavedMsgUser);
                }
                else
                {
                    //Update

                    var userUpdate = await _userManager.FindByIdAsync(user.Id);
                   
                        userUpdate.Id = model.NewRegister.Id;
                        userUpdate.Name = model.NewRegister.Name;
                        userUpdate.UserName = model.NewRegister.Email;
                        userUpdate.Email = model.NewRegister.Email;
                        userUpdate.Active = model.NewRegister.ActiveUser;
                        userUpdate.Image = model.NewRegister.ImageUser;


                    var result = await _userManager.UpdateAsync(userUpdate);
                    if(result.Succeeded)
                    {
                        var oldRole = await _userManager.GetRolesAsync(userUpdate);
                        await _userManager.RemoveFromRolesAsync(userUpdate, oldRole);
                        var AddRole =await _userManager.AddToRoleAsync(userUpdate, model.NewRegister.RoleName);
                        if (AddRole.Succeeded)
                            SessionMsg(Helper.Success, Resources.ResourceWeb.lbUpdate, Resources.ResourceWeb.lbUpdateMsgUser);
                        else
                            SessionMsg(Helper.Error, Resources.ResourceWeb.lbNotUpdate, Resources.ResourceWeb.lbNotUpdateMsgUserRole);
                    }
                    else
                        SessionMsg(Helper.Error, Resources.ResourceWeb.lbNotUpdate, Resources.ResourceWeb.lbNotUpdateMsgUser);
                }
                return RedirectToAction("Register", "Accounts");
          }
            return RedirectToAction("Register", "Accounts");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")] 
        public async Task<IActionResult> ChangePassword(RegisterViewModel model) 
        {
            //var user = await _userManager.FindByIdAsync(model.changePassword.Id);
            try
            {
                var user = await _userManager.FindByIdAsync(model.changePassword.Id);
                if (user == null)
                {
                    // Handle the case where the user does not exist
                    return NotFound(); // Or return an appropriate error response
                }
                if (user != null)
                {
                    await _userManager.RemovePasswordAsync(user);
                    var AddNewPassword = await _userManager.AddPasswordAsync(user, model.changePassword.NewPassword);
                    if (AddNewPassword.Succeeded)
                        SessionMsg(Helper.Success, Resources.ResourceWeb.lbSave, Resources.ResourceWeb.lbMsgSavedChangePassword);
                    else
                        SessionMsg(Helper.Error, Resources.ResourceWeb.lbNotSaved, Resources.ResourceWeb.lbMsgNotSavedChangePassword);

                    return RedirectToAction(nameof(Register));
                }
                return RedirectToAction(nameof(Register));
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while processing your request.");
            }
           
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = _userManager.Users.FirstOrDefault(x=>x.Id == id);
            if (user.Image != null && user.Image != Guid.Empty.ToString())
            {
                //var PathImage = Path.Combine(@"wwwroot/", Helper.PathImageuser, user.Image);
                var PathImageA = Path.Combine("wwwroot", Helper.PathImageuser, user.Image);
                var PathImage = PathImageA.Replace("\\", "/");
                //if (System.IO.File.Exists(PathImage))
                //    System.IO.File.Delete(PathImage);
                // Check if the file exists before attempting deletion
                if (System.IO.File.Exists(PathImageA))
                {
                    try
                    {
                        System.IO.File.Delete(PathImageA);
                    }
                    catch (Exception ex)
                    {
                        // Handle potential file deletion errors (e.g., log, display error message)
                        ModelState.AddModelError("", "An error occurred while deleting the user's image: " + ex.Message);
                        return View("DeleteUser", user); // Re-render the view with error
                    }
                }
            }
            if ((await _userManager.DeleteAsync(user)).Succeeded)
                return RedirectToAction("register", "Accounts");
            return RedirectToAction("register", "Accounts");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    ViewBag.Errorlogin = false;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LoginViewModel model)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
    }
}
