using FreigthGrpc.Server.Data;
using FreigthGrpc.Server.Models;
using FreigthGrpc.Server.Models.Command;
using FreigthGrpc.Server.Models.Query;
using FreigthGrpc.Server.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreigthGrpc.Server.Services
{
    public class FreightService : FreigthProtoService.FreigthProtoServiceBase
    {
        private IMediator _mediator;
        public FreightService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override Task<Empty> Test(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new Empty());
        }
        public override async Task GetAllFreights(GetAllFreightsRequest request, IServerStreamWriter<FreightModel> responseStream, ServerCallContext context)
        {
            GetAllFreightsQuery query = new GetAllFreightsQuery();
            IEnumerable<Freight> freights = await _mediator.Send(query);
            foreach (Freight freight in freights)
            {
                FreightModel model = new FreightModel
                {
                    Width = freight.Width,
                    Height = freight.Height,
                    Length = freight.Length,
                    FreightType = Protos.FreightType.FullTruckLoad,
                    Weight = freight.Weight,
                    CreateDate = Timestamp.FromDateTime(freight.CreateDate)

                };
            }
        }
        public override async Task<FreightModel> GetFreight(GetFreightRequest request, ServerCallContext context)
        {
            GetFreightByIdQuery query = new GetFreightByIdQuery { Id =  Guid.Parse(request.FreightId)};
            Freight freight = await _mediator.Send(query);
            FreightModel response;
            if(freight != null)
            {
                response = new FreightModel
                {
                    Width = freight.Width,
                    Height = freight.Height,
                    Length = freight.Length,
                    FreightType = Protos.FreightType.FullTruckLoad,
                    Weight = freight.Weight,
                    CreateDate = Timestamp.FromDateTime(freight.CreateDate)
                };
            } else { response = new FreightModel(); }
            return await Task.FromResult(response);

        }
        public override async Task<AddFreightResponse> AddFreight(AddFreightRequest request, ServerCallContext context)
        {
            CreateFreightCommand command = new CreateFreightCommand
            {
                Width = request.Freight.Width,
                Height = request.Freight.Height,
                Length = request.Freight.Length,
                Weight = request.Freight.Weight,
                FreightType = (Models.FreightType)request.Freight.FreightType,
                CreateDate = DateTime.Now
            };
            Guid id = _mediator.Send(command).Result;
            AddFreightResponse response = new AddFreightResponse { Id = id.ToString() };
            return await Task.FromResult(response);
        }
        public override Task<FreightModel> UpdateFreight(UpdateFreightRequest request, ServerCallContext context)
        {
            UpdateFreightCommand command = new UpdateFreightCommand
            {
                Id = Guid.Parse(request.Id),
                Width = request.Freight.Width,
                Height = request.Freight.Height,
                Length = request.Freight.Length,
                Weight = request.Freight.Weight,
                FreightType = (Models.FreightType)request.Freight.FreightType,
            };
            Freight updatedFreight = _mediator.Send(command).Result;
            FreightModel response = new FreightModel
            {
                Height = updatedFreight.Height,
                Width = updatedFreight.Width,
                Length = updatedFreight.Length,
                Weight = updatedFreight.Weight,
                FreightType = (Protos.FreightType)updatedFreight.FreightType
            };
            return Task.FromResult(response);
        }
        public override Task<DeleteFreightResponse> DeleteFreight(DeleteFreigtRequest request, ServerCallContext context)
        {
            DeleteFreightCommand command = new DeleteFreightCommand { Id = Guid.Parse(request.Id) };
            bool deleteResult = _mediator.Send(command).Result;
            DeleteFreightResponse response = new DeleteFreightResponse { IsSuccess = deleteResult };
            return Task.FromResult(response);
        }
    }
}
