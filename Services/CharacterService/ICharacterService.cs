using System.Collections.Generic;
using System.Threading.Tasks;
using _netCourse.Dtos.Character;
using _netCourse.Dtos.Skills;
using _netCourse.Models;

namespace _netCourse.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAll();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterDto(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillsDto newCharacterSkills);
    }
}