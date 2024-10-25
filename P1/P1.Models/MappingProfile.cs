using AutoMapper;
using P1.Models.DTO;

namespace P1.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, LogInDTO>().ReverseMap();
        CreateMap<Account, CreateAccountDTO>().ReverseMap();
    }
}