using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrm.Shared
{
    public class Note
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employees { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
