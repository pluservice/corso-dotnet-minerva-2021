using System;
using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.Models
{
    public class Person
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(80)]
        public string LastName { get; set; }

        [Range(0, int.MaxValue)]
        public int Value { get; set; }

        //[FiscalCode]
        public string FiscalCode { get; set; }

        public bool HasDiscount { get; set; }

        public int? Discount { get; set; }

        public DateTime RegistratioDate { get; set; }

        [Range(18, 99)]
        public int Age { get; set; }
    }
}
