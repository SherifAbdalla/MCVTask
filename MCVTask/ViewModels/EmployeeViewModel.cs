using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCVTask.ViewModels
{
    public class EmployeeViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public DateTime HiringDate { get; set; }

        public DateTime Birthday { get; set; }

        public string Title { get; set; }

        public string Department { get; set; }
    }
}
