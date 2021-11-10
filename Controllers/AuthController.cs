using System.Threading.Tasks;
using _netCourse.Data;
using _netCourse.Dtos.User;
using _netCourse.Models;
using Microsoft.AspNetCore.Mvc;

namespace _netCourse
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await _authRepo.Register(
                new User { username = request.username }, request.password
            );
            if (response.Success)
            {
                return this.Ok(response);
            }
            return this.BadRequest(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto userLogin)
        {
            var response = await _authRepo.Login(
                 userLogin.username, userLogin.password
            );
            if (response.Success)
            {
                return this.Ok(response);
            }
            return this.BadRequest(response);
        }
    }
}