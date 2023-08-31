using Taskmaster.Models;

namespace Taskmaster.Services.Interfaces;

public interface ICompanyDetailsCrudService
{
    IQueryable<CompanyDetails> GetCompanyDetails(string companyName);
}
