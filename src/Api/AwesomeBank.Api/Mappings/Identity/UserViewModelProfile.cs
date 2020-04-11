namespace AwesomeBank.Api.Mappings.Identity
{
    using AutoMapper;
    using AwesomeBank.Api.Modules.Identity.Models;
    using AwesomeBank.Identity.Application.Commands;

    public class UserViewModelProfile : Profile
    {
        public UserViewModelProfile()
        {
            CreateMap<CreateUserViewModel, CreateUserCommand>();
        }
    }
}