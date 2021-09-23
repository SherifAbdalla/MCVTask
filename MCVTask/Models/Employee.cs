using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MCVTask.Models
{
    public class Employee
    {
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime HiringDate { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
