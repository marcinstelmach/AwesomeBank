namespace AwesomeBank.Api.Mappings.Identity
{
    using AutoMapper;
    using AwesomeBank.Api.Modules.Identity.Models;
    using AwesomeBank.Identity.Application.Commands;
    using AwesomeBank.Identity.Application.Dtos;

    public class AuthenticationViewModelsProfile : Profile
    {
        public AuthenticationViewModelsProfile()
        {
            CreateMap<SignInUserViewModel, SignInUserCommand>();

            CreateMap<TokenDto, TokenViewModel>();
        }
    }
}