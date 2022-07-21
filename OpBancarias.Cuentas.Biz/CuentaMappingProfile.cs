using AutoMapper;

namespace OpBancarias.Cuentas.Biz
{
    public class CuentaMappingProfile: Profile
    {
        public CuentaMappingProfile()
        {
            CreateMap<Data.Models.Cuenta, Cuenta.Biz.Cuenta>()
                .ReverseMap()
                .DisableCtorValidation();

            CreateMap<Data.Models.Movimiento, Movimientos.Biz.Movimiento>()
                .ForMember(dest => dest.NumeroCuenta, opt => opt.Ignore())
                .DisableCtorValidation();
        }
      
    }
}
