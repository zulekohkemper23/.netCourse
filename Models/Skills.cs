using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace _netCourse.Models
{
    public class Skills 
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int damage { get; set; }
        public List<Character> characters { get; set; }
    }
}