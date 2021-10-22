﻿using System;
using System.Collections.Generic;

namespace SampleWebApi.Shared.Models
{
    public class SavePersonRequest
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public DateTime? BirthDate { get; set; }

        public int Age { get; set; }

        public string FiscalCode { get; set; }

        public bool HasDiscount { get; set; }

        public int? Discount { get; set; }

        public string Country { get; set; }

        public IEnumerable<FavoriteCategory> Categories { get; set; }
    }

    public class FavoriteCategory
    {
        public int CategoryId { get; set; }

        public IEnumerable<int> ProductIds { get; set; }
    }
}
