using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kai_UI.Models
{
    public class Model : BindableBase
    {
        // Class containing all data used in the solution

        // List of days in the week in which the OC canteen might be open (fairly self-explanatory)
        private readonly List<string> daysOfTheSchoolWeek = new()
        {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday"
        };

        public List<string> DaysOfTheSchoolWeek
        {
            get { return daysOfTheSchoolWeek; }
        }
    }
}
