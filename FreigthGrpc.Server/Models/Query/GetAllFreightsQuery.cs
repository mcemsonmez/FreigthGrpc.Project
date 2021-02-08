using FreigthGrpc.Server.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreigthGrpc.Server.Models
{
    public class GetAllFreightsQuery : IRequest<IEnumerable<Freight>>
    {
        public class GetAllFreightsQueryHandler : IRequestHandler<GetAllFreightsQuery, IEnumerable<Freight>>
        {
            private readonly FreightContext _context;
            public GetAllFreightsQueryHandler(FreightContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Freight>> Handle(GetAllFreightsQuery query, CancellationToken cancellationToken)
            {
                var freights = await _context.Freights.ToListAsync();
                if (freights == null)
                {
                    return null;
                }
                return freights.AsReadOnly();
            }
        }
    }
}
