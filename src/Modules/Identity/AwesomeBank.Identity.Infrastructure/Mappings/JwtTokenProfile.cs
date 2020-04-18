namespace AwesomeBank.Identity.Infrastructure.Mappings
{
    using AutoMapper;
    using AwesomeBank.Identity.Application.Dtos;
    using AwesomeBank.Identity.Domain.Models;

    public class JwtTokenProfile : Profile
    {
        public JwtTokenProfile()
        {
            CreateMap<JwtToken, TokenDto>();
        }
    }
}