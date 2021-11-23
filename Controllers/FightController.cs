using System.Threading.Tasks;
using _netCourse.Dtos.Fights;
using _netCourse.Models;
using _netCourse.Services;
using Microsoft.AspNetCore.Mvc;

namespace _netCourse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FightController : ControllerBase
    {
        private readonly IFightService _fightService;
        public FightController(IFightService fightService)
        {
            _fightService = fightService;

        }

        [HttpPost("Weapon")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> WeaponAttack(WeaponAttackDto result)
        {
            return Ok(await _fightService.WeaponAttack(result));
        }
    }
}