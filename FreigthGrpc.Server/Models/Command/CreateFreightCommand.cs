using FreigthGrpc.Server.Data;
using FreigthGrpc.Server.Protos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreigthGrpc.Server.Models.Command
{
    public class CreateFreightCommand : IRequest<Guid>
    {
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }
        public FreightType FreightType { get; set; }
        public DateTime CreateDate { get; set; }

        public class CreateFreightCommandHanlder : IRequestHandler<CreateFreightCommand, Guid>
        {
            FreightContext _context;
            public CreateFreightCommandHanlder(FreightContext context)
            {
                _context = context;
            }
            public Task<Guid> Handle(CreateFreightCommand request, CancellationToken cancellationToken)
            {
                Freight freight = new Freight
                {
                    Width = request.Width,
                    Height = request.Height,
                    Length = request.Length,
                    Weight = request.Weight,
                    FreightType = request.FreightType,
                    CreateDate = DateTime.Now
                };
                _context.Freights.Add(freight);
                _context.SaveChanges();
                Guid lastId = _context.Freights.Last().Id;

                return Task.FromResult(lastId);
            }
        }
    }
}
