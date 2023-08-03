using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{//will hold all static deails so SD
    public static class SD
    {
        public const string Role_User_Indi = "Individual"; //Individual user
        public const string Role_User_Comp = "Company";    //Company User
        public const string Role_Admin = "Admin";   //Management side or company side
        public const string Role_Employee = "Employee";   //Management side or company side

        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayPayment = "ApprovedForDelayedPayment";
        public const string PaymentStatusRejected = "Rejected";


        public const string SessionCart  = "SessionShoppingCart";
    }
}
