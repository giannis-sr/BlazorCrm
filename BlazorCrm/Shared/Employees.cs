using Syncfusion.Blazor.Diagram;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorCrm.Shared
{
    public class Employees
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string NickName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set;}
        public DateTime? DateDeleted { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        [JsonIgnore]
        public List<Note> Notes { get; set; } = new List<Note>();



    }
}
