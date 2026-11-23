using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class DishNotAvailableException : Exception
    {
        public string DishName { get; }
        public DishNotAvailableException(string dishName): base($"Страва '{dishName}' зараз недоступна.") { DishName = dishName; }
    }
}
