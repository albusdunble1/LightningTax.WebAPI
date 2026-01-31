using LightningTax.WebAPI.Dtos;
using LightningTax.WebAPI.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LightningTax.WebAPI.Helpers
{
    public class ResponseHelper
    {
        public static ObjectResult NotFound()
        {
            var response = new BaseResponseDto { StatusCode = ServerStatusEnum.NotFound };

            return new NotFoundObjectResult(response)
            {
                StatusCode = StatusCodes.Status404NotFound
            };
        }

        public static ObjectResult SystemError()
        {
            var response = new BaseResponseDto { StatusCode = ServerStatusEnum.SystemError };

            return new ObjectResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
