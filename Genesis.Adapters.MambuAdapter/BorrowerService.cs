using Genesis.Common.Core;
using Genesis.Modules.BorrowersModule.Models;
using Genesis.Modules.BorrowersModule.ServiceInterfaces;

namespace Genesis.Adapters.MambuAdapter
{
    public class BorrowerService : IBorrowerService
    {
        public Task<bool> BorrowerExistsAsync(Guid borrowerId)
        {
            throw new NotImplementedException();
        }

        [ServiceAction(ServiceActions.Borrowers.CreateBorrower)]
        public Task<Guid> CreateBorrowerAsync(CreateBorrowerRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBorrowerAsync(Guid borrowerId)
        {
            throw new NotImplementedException();
        }

        public Task<BorrowerProjection> GetBorrowerAsync(Guid borrowerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BorrowerProjection>> GetBorrowersAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateBorrowerAsync(Guid borrowerId, CreateBorrowerRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
