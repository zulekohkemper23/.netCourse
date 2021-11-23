using _netCourse.Dtos.Character;
using _netCourse.Dtos.Fights;
using _netCourse.Dtos.Skills;
using _netCourse.Dtos.Weapon;
using _netCourse.Models;
using AutoMapper;

namespace _netCourse
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapons, GetWeaponDto>();
            CreateMap<Skills, GetSkillDto>();
            CreateMap<Character, HighScoreDto>(); 
        }
    }
}