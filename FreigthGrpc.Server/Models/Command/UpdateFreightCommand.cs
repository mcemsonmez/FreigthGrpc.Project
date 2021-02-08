using FreigthGrpc.Server.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreigthGrpc.Server.Models.Command
{
    public class UpdateFreightCommand : IRequest<Freight>
    {
        public Guid Id { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }
        public FreightType FreightType { get; set; }

        public class UpdateFreightCommandHandler : IRequestHandler<UpdateFreightCommand, Freight>
        {
            FreightContext _context;
            public UpdateFreightCommandHandler(FreightContext context)
            {
                _context = context;
            }
            public Task<Freight> Handle(UpdateFreightCommand request, CancellationToken cancellationToken)
            {
                Freight freight = _context.Freights.Find(request.Id);
                if(freight != null)
                {
                    freight.Weight = request.Weight;
                    freight.Height = request.Height;
                    freight.Length = request.Length;
                    freight.FreightType = request.FreightType;
                    _context.Freights.Update(freight);
                    _context.SaveChanges();
                    return Task.FromResult(freight);
                }
                return Task.FromResult(new Freight());
            }
        }
    }
}
