using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrueDogStore.Data;
using TrueDogStore.Interfaces;
using TrueDogStore.Models;
using TrueDogStore.ViewModels;

namespace TrueDogStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;
        private readonly IAccountRepository _accountRepository;
        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor,
            IPhotoService photoService, IAccountRepository accountRepository
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
            _accountRepository = accountRepository;
    }
        private void MapUserEdit(AppUser user, EditAccountUserViewModel editAVM, ImageUploadResult? photoResult)
        {
            user.Id = editAVM.Id;
            user.UserName = editAVM.UserName;
            user.Description = editAVM.Description;
            if (photoResult != null)
            {
                user.Image = photoResult.Url.ToString();
            }
            user.Country = editAVM.Country;
           
        }
        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid) return View(loginViewModel);
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if (user != null)
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");   
                    }
                }
                //Password is incorrect
                TempData["Error"] = "Wrong credentials. Please try again";
                return View(loginViewModel); 
            }
            //User not found
            TempData["Error"] = "Wrong credentials.Please try again";
            return View(loginViewModel );
        }
        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }

            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                await _signInManager.PasswordSignInAsync(newUser, registerViewModel.Password, false, false);
                return RedirectToAction("Index", "Home");
            }
            TempData["Error"] = "Сheck password restrictions";
            return View(registerViewModel);
        }
        public async Task<IActionResult> EditUserProfile()
        {    
          var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _accountRepository.GetUserByIdAsync(curUserId);
            if (user == null) return View("Error");
            var editAccountUserViewModel = new EditAccountUserViewModel()
            {
                Id = curUserId,
                UserName = user.UserName,
                Description = user.Description,
                Country = user.Country,
                ProfileImageUrl = user.Image,
            };
          return View(editAccountUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditAccountUserViewModel editAVM)
        {
            if (!ModelState.IsValid)
            {   
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editAVM);
            }
            var user = await _accountRepository.GetUserByIdAsyncNoTracking(editAVM.Id);
                ImageUploadResult? photoResult = null;
            if (editAVM.Image != null)
            {
                if (string.IsNullOrEmpty(user.Image))
                {

                    photoResult = await _photoService.AddPhotoAsync(editAVM.Image);

                    MapUserEdit(user, editAVM, photoResult);

                    _accountRepository.Update(user);
                    return RedirectToAction("Index");
                }
                else
                {
                    try
                    {
                        var fi = new FileInfo(user.Image);
                        var publicId = Path.GetFileNameWithoutExtension(fi.Name);
                        await _photoService.DeletePhotoAsync(publicId);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Could not delete photo");
                        return View(editAVM);
                    }
                    photoResult = await _photoService.AddPhotoAsync(editAVM.Image);
                }
            }
                    MapUserEdit(user, editAVM, photoResult);
                    _accountRepository.Update(user);
                    return RedirectToAction("DetailUserProfile");         
        }
        public async Task<IActionResult> Logout()
        {
        await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<ActionResult> DetailUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _accountRepository.GetUserByIdAsync(curUserId);
            if (user == null) return View("Error");
            var detailAccountUserViewModel = new DetailAccountUserViewModel()
            {
                Id = curUserId,
                UserName = user.UserName,
                Description = user.Description,
                Country = user.Country,
                ProfileImageUrl = user.Image,
            };
            return View(detailAccountUserViewModel);
        }
    }
}
