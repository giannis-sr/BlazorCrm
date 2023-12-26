using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BlazorCrm.Shared
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        public int ProductCode { get; set; }
        public float Price { get; set; }
        public bool Availability { get; set; }
        [NotMapped]
        public Image ProductImage { get; set; }
        public float Weight { get; set; }
        public float Dimensions { get; set; }
        public string Text { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateDeleted { get; set; }


    }
}
