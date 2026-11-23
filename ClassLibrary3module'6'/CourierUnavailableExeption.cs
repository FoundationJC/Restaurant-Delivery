using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class CourierUnavailableException : Exception
    {
        
        public CourierUnavailableException() : base("Немає вільних кур'єрів.") { }

    }
}
