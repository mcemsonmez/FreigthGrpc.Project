using FreigthGrpc.Server.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreigthGrpc.Server.Models.Query
{
    public class GetFreightByIdQuery : IRequest<Freight>
    {
        public Guid Id { get; set; }
        public class GetFreightByIdQueryHandler : IRequestHandler<GetFreightByIdQuery, Freight>
        { 
            FreightContext _context;
            public GetFreightByIdQueryHandler(FreightContext context)
            {
                _context = context;
            }
            public Task<Freight> Handle(GetFreightByIdQuery request, CancellationToken cancellationToken)
            {
                Freight freight = _context.Freights.Find(request.Id);
                if (freight == null) return null;
                return Task.FromResult(freight);
            }
        }
    }
}
