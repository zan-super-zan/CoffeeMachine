using AutoMapper;
using CoffeeMachineModel;
using CoffeMachineAPI.PostgresModel;

namespace CoffeMachineAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // postgre -> dto/rest api model
            CreateMap<MachineEvent, MachineEventDto>();
            CreateMap<MachineState, MachineStateDto>();
        }
    }
}
