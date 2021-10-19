using System;

namespace SampleWebApi.Shared.Models
{
    public class SavePersonRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
