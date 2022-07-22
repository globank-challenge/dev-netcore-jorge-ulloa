using AutoMapper;

namespace OpBancarias.Clientes.Biz
{
    //Mapping profile Data to BIZ
    public class ClienteMappingProfile : Profile
    {
        public ClienteMappingProfile()
        {
            CreateMap<Data.Models.Cliente, Cliente>()
                .ReverseMap()
                .DisableCtorValidation();

            CreateMap<Data.Models.Cuenta, Cuenta.Biz.Cuenta>()
                .ReverseMap()
                .DisableCtorValidation();

            CreateMap<Data.Models.Movimiento, Movimientos.Biz.Movimiento>()
                .ForMember(dest => dest.NumeroCuenta, opt => opt.Ignore())
                .ReverseMap()
                .DisableCtorValidation();
        }

    }
}
