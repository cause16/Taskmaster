using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskmaster.Services.Interfaces;
using Taskmaster.ViewModels;

namespace Taskmaster.Controllers;

public class HomeController : Controller
{
	[HttpGet]
	public IActionResult Index()
	{
		return View();
	}

	[HttpGet]
	public async Task<IActionResult> Contacts([FromServices] ICompanyDetailsCrudService companyDetailsCrudService,
			[FromServices] IMapper mapper, [FromServices] IConfiguration configuration)
	{
		string companyName = configuration["CompanyName"] ?? "";

		var companyDetails = await companyDetailsCrudService.GetCompanyDetails(companyName)
			.FirstOrDefaultAsync();

		if (companyDetails == null)
			return NotFound();

		var model = mapper.Map<CompanyDetailsViewModel>(companyDetails);

		return View(model);
	}
}
