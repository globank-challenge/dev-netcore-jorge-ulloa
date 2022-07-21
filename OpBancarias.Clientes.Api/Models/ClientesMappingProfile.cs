using AutoMapper;
using OpBancarias.Clientes.Biz;
using OpBancarias.Cuentas.Api.Models;

namespace OpBancarias.Clientes.Api.Models
{
    public class ClienteMappingProfile : Profile
    {
        public ClienteMappingProfile()
        {
            CreateMap<Cliente, ClienteModel>()
                .DisableCtorValidation();

            CreateMap<ClienteModel, Cliente>()
                .ForMember(dest => dest.Cuentas, opt => opt.Ignore())
                .DisableCtorValidation();

            CreateMap<ClienteQueryModel, Cliente>()
                .ForMember(dest => dest.Cuentas, opt => opt.Ignore())
                .DisableCtorValidation();

            CreateMap<CuentaModel, Cuenta.Biz.Cuenta>()
                .ForMember(dest => dest.Movimientos, opt => opt.Ignore())
                .DisableCtorValidation();

            CreateMap<ReporteMovimientosModel, MovimientosByClienteModel>()
                .ReverseMap()
                .DisableCtorValidation();
        }

    }
}
