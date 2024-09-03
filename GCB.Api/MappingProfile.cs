using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
           CreateMap<Transaction, TransactionDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.BankAccountName, opt => opt.MapFrom(src => src.BankAccount.BankName))
            .ReverseMap();
    }
}