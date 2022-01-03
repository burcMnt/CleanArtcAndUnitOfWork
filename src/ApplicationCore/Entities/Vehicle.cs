﻿using ApplicationCore.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Vehicle :BaseEntity
    {
        public string VehicleName { get; set; }
        public string VehiclePlate { get; set; }

        public Container Container { get; set; }
    }
}
