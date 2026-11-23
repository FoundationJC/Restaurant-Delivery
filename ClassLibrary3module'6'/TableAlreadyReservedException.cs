using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class TableAlreadyReservedException : Exception
    {
        public TableAlreadyReservedException(int table, DateTime start): base($"Столик {table} уже заброньований на {start:dd.MM.yyyy HH:mm}.") { }
    }
}
