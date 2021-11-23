using System;
using System.Threading.Tasks;
using _netCourse.Data;
using _netCourse.Dtos.Fights;
using _netCourse.Models;
using Microsoft.EntityFrameworkCore;

namespace _netCourse.Services
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        public FightService(DataContext context)
        {
            _context = context;

        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto result)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                 var attacker = await _context.characters
                    .Include(c => c.weapon)
                    .FirstOrDefaultAsync(c => c.Id == result.AttackerId);

                var opponent = await _context.characters
                    .FirstOrDefaultAsync(c => c.Id == result.OpponentId);

                int damage = attacker.weapon.Damage + (new Random().Next(attacker.Strength));
                damage -= new Random().Next(opponent.Defense);

                if (damage > 0)
                {
                    opponent.HitPoints -= damage;
                }
                if (opponent.HitPoints <= 0)
                {
                    response.Message = $"{opponent.Name} has been defeated!";
                }

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto 
                {
                    Attacker = attacker.Name,
                    AttackerHp = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHp = opponent.HitPoints,
                    Damage = damage
                };
            }
            catch (Exception ex)
            {
                
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}