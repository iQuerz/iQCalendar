using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace iQCalendarClient
{
    class CalendarCellAccess
    {
        public Border Border { get; set; }
        public TextBlock Date { get; set; }
        public TextBlock Event { get; set; }
        public CheckBox CheckBox { get; set; }
    }
}
