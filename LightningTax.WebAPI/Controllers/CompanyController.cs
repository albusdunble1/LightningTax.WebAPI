using LightningTax.WebAPI.Data;
using LightningTax.WebAPI.Dtos;
using LightningTax.WebAPI.Helpers;
using LightningTax.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LightningTax.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DataContext _context;

        public CompanyController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetCompanies")]
        public async Task<ActionResult<GetCompaniesResponseDto>> GetCompanies()
        {
            try
            {
                var response = new GetCompaniesResponseDto();

                var companies = await _context.Companies.ToListAsync();
                //throw new Exception("test exception");

                response.Companies = companies;

                return Ok(response);
            }
            catch (Exception ex)
            {
                // TODO: log ex

                //var response = new GetCompaniesResponseDto
                //{
                //    StatusCode = ServerStatusEnum.SystemError
                //    //Errors = new List<string> { ex.Message }
                //};

                //return StatusCode(StatusCodes.Status500InternalServerError, response);
                return ResponseHelper.SystemError();
            }

        }

        [HttpGet("{companyId}")]
        public async Task<ActionResult<GetCompanyByIdResponseDto>> GetCompany(long companyId)
        {
            try
            {
                var response = new GetCompanyByIdResponseDto();

                var company = await _context.Companies.FindAsync(companyId);

                if (company == null)
                {
                    //response.StatusCode = ServerStatusEnum.NotFound;
                    ////return NotFound("Company not found");
                    //return StatusCode(StatusCodes.Status404NotFound, response);
                    return ResponseHelper.NotFound();
                }

                response.Company = company;

                return Ok(response);
            }
            catch (Exception ex)
            {
                // TODO: log ex
                return ResponseHelper.SystemError();
            }
        }

        [HttpPost("AddCompany")]
        public async Task<ActionResult<AddCompanyResponseDto>> AddCompany([FromBody] Company company)
        {
            try
            {
                var response = new AddCompanyResponseDto();

                _context.Companies.Add(company);
                await _context.SaveChangesAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                // TODO: log ex
                return ResponseHelper.SystemError();
            }
        }

        [HttpPut("UpdateCompany")]
        public async Task<ActionResult<UpdateCompanyResponseDto>> UpdateCompany([FromBody] Company requestedCompany)
        {
            try
            {
                var response = new UpdateCompanyResponseDto();

                var company = await _context.Companies.FindAsync(requestedCompany.Id);
                if (company == null)
                    return ResponseHelper.NotFound();

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

                await _context.SaveChangesAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                // TODO: log ex
                return ResponseHelper.SystemError();
            }
        }

        [HttpDelete("{companyId}")]
        public async Task<ActionResult<DeleteCompanyByIdResponseDto>> DeleteCompany(long companyId)
        {
            try
            {
                var response = new DeleteCompanyByIdResponseDto();

                var companyToDelete = await _context.Companies.FindAsync(companyId);
                if (companyToDelete == null)
                    return ResponseHelper.NotFound();

                _context.Companies.Remove(companyToDelete);
                await _context.SaveChangesAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                // TODO: log ex
                return ResponseHelper.SystemError();
            }
        }

        [HttpGet("GetCompanyNames")]
        public async Task<ActionResult<GetCompanyNamesResponseDto>> GetCompanyNames()
        {
            try
            {
                var response = new GetCompanyNamesResponseDto();

                var companies = await _context.Companies.ToListAsync();
                var companyNames = companies.Select(x => x.Name).ToList();

                response.CompanyNames = companyNames;

                return Ok(response);
            }
            catch (Exception ex)
            {
                // TODO: log ex
                return ResponseHelper.SystemError();
            }
        }


    }
}
