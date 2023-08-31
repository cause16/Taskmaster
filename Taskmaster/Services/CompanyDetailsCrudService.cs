using Taskmaster.Models;
using Taskmaster.Models.Context;
using Taskmaster.Services.Interfaces;

namespace Taskmaster.Services;

public class CompanyDetailsCrudService : ICompanyDetailsCrudService
{
    private readonly AppDbContext _context;

    public CompanyDetailsCrudService(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<CompanyDetails> GetCompanyDetails(string companyName)
	{
        return _context.CompanyDetails.Where(cd => cd.Name == companyName);
    }
}
