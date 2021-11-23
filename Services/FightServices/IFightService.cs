using System.Threading.Tasks;
using _netCourse.Dtos;
using _netCourse.Dtos.Fights;
using _netCourse.Models;

namespace _netCourse.Services
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto result);
    }
}