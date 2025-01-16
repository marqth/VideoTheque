using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.Hosts;

namespace VideoTheque.Businesses.Hosts
{
    public class HostsBusiness : IHostsBusiness
    {
        private readonly IHostsRepository _hostDao;

        public HostsBusiness(IHostsRepository hostDao)
        {
            _hostDao = hostDao;
        }

        public Task<List<HostDto>> GetHosts() => _hostDao.GetHosts();

        public HostDto GetHost(int id)
        {
            var host = _hostDao.GetHost(id).Result;

            if (host == null)
            {
                throw new NotFoundException($"Host '{id}' not found");
            }

            return host;
        }

        public Task InsertHost(HostDto host) => _hostDao.InsertHost(host);

        public Task UpdateHost(int id, HostDto host) => _hostDao.UpdateHost(id, host);

        public Task DeleteHost(int id) => _hostDao.DeleteHost(id);
    }
}