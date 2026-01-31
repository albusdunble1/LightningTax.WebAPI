using LightningTax.WebAPI.Models.Entities;

namespace LightningTax.WebAPI.Dtos
{
    public class CompanyResponseDto
    {
    }

    public class GetCompaniesResponseDto: BaseResponseDto
    {
        public IList<Company> Companies { get; set; }
    }

    public class GetCompanyByIdResponseDto : BaseResponseDto
    {
        public Company Company { get; set; }
    }

    public class GetCompanyNamesResponseDto : BaseResponseDto
    {
        public IList<string> CompanyNames { get; set; }
    }

    public class UpdateCompanyResponseDto : BaseResponseDto { }
    public class AddCompanyResponseDto: BaseResponseDto { }
    public class DeleteCompanyByIdResponseDto : BaseResponseDto { }
}
