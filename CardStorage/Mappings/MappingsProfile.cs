using AutoMapper;
using CardStorage.Data.Models;
using CardStorage.Models;
using CardStorage.Models.Requests;

namespace CardStorage.Mappings;

public class MappingsProfile : Profile
{
    public MappingsProfile()
    {
        CreateMap<Card, CardDto>();
        CreateMap<CreateCardRequest, Card>();
    }
}