using FreigthGrpc.Server.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreigthGrpc.Server.Models.Command
{
    public class DeleteFreightCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public class DeleteFreightCommandCommand : IRequestHandler<DeleteFreightCommand, bool>
        {
            private readonly FreightContext _context;
            public DeleteFreightCommandCommand(FreightContext context)
            {
                _context = context;
            }
            public Task<bool> Handle(DeleteFreightCommand request, CancellationToken cancellationToken)
            {
                Freight freight = _context.Freights.Find(request.Id);
                if (freight == null) return Task.FromResult(false);
                _context.Freights.Remove(freight);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
        }
    }
}
