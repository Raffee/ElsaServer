using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesis.Modules.BorrowersModule.ServiceInterfaces
{
    public static class ServiceActions
    {
        public static class Borrowers
        {
            public const string CreateBorrower = "CreateBorrower";
            public const string GetBorrower = "GetBorrower";
            public const string GetBorrowers = "GetBorrowers";
            public const string UpdateBorrower = "UpdateBorrower";
        }

        public static class Loans
        {
            public const string CreateFacilityLoan = "CreateFacilityLoan";
            public const string CreateLoanTrench = "CreateLoanTrench";
            public const string GetLoan = "GetLoan";
            public const string GetLoans = "GetLoans";
            public const string UpdateLoan = "UpdateLoan";
        }
    }
}
