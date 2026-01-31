using LightningTax.WebAPI.Data;
using LightningTax.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LightningTax.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DataContext context;

        public CompanyController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("companies")]
        public async Task<ActionResult<List<Company>>> GetCompanies()
        {
            return Ok(await context.Companies.ToListAsync());
        }

        [HttpGet("{companyId}")]
        public async Task<ActionResult<List<Company>>> GetCompany(long companyId)
        {
            var company = await context.Companies.FindAsync(companyId);
            if (company == null)
                return NotFound("Company not found");

            return Ok(company);
        }

        [HttpPost("add")]
        public async Task<ActionResult<List<Company>>> AddCompany([FromBody] Company company)
        {
            context.Companies.Add(company);
            await context.SaveChangesAsync();

            return Ok(await context.Companies.FindAsync(company.Id));
        }

        [HttpPut("edit")]
        public async Task<ActionResult<List<Company>>> UpdateCompany([FromBody] Company requestedCompany)
        {
            var company = await context.Companies.FindAsync(requestedCompany.Id);
            if (company == null)
                return NotFound("Company not found");

            //company = requestedCompany;
            company.CompanyNumber = requestedCompany.CompanyNumber;
            company.Name = requestedCompany.Name;
            company.ResidentStatus = requestedCompany.ResidentStatus;
            company.BusinessType = requestedCompany.BusinessType;
            company.PaidUpCapital = requestedCompany.PaidUpCapital;
            company.SmeEligible = requestedCompany.SmeEligible;
            company.FinancialYearEnd = requestedCompany.FinancialYearEnd;
            company.CommencementDate = requestedCompany.CommencementDate;
            company.CessationDate = requestedCompany.CessationDate;
            //company.CreatedAt = requestedCompany.CreatedAt;

            await context.SaveChangesAsync();

            return Ok(await context.Companies.ToListAsync());
        }

        [HttpDelete("{companyId}")]
        public async Task<ActionResult<List<Company>>> DeleteCompany(long companyId)
        {
            var dbcompany = await context.Companies.FindAsync(companyId);
            if (dbcompany == null)
                return NotFound("Company not found");

            context.Companies.Remove(dbcompany);
            await context.SaveChangesAsync();

            return Ok(await context.Companies.ToListAsync());
        }

        [HttpGet("companyNames")]
        public async Task<ActionResult<object>> GetCompanyNames()
        {
            var x = await context.Companies.ToListAsync();
            var y = x.Select(x => x.Name);

            return Ok(y);
        }
    }
}
