using VideoTheque.DTOs;
using VideoTheque.Context;
using Microsoft.EntityFrameworkCore;

namespace VideoTheque.Repositories.Hosts
{
    public class HostsRepository : IHostsRepository
    {
        private readonly VideothequeDb _context;

        public HostsRepository(VideothequeDb context)
        {
            _context = context;
        }

        public async Task<List<HostDto>> GetHosts()
        {
            return await _context.Hosts.ToListAsync();
        }

        public async ValueTask<HostDto?> GetHost(int id)
        {
            return await _context.Hosts.FindAsync(id);
        }

        public async Task InsertHost(HostDto host)
        {
            await _context.Hosts.AddAsync(host);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHost(int id, HostDto host)
        {
            var existingHost = await _context.Hosts.FindAsync(id);
            if (existingHost != null)
            {
                _context.Entry(existingHost).CurrentValues.SetValues(host);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteHost(int id)
        {
            var host = await _context.Hosts.FindAsync(id);
            if (host != null)
            {
                _context.Hosts.Remove(host);
                await _context.SaveChangesAsync();
            }
        }
    }
}