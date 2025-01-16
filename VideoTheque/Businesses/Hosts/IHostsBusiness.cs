using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Hosts
{
    public interface IHostsBusiness
    {
        Task<List<HostDto>> GetHosts();
        HostDto GetHost(int id);
        Task InsertHost(HostDto host);
        Task UpdateHost(int id, HostDto host);
        Task DeleteHost(int id);
    }
}