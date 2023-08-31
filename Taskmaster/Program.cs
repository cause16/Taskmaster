using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Taskmaster.Mappings;
using Taskmaster.Middlewares.Extensions;
using Taskmaster.Models;
using Taskmaster.Models.Context;
using Taskmaster.Services;
using Taskmaster.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSeq(builder.Configuration["ServerUrl"], builder.Configuration["SeqApiKey"]);

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
	options.Password.RequiredLength = 8;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredUniqueChars = 0;
})
  .AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.Name = "TaskmasterCookie";
	options.LoginPath = "/Account/Login";
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ICompanyDetailsCrudService, CompanyDetailsCrudService>();
builder.Services.AddScoped<IProjectTaskCrudService, ProjectTaskCrudService>();
builder.Services.AddScoped<ITaskSortService, TaskSortService>();

builder.Services.AddMvc(options =>
{
	options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.UseErrorLogging();

app.MapDefaultControllerRoute();
app.Run();
