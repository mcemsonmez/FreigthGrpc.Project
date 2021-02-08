using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreigthGrpc.Server.Models
{
    public class Freight
    {
        public Guid Id { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }
        public FreightType FreightType { get; set; }
        public DateTime CreateDate { get; set; }
	}

    public enum FreightType
    {
        LessThanTruckLoad,
        FullTruckLoad
    };

}
