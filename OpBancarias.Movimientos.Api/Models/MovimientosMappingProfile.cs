using AutoMapper;
using OpBancarias.Movimientos.Biz;

namespace OpBancarias.Movimientos.Api.Models
{
    public class MovimientosMappingProfile: Profile
    {
        public MovimientosMappingProfile()
        {
            CreateMap<MovimientoQueryModel, Movimiento>()
                .ForMember(dest => dest.Saldo, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Tipo, opt => opt.Ignore())
                .ForMember(dest => dest.Fecha, opt => opt.Ignore())
                .DisableCtorValidation();

            CreateMap<Movimiento, MovimientoModel>()
                .DisableCtorValidation();
        }
    }
}
