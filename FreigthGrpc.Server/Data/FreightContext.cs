using FreigthGrpc.Server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreigthGrpc.Server.Data
{
    public class FreightContext : DbContext
    {
        public FreightContext(DbContextOptions<FreightContext> options)
        : base(options)
        { }

        public DbSet<Freight> Freights { get; set; }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
