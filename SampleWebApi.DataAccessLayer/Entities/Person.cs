using System;
using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.DataAccessLayer.Entities
{
    public class Person
    {
        public int Id { get; set; }

        //[JsonIgnore]
        [Required]
        public string FirstName { get; set; }

        //[JsonIgnore]
        public string LastName { get; set; }

        //[MaxLength(16)]
        //public string FiscalCode { get; set; }

        //[NotMapped]
        //public string Name => $"{FirstName} {LastName}";

        public string City { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
