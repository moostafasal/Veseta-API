using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Veseta.Core.entites;
using Veseta.Core.IServices;
using Veseta.CoreAPI.DTOs;
using Veseta.CoreCore.DTOs;
using VesetaAPI.Errors;



namespace Vezeeta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IAdminDashBoardService _adminDashBoardService;
        private readonly ITokenService _tokenService;

        public AdminAccountController(UserManager<ApplicationUser> userManager, IAccountService accountService, IMapper mapper, IAdminDashBoardService adminDashBoardService, ITokenService tokenService)
        {
            _userManager = userManager;
            _accountService = accountService;
            _mapper = mapper;
            _adminDashBoardService = adminDashBoardService;
            _tokenService = tokenService;
        }

        [HttpPost("AdminLogin")]
        public async Task<ActionResult<UserToReturnDto>> AdminLoginAsync(LoginDto model)
        {
            var loginResult = await _accountService.Login(model);
            if (loginResult == null)
            {
                return Unauthorized(new ApiResponse(404));
            }

            return Ok(new UserToReturnDto()
            {
                Email = loginResult.Email,
                UserName = loginResult.UserName,
                Token = await _tokenService.CreateTokenAsync(loginResult, _userManager)
            });
        }
        [Authorize(Roles = "Admin")]

        [HttpPost("RegisterDoctor")]
        public async Task<ActionResult<bool>> RegisterDoctorAsync(UserDTO doctor, int specializationId)
        {
            var existingUser = await _userManager.FindByEmailAsync(doctor.Email);
            if (existingUser != null)
            {
                return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "Email is already Exist" } });
            }

            var registrationResult = await _accountService.AddUser(doctor, "Doctor", specializationId);

            return registrationResult != null ? Ok(true) : BadRequest(new ApiResponse(400));
        }
        [Authorize(Roles = "Admin")]

        [HttpPut("EditDoctorInfo")]
        public async Task<ActionResult<bool>> EditDoctorInfoAsync(UserDTO doctor, int specializationId)
        {
            var existingUser = await _userManager.FindByEmailAsync(doctor.Email);
            if (existingUser == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var updateResult = await _accountService.UpdateUser(doctor, specializationId);

            return updateResult ? Ok(true) : BadRequest(new ApiResponse(400));
        }
        [Authorize(Roles = "Admin")]


        [HttpGet("AllDoctors")]
        public async Task<ActionResult<IList<DoctorsToReturnDto>>> GetAllDoctorsAsync()
        {
            var doctors = await _accountService.GetUsersInRole("Doctor");
            List<Doctor> data = doctors.OfType<Doctor>().ToList();

            return doctors != null ? Ok(_mapper.Map<IList<Doctor>, IList<DoctorsToReturnDto>>(data)) : NotFound(new ApiResponse(404));
        }

        [Authorize(Roles = "Admin")]


        [HttpDelete("DeleteDoctor")]
        public async Task<ActionResult<bool>> DeleteDoctorAsync(string doctorId)
        {
            var deleteResult = await _accountService.DeleteDoctor(doctorId);
            return deleteResult ? Ok(true) : BadRequest(new ApiResponse(400, "Can't delete this doctor"));
        }
    }
}

