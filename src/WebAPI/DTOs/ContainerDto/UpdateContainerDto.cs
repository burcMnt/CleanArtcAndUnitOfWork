using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs.ContainerDto
{
    public class UpdateContainerDto
    {
        public int Id { get; set; }
        public string ContainerName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

    }
}
