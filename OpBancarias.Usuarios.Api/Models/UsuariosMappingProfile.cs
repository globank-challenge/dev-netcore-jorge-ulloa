using AutoMapper;
using OpBancarias.Usuarios.Biz;

namespace OpBancarias.Usuarios.Api.Models
{
    public class UsuariosMappingProfile: Profile
    {
        public UsuariosMappingProfile()
        {
            CreateMap<UsuarioModel, Usuario>()
                .DisableCtorValidation()
                .ReverseMap();

            CreateMap<UsuarioQueryModel, Usuario>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .DisableCtorValidation();
        }
    }
}
