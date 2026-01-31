using LightningTax.WebAPI.Models.Enums;

namespace LightningTax.WebAPI.Dtos
{
    public class BaseResponseDto
    {
        public ServerStatusEnum StatusCode { get; set; } = ServerStatusEnum.Success;
        public DateTime ServerTime { get; set; } = DateTime.UtcNow;

        //public List<string> Errors { get; set; } = new();
    }

}
