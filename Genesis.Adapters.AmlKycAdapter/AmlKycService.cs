using Genesis.Modules.BorrowersModule.ServiceInterfaces;

namespace Genesis.Adapters.AmlKycAdapter
{
    public class AmlKycService : IAmlKycService
    {
        public Task<bool> IsBorrowerKycCompliantAsync(Guid borrowerId)
        {
            throw new NotImplementedException();
        }
    }
}
