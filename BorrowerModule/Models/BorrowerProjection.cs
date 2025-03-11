namespace Genesis.Modules.BorrowersModule.Models
{
    public class BorrowerProjection
    {
        public Guid BorrowerId { get; set; }
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
