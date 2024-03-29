﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorthwindDemo.Entities
{
    public class Supplier
    {
        [Column("SupplierId")]
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string City { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
