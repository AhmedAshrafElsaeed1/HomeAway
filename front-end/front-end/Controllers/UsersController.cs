using front_end.Interfaces;
using front_end.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace front_end.Controllers
{
    public class UsersController : Controller
    {
        IAdminService _adminService;
        public UsersController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _adminService.GetAllUsersAsync();
                UsersViewModel usersViewModel = new UsersViewModel()
                {
                    Users = users
                };
                return View(usersViewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }

        }
        public async Task<IActionResult> Ban(string id)
        {
            try
            {
                if (await _adminService.DeleteUserAsync(id))
                {

                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
    }
}
