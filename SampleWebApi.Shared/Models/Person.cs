using System;

namespace SampleWebApi.Shared.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public string FirstName { get; set; }

        //public string LastName { get; set; }

        public string City { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
