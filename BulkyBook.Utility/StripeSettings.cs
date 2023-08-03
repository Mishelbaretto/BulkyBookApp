using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class StripeSettings
    {
        //the properties inside this class, shud be the same as key name inside the section of stripe

        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }
    }
}
