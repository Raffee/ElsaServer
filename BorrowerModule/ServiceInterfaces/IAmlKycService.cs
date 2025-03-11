namespace Genesis.Modules.BorrowersModule.ServiceInterfaces
{
    public interface IAmlKycService
    {
        public Task<bool> IsBorrowerKycCompliantAsync(Guid borrowerId);
    }
}
