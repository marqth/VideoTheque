using VideoTheque.Core;
using VideoTheque.DTOs;
using VideoTheque.Repositories.Supports;

namespace VideoTheque.Businesses.Supports
{
    public class SupportsBusiness : ISupportsBusiness
    {
        private readonly ISupportsRepository _supportDao;

        public SupportsBusiness(ISupportsRepository supportDao)
        {
            _supportDao = supportDao;
        }
        
        public Task<List<SupportDto>> GetSupports() => _supportDao.GetSupports();

        public SupportDto GetSupport(int id)
        {
            var support = _supportDao.GetSupport(id).Result;

            if (support == null)
            {
                throw new NotFoundException($"Support '{id}' non trouvé");
            }

            return support;
        }
    }
}