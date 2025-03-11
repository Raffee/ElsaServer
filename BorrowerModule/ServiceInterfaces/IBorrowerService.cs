using Genesis.Common.Core;
using Genesis.Common.ServiceProviders;
using Genesis.Modules.BorrowersModule.Models;

namespace Genesis.Modules.BorrowersModule.ServiceInterfaces
{
    public interface IBorrowerService
    {
        [ServiceAction(ServiceActions.Borrowers.CreateBorrower)]
        public Task<Guid> CreateBorrowerAsync(CreateBorrowerRequestDto request);

        [ServiceAction(ServiceActions.Borrowers.GetBorrower)]
        public Task<BorrowerProjection> GetBorrowerAsync(Guid borrowerId);
         
        public Task<IEnumerable<BorrowerProjection>> GetBorrowersAsync();
        public Task UpdateBorrowerAsync(Guid borrowerId, CreateBorrowerRequestDto request);
        public Task DeleteBorrowerAsync(Guid borrowerId);
        public Task<bool> BorrowerExistsAsync(Guid borrowerId);
    }
}
