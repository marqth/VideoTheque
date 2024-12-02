using VideoTheque.DTOs;
using VideoTheque.DTOs.Enums;

namespace VideoTheque.Repositories.Supports
{
    public class SupportsRepository : ISupportsRepository
    {
        private readonly SupportEnum _supportEnum;
        
        public SupportsRepository(SupportEnum supportEnum)
        {
            _supportEnum = supportEnum;
        }
        
        public Task<List<SupportDto>> GetSupports()
        {
            var supports = Enum.GetValues(typeof(SupportEnum))
                .Cast<SupportEnum>()
                .Select(e => new SupportDto { Id = (int)e, nom = e.ToString() })
                .ToList();
            return Task.FromResult(supports);
        }
        
        public ValueTask<SupportDto?> GetSupport(int id)
        {
            var support = Enum.IsDefined(typeof(SupportEnum), id)
                ? new SupportDto { Id = id, nom = ((SupportEnum)id).ToString() }
                : null;
            return ValueTask.FromResult(support);
        }
    }
}