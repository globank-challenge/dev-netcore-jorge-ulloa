using AutoMapper;
using OpBancarias.Movimientos.Api.Models;
using OpBancarias.Movimientos.Biz;

namespace OpBancarias.Cuentas.Api.Models
{
    public class CuentaMappingProfile: Profile
    {
        public CuentaMappingProfile()
        {
            CreateMap<CuentaModel, Cuenta.Biz.Cuenta>()
                .ForMember(dest => dest.Movimientos, opt => opt.Ignore())
                .DisableCtorValidation();

            CreateMap<Cuenta.Biz.Cuenta, CuentaModel>()
                .DisableCtorValidation();

            CreateMap<MovimientoModel, Movimiento>()
                .ReverseMap()
                .DisableCtorValidation();

            CreateMap<CuentaQueryModel, Cuenta.Biz.Cuenta>()
                .ForMember(dest => dest.Movimientos, opt => opt.Ignore())
                .DisableCtorValidation();
        }
    }
}
