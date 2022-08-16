using DavidsBlog.Application.Authentication.Commands.Register;
using DavidsBlog.Application.Authentication.Commands.Register.Queries.Login;
using DavidsBlog.Application.Authentication.Common;
using DavidsBlog.Contracts.Authentication;
using Mapster;

namespace DavidsBlog.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginQuery>();
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}