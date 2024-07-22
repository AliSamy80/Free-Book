using Domin.Entity;
using Domin.ViewModel;
using Infrastructure.IReprository;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebBooks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        #region Declaration
        private readonly IServicesReprository<Category> _servicesCategory;
        private readonly IServicesReprositoryLog<LogCategory> _servicesCategoryLog;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion



        #region Constructor
        public CategoriesController(IServicesReprository<Category> servicesCategory,
            IServicesReprositoryLog<LogCategory> servicesCategoryLog,
            UserManager<ApplicationUser> userManager)
        {
            _servicesCategory = servicesCategory;
            _servicesCategoryLog = servicesCategoryLog;
            _userManager = userManager;
        }
        #endregion



        #region Method
        public IActionResult Categories()
        {
            return View( new CategoryViewModel
            {
                Categories = _servicesCategory.GetAll(),
                LogCategories = _servicesCategoryLog.GetAll(),
                NewCategory = new Category()
            });
        }

        public IActionResult Delete(Guid Id)
        {
            var userId = _userManager.GetUserId(User);
            if (_servicesCategory.Delete(Id) && _servicesCategoryLog.Delete(Id, Guid.Parse(userId))) 
            {
                return RedirectToAction(nameof(Categories));
            }
            return RedirectToAction(nameof(Categories));
        }

        public IActionResult DeleteLog(Guid Id)
        {
            if ( _servicesCategoryLog.DeleteLog(Id))
            {
                return RedirectToAction(nameof(Categories));
            }
            return RedirectToAction(nameof(Categories));
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Save(CategoryViewModel model)
        {

            //if (ModelState.IsValid)
            //{
                var userId = _userManager.GetUserId(User);

                if (model.NewCategory.Id == Guid.Parse(Guid.Empty.ToString()))
                {
                    //Save
                    if (_servicesCategory.FindBy(model.NewCategory.Name) != null)
                    {
                        SessionMsg(Helper.Error, Resources.ResourceWeb.lbNotSaved, Resources.ResourceWeb.lbMsgDuplicateNameCategory);
                    }
                    else
                    {
                        if(_servicesCategory.Save(model.NewCategory) 
                            && _servicesCategoryLog.Save(model.NewCategory.Id,  Guid.Parse(userId)))
                        {
                            SessionMsg(Helper.Success, Resources.ResourceWeb.lbSave, Resources.ResourceWeb.lbMsgSaveCategory);
                        }
                        else
                        {
                            SessionMsg(Helper.Error, Resources.ResourceWeb.lbNotSaved, Resources.ResourceWeb.lbMsgNotSavedCategory);
                        }
                    }
                }
                else
                {
                    //Update
                    if (_servicesCategory.Save(model.NewCategory) 
                        && _servicesCategoryLog.Update(model.NewCategory.Id, Guid.Parse(userId)))
                    {
                        SessionMsg(Helper.Success, Resources.ResourceWeb.lbUpdate, Resources.ResourceWeb.lbMsgUpdateCategory);
                    }
                    else
                    {
                        SessionMsg(Helper.Error, Resources.ResourceWeb.lbNotSaved, Resources.ResourceWeb.lbMsgNotUpdatedCategory);
                    }
                }
            //}
            //// If we got this far, something failed; redisplay form with validation errors
            //foreach (var key in ModelState.Keys)
            //{
            //    var modelStateEntry = ModelState[key];
            //    if (modelStateEntry.Errors.Any())
            //    {
            //        foreach (var error in modelStateEntry.Errors)
            //        {
            //            // Log or debug the error messages
            //            Console.WriteLine($"Model Error: {key} - {error.ErrorMessage}");
            //        }
            //    }
            //}
            return RedirectToAction(nameof(Categories));

        }

        private void SessionMsg(string msgType, string title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, msgType);
            HttpContext.Session.SetString(Helper.Title, title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }

        #endregion
    }
}
