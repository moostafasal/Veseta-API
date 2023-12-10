using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Veseta.Core.entites;
using Veseta.Core.Helper;
using Veseta.Core.IServices;
using Veseta.CoreAPI.DTOs;
using Veseta.CoreCore.DTOs;
using VesetaAPI.Errors;


namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public PatientController(IAccountService accountService, ITokenService tokenService, UserManager<ApplicationUser> userManager, IDoctorService doctorService, IPatientService patientService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _userManager = userManager;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToReturnDto>> Login(LoginDto model)
        {
            var res = await _accountService.Login(model); 
            if (res == null) { return Unauthorized(new ApiResponse(401)); }
            return Ok(new UserToReturnDto()
            {
                Email = res.Email,
                UserName = res.UserName,
                Token = await _tokenService.CreateTokenAsync(res, _userManager)
            });
        }

        [HttpPost("Register")]

        public async Task<ActionResult<UserToReturnDto>> Register(UserDTO patient)
        {
            var user = await _userManager.FindByEmailAsync(patient.Email);
            if (user != null) BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "Email is alredy Exist" } });
            var newUser = await _accountService.AddUser(patient, "Patient",null);
            if (newUser != null)
            {
                return Ok(new UserToReturnDto()
                {
                    UserName = newUser.UserName,
                    Email = newUser.Email,
                    Token = await _tokenService.CreateTokenAsync(newUser, _userManager)
                });
            }
            else
                return BadRequest(new ApiResponse(400));
        }
        [HttpGet("GetDoctorsAppointements")]
        public async Task<ActionResult<IList<AppointmentToReturnDto>>> GetAllDoctorsWithAppointements([FromQuery]Paging paging)
        {
            var result = await _doctorService.GetDoctorWithAppointement(paging);
            if(result == null) { return NotFound(new ApiResponse(404)); }
            return Ok(result.ToList());
        }

        [HttpPost("Booking")]
        [Authorize(Roles = "Patient")]
        public async Task<ActionResult<bool>> Booking(BookingDTO bookingDTO)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email); 
            var result  = await _patientService.Booking(bookingDTO ,userEmail);
            if (result > 0) { return Ok(true); }
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("GetAllBookingsForPatient")]
        [Authorize(Roles = "Patient")]
        public async Task<ActionResult<IList<BookingsForPatientToReturnDTO>>> GetAllBookingsForPatient()
        {
            string patientEmail = User.FindFirstValue(ClaimTypes.Email);
            IList<BookingsForPatientToReturnDTO> result = await _patientService.GetAllBookings(patientEmail);
            if (result == null) { return NotFound(new ApiResponse(404)); }
            return Ok(result);

        }
        [HttpPut("CancelAppointment")]
        [Authorize(Roles ="Patient")]
        public async Task<ActionResult<bool>> CancelAppointment([FromBody]int bookingId)
        {
            var result = await _patientService.CancelBooking(bookingId);
            if (result > 0) { return Ok(true); }
            return BadRequest(new ApiResponse(400));
        }

    }
}
