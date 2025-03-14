using Genesis.Common.Core;
using Genesis.Common.ServiceProviders;
using Genesis.Modules.BorrowersModule.Models;
using Genesis.Modules.BorrowersModule.ServiceInterfaces;

namespace Genesis.Adapters.MambuAdapter
{
    /// <summary>
    /// Borrower service implementation. The adapter doesn't need to be aware of the Workflow Core at this point.
    /// It only implements the service interface of the module.
    /// This allows modules to be developed and distributed independently of the Workflow Core.
    /// We can event separate the module interfaces and implementations into separate packages.
    /// </summary>
    public class BorrowerService : IBorrowerService, IGenesisServiceProvider
    {
        public Task<bool> BorrowerExistsAsync(Guid borrowerId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateBorrowerAsync(CreateBorrowerRequestDto request)
        {
            return Task.FromResult(Guid.NewGuid());
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

        public void SelfRegister()
        {
            throw new NotImplementedException();
        }

        public Task UpdateBorrowerAsync(Guid borrowerId, CreateBorrowerRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
