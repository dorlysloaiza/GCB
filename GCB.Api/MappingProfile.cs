using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
           CreateMap<Transaccion, TransaccionDto>()
            .ForMember(dest => dest.DescripcionCategoria, opt => opt.MapFrom(src => src.Categoria.Descripcion))
            .ForMember(dest => dest.TipoCategoria, opt => opt.MapFrom(src => src.Categoria.Tipo))
            .ForMember(dest => dest.NumeroCuenta, opt => opt.MapFrom(src => src.CuentaBancaria.NumeroCuenta))
            .ForMember(dest => dest.NombreBanco, opt => opt.MapFrom(src => src.CuentaBancaria.Banco.Nombre))
            .ReverseMap();
    }
}