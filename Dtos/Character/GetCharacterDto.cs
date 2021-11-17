using System.Collections.Generic;
using _netCourse.Dtos.Skills;
using _netCourse.Dtos.Weapon;

namespace _netCourse.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Sam";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public GetWeaponDto weapon { get; set; }
        public List<GetSkillDto> Skill { get; set; }
    }
}