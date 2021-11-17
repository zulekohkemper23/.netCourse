using System.Threading.Tasks;
using _netCourse.Dtos.Character;
using _netCourse.Models;
using _netCourse.Dtos.Weapon;

namespace _netCourse.Services.WeaponService
{
    public interface IWeaponService 
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}