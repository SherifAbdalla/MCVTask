using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCVTask.ViewModels
{
    public class EmployeeSearchViewMode
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public DateTime? MinHiringDate { get; set; }

        public DateTime? MaxHiringDate { get; set; }

        public DateTime? MinBirthday { get; set; }

        public DateTime? MaxBirthday { get; set; }

        public int Department { get; set; }
    }
}
