using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veseta.Core.IServices;
using Veseta.CoreAPI.DTOs;
using Veseta.CoreCore.DTOs;
using VesetaAPI.Errors;


namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountCouponController : ControllerBase
    {
        private readonly IDiscountCouponService _discountCouponService;

        public DiscountCouponController(IDiscountCouponService discountCouponService)
        {
            _discountCouponService = discountCouponService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<bool>> AddCouponAsync(DiscountCodeCouponDTO model)
        {
            var result = await _discountCouponService.AddNewCoupon(model);
            return result > 0 ? Ok(true) : BadRequest(new ApiResponse(400));
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<bool>> DeleteCouponAsync(int id)
        {
            var result = await _discountCouponService.DeleteCoupon(id);
            return result > 0 ? Ok(true) : BadRequest(new ApiResponse(400));
        }

        [HttpPut("Deactivate")]
        public async Task<ActionResult<bool>> DeactivateCouponAsync(int id)
        {
            var result = await _discountCouponService.DeActivateCoupon(id);
            return result > 0 ? Ok(true) : BadRequest(new ApiResponse(400));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<bool>> UpdateCouponAsync(DiscountToUpdateDto model)
        {
            var result = await _discountCouponService.UpdateCoupon(model);
            return result > 0 ? Ok(true) : BadRequest(new ApiResponse(400));
        }
    }
}
