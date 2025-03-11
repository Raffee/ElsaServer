using Genesis.Modules.BorrowersModule.Models;

namespace Genesis.Modules.BorrowersModule.ServiceInterfaces
{
    public interface IBorrowerService
    {
        public Task<Guid> CreateBorrowerAsync(CreateBorrowerRequestDto request);
        public Task<BorrowerProjection> GetBorrowerAsync(Guid borrowerId);
         
        public Task<IEnumerable<BorrowerProjection>> GetBorrowersAsync();
        public Task UpdateBorrowerAsync(Guid borrowerId, CreateBorrowerRequestDto request);
        public Task DeleteBorrowerAsync(Guid borrowerId);
        public Task<bool> BorrowerExistsAsync(Guid borrowerId);
    }
}
