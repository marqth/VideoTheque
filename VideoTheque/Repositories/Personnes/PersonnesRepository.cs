using VideoTheque.DTOs;
using VideoTheque.Context;
using Microsoft.EntityFrameworkCore;

namespace VideoTheque.Repositories.Personnes
{
    public class PersonnesRepository : IPersonnesRepository
    {
        private readonly VideothequeDb _context;

        public PersonnesRepository(VideothequeDb context)
        {
            _context = context;
        }

        public async Task<List<PersonneDto>> GetPersonnes()
        {
            return await _context.Personnes.ToListAsync();
        }

        public async ValueTask<PersonneDto?> GetPersonne(int id)
        {
            return await _context.Personnes.FindAsync(id);
        }

        public async Task InsertPersonne(PersonneDto personne)
        {
            await _context.Personnes.AddAsync(personne);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonne(int id, PersonneDto personne)
        {
            var existingPersonne = await _context.Personnes.FindAsync(id);
            if (existingPersonne != null)
            {
                _context.Entry(existingPersonne).CurrentValues.SetValues(personne);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeletePersonne(int id)
        {
            var personne = await _context.Personnes.FindAsync(id);
            if (personne != null)
            {
                _context.Personnes.Remove(personne);
                await _context.SaveChangesAsync();
            }
        }
    }
}