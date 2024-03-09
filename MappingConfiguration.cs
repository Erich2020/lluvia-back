using AutoMapper;
using TempporalWS.Models;
using TempporalWS.Models.Dto;

namespace TempporalWS
{
    public class MappingConfiguration
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<TicketDto, Ticket>();
                config.CreateMap<Ticket, TicketDto>(); 
                config.CreateMap<ProductoDto,Producto>();
                config.CreateMap<Producto, ProductoDto>();
                config.CreateMap<UsuarioDto,Usuario>();
                config.CreateMap<Usuario, UsuarioDto>();
                config.CreateMap<ProductoTicketDto,ProductoTicket>();
                config.CreateMap<ProductoTicket, ProductoTicketDto>();
            });
            return mappingConfig;
        }
    }
}
