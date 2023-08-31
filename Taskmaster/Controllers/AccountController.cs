using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taskmaster.Models;
using Taskmaster.ViewModels;

namespace Taskmaster.Controllers;

public class AccountController : Controller
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;

	public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	[HttpGet]
	public IActionResult Login(string returnUrl)
	{
		ViewBag.ReturnUrl = returnUrl;
		var model = new LoginViewModel();

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
	{
		if (ModelState.IsValid)
		{
			var user = await _userManager.FindByNameAsync(model.UserName);

			if (user != null)
			{
				await _signInManager.SignOutAsync();
				var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

				if (signInResult.Succeeded)
				{
					return Redirect(returnUrl ?? "/");
				}
			}
			ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неправильний логін або пароль");
		}
		ViewBag.ReturnUrl = returnUrl;

		return View(model);
	}

	[HttpGet]
	public IActionResult Registration(string returnUrl)
	{
		ViewBag.ReturnUrl = returnUrl;
		var model = new RegistrationViewModel();

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Registration(RegistrationViewModel model, string returnUrl)
	{
		if (ModelState.IsValid)
		{
			var user = new User
			{
				Email = model.Email,
				UserName = model.Email,
				FirstName = model.FirstName.Trim(),
				LastName = model.LastName.Trim()
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, false);

				return Redirect(returnUrl ?? "/");
			}

			var duplicateUserNameError = result.Errors.FirstOrDefault(error => error.Code == "DuplicateUserName");

			if (duplicateUserNameError != null)
				ModelState.AddModelError(nameof(RegistrationViewModel.Email), "Користувач з таким email вже зареєстрований");
		}
		ViewBag.ReturnUrl = returnUrl;

		return View(model);
	}

	[Authorize]
	[HttpPost]
	[IgnoreAntiforgeryToken]
	public async Task<IActionResult> LogOut()
	{
		await _signInManager.SignOutAsync();

		return RedirectToAction("Index", "Home");
	}
}
