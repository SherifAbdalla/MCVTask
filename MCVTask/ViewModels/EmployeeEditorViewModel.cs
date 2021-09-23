using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MCVTask.ViewModels
{
    public class EmployeeEditorViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime HiringDate { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
    }
}
