using Application.DTO;
using AutoMapper;
using Domain.Models;

namespace Application.MappingProfiler
{
    public class ClienteMap : Profile
    {
        public ClienteMap()
        {
            CreateMap<Cliente,ClienteDTO>().ReverseMap();
            CreateMap<Contato, ContatoDTO>().ReverseMap();
        }
    }
}
