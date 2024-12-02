using VideoTheque.DTOs;

namespace VideoTheque.Businesses.Supports
{
    public interface ISupportsBusiness
    {
        Task<List<SupportDto>> GetSupports();

        SupportDto GetSupport(int id);
    }
}