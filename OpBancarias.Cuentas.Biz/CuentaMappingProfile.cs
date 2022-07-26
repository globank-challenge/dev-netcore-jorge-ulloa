using AutoMapper;

namespace OpBancarias.Cuentas.Biz
{
    //Mapping profile Data to BIZ
    public class CuentaMappingProfile: Profile
    {
        public CuentaMappingProfile()
        {
            CreateMap<Data.Models.Cuenta, Cuenta.Biz.Cuenta>()
                .DisableCtorValidation()
                .ReverseMap();

            CreateMap<Data.Models.Movimiento, Movimientos.Biz.Movimiento>()
                .DisableCtorValidation()
                .ForMember(dest => dest.NumeroCuenta, opt => opt.Ignore());

            CreateMap<Movimientos.Biz.Movimiento, Data.Models.Movimiento>()
                .DisableCtorValidation();
        }
      
    }
}
