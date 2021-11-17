using _netCourse.Models;

namespace _netCourse
{
    public class Weapons 
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public Character character { get; set; }
        public int CharacterId { get; set; }
    }
}