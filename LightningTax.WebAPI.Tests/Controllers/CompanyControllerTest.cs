using LightningTax.WebAPI.Controllers;
using LightningTax.WebAPI.Data;
using LightningTax.WebAPI.Dtos;
using LightningTax.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LightningTax.WebAPI.Tests.Controllers
{
    public class CompanyControllerTest : IDisposable
    {
        private readonly DataContext _context;
        private readonly CompanyController _controller;

        public CompanyControllerTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new DataContext(options);

            // Seed data
            _context.Companies.AddRange(
                new Company
                {
                    Id = 1,
                    CompanyNumber = "C001",
                    Name = "Alpha Sdn Bhd",
                    SmeEligible = true,
                    FinancialYearEnd = new DateOnly(2024, 12, 31),
                    CommencementDate = new DateOnly(2020, 1, 1)
                },
                new Company
                {
                    Id = 2,
                    CompanyNumber = "C002",
                    Name = "Beta Sdn Bhd",
                    SmeEligible = false,
                    FinancialYearEnd = new DateOnly(2024, 12, 31),
                    CommencementDate = new DateOnly(2021, 6, 1)
                }
            );

            _context.SaveChanges();
            _controller = new CompanyController(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        // -------------------------
        // GET ALL
        // -------------------------
        [Fact]
        public async Task GetCompanies_ReturnsAllCompanies()
        {
            // Act
            var result = await _controller.GetCompanies();

            // Assert
            //var ok = Assert.IsType<ActionResult<List<Company>>>(result);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var responseDto = ok.Value as GetCompaniesResponseDto;
            var companies = Assert.IsAssignableFrom<IEnumerable<Company>>(responseDto.Companies);

            Assert.Equal(2, companies.Count());
        }

        // -------------------------
        // GET ALL COMPANY NAMES
        // -------------------------
        [Fact]
        public async Task GetCompanies_ReturnsAllCompanyNames()
        {
            // Act
            var result = await _controller.GetCompanyNames();

            // Assert
            //var ok = Assert.IsType<ActionResult<List<Company>>>(result);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var responseDto = ok.Value as GetCompanyNamesResponseDto;

            //var companyNames = Assert.IsAssignableFrom<IEnumerable<string>>(ok.Value);
            var companyNames = Assert.IsAssignableFrom<IEnumerable<string>>(responseDto.CompanyNames);

            Assert.Equal(2, companyNames.Count());
        }

        // -------------------------
        // GET BY ID
        // -------------------------
        [Fact]
        public async Task GetCompany_ReturnsCompany_WhenExists()
        {
            // Arrange
            var companyId = 1;

            // Act
            var result = await _controller.GetCompany(companyId);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var responseDto = ok.Value as GetCompanyByIdResponseDto;

            //var company = Assert.IsType<Company>(ok.Value);
            var company = Assert.IsType<Company>(responseDto.Company);

            Assert.Equal("Alpha Sdn Bhd", company.Name);
        }

        [Fact]
        public async Task GetCompany_ReturnsNotFound_WhenNotExists()
        {
            // Arrange
            var invalidCompanyId = 999;

            // Act
            var result = await _controller.GetCompany(invalidCompanyId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        // -------------------------
        // ADD
        // -------------------------
        [Fact]
        public async Task AddCompany_CreatesCompanySuccessfully()
        {
            // Arrange
            var newCompany = new Company
            {
                CompanyNumber = "C003",
                Name = "Gamma Sdn Bhd",
                SmeEligible = true,
                FinancialYearEnd = new DateOnly(2024, 12, 31),
                CommencementDate = new DateOnly(2022, 1, 1)
            };

            // Act
            var result = await _controller.AddCompany(newCompany);

            // Assert
            //var created = Assert.IsType<CreatedAtActionResult>(result.Result);
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            //var responseDto = ok.Value as AddCompanyResponseDto;

            //var company = Assert.IsType<Company>(responseDto.Company);

            //Assert.Equal("Gamma Sdn Bhd", company.Name);
            //Assert.Equal("C003", company.CompanyNumber);

            // Verify persisted
            Assert.Equal(3, _context.Companies.Count());
        }

        // -------------------------
        // UPDATE
        // -------------------------
        [Fact]
        public async Task UpdateCompany_UpdatesSuccessfully()
        {
            long updatedCompanyId = 1;
            var updated = new Company
            {
                Id = 1,
                CompanyNumber = "C001",
                Name = "Alpha Updated Sdn Bhd",
                SmeEligible = true,
                FinancialYearEnd = new DateOnly(2024, 12, 31),
                CommencementDate = new DateOnly(2020, 1, 1)
            };

            var result = await _controller.UpdateCompany(updated);

            //Assert.IsType<NoContentResult>(result.Result);
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("Alpha Updated Sdn Bhd", _context.Companies.Find(updatedCompanyId)!.Name);
        }

        // -------------------------
        // DELETE
        // -------------------------
        [Fact]
        public async Task DeleteCompany_RemovesCompany_WhenExists()
        {
            long deletedCompanyId = 1;
            var result = await _controller.DeleteCompany(1);

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Null(_context.Companies.Find(deletedCompanyId));
        }

        [Fact]
        public async Task DeleteCompany_ReturnsNotFound_WhenMissing()
        {
            long invalidCompanyId = 999;
            var result = await _controller.DeleteCompany(invalidCompanyId);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
