using AutoMapper;

namespace OpBancarias.Movimientos.Biz
{
    //Mapping profile Data to BIZ
    public class MovimientosMappingProfile : Profile
    {
        public MovimientosMappingProfile()
        {
            CreateMap<Movimiento, Data.Models.Movimiento>()
                .DisableCtorValidation();

            CreateMap<Data.Models.Movimiento, Movimiento>()
                .DisableCtorValidation()
                .ForMember(dest => dest.NumeroCuenta, opt => opt.Ignore());
        }
    }
}
