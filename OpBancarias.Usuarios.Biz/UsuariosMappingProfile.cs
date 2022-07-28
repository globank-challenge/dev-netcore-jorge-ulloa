using AutoMapper;

namespace OpBancarias.Usuarios.Biz
{
    public class UsuariosMappingProfile: Profile
    {
        public UsuariosMappingProfile()
        {
            CreateMap<Data.Models.Usuario, Usuario>()
                .DisableCtorValidation()
                .ReverseMap();
        }
    }
}
