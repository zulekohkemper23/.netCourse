using System.Threading.Tasks;
using _netCourse.Dtos.Character;
using _netCourse.Dtos.Weapon;
using _netCourse.Models;
using _netCourse.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _netCourse.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;
        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;

        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddWeapon(AddWeaponDto addweapon)
        {
            return Ok(await _weaponService.AddWeapon(addweapon));
        }
    }
}