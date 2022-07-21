using AutoMapper;

namespace OpBancarias.Movimientos.Biz
{
    public class MovimientosMappingProfile : Profile
    {
        public MovimientosMappingProfile()
        {
            CreateMap<Movimiento, Data.Models.Movimiento>()
                .ReverseMap()
                .DisableCtorValidation();
        }
    }
}
