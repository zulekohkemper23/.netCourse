
using System.Collections.Generic;
using _netCourse.Models;

namespace _netCourse.Models
{
    public class Character 
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Sam";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public User User { get; set; }
        public Weapons weapon { get; set; }
        public List<Skills> Skill { get; set; }
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int defeats { get; set; }
    }
}