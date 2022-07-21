using AutoMapper;

namespace OpBancarias.Clientes.Biz
{
    //Mapping profile Data to BIZ
    public class ClienteMappingProfile : Profile
    {
        public ClienteMappingProfile()
        {
            CreateMap<Data.Models.Cliente, Cliente>()
                .DisableCtorValidation();

            CreateMap<Cliente, Data.Models.Cliente>()
                .DisableCtorValidation();
        }

    }
}
