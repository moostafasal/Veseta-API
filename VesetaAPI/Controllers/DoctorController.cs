using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Veseta.Core.entites;
using Veseta.Core.Helper;
using Veseta.Core.IServices;
using Veseta.CoreCore.DTOs;
using Veseta.repository.Data;
using VesetaAPI.Errors;




namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDoctorService _doctorService;
        private readonly ApplicationDbContext _context;

        public DoctorController(UserManager<ApplicationUser> userManager, ITokenService tokenService, IAccountService accountService, IDoctorService doctorService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _accountService = accountService;
            _doctorService = doctorService;
            _context = context;
        }

        [HttpPost("LoginDoctor")]
        public async Task<ActionResult<UserToReturnDto>> LoginDoctor(LoginDto model)
        {
            var loginResult = await _accountService.Login(model);
            if (loginResult == null) { return Unauthorized(new ApiResponse(401)); }
            return Ok(new UserToReturnDto()
            {
                Email = loginResult.Email,
                UserName = loginResult.UserName,
                Token = await _tokenService.CreateTokenAsync(loginResult, _userManager)
            });
        }

        [HttpPost("AddDoctorAppointment")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddDoctorAppointmentAsync(AppointmentDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.Email);
            var result = await _doctorService.CreateAppointment(model, userId);
            if (result == null) { return NotFound(new ApiResponse(404)); }
            return Ok(result);
        }

        [HttpPut("UpdateDoctorAppointment")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateDoctorAppointmentAsync(AppointmentDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.Email);
            var result = await _doctorService.UpdateAppointmentAsync(model, userId);
            return result > 0 ? Ok(true) : BadRequest(new ApiResponse(400));
        }

        [HttpDelete("DeleteDoctorAppointment")]
        [Authorize(Roles = "Doctor")]
        public async Task<ActionResult<bool>> DeleteDoctorAppointmentAsync(IList<int> timeSlotsIds)
        {
            var result = await _doctorService.DeleteAppointmentAsync(timeSlotsIds);
            return result > 0 ? Ok(true) : BadRequest(new ApiResponse(400));
        }

        [HttpPut("ConfirmDoctorCheckUp")]
        [Authorize(Roles = "Doctor")]
        public async Task<ActionResult<bool>> ConfirmDoctorCheckUpAsync([FromBody] int bookingId)
        {
            var result = await _doctorService.ConfirmCheckUp(bookingId);
            return result > 0 ? Ok(true) : BadRequest(new ApiResponse(400));
        }

        [HttpGet("DoctorBookings")]
        [Authorize(Roles = "Doctor")]
        public async Task<ActionResult<IList<BookingsForPatientToReturnDTO>>> GetDoctorBookingsAsync([FromQuery] Paging paging)
        {
            string doctorEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await _doctorService.GetAllBookingsForDoctor(doctorEmail, paging);
            return result == null ? NotFound(new ApiResponse(404)) : Ok(result);
        }
    }
}


