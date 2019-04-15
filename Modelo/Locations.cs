﻿using System;
using System.Collections.Generic;

namespace Modelo
{
    public partial class Locations
    {
        public Locations()
        {
            Departments = new HashSet<Departments>();
        }

        public byte LocationId { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string CountryId { get; set; }

        public virtual Countries Country { get; set; }
        public virtual ICollection<Departments> Departments { get; set; }
    }
}
