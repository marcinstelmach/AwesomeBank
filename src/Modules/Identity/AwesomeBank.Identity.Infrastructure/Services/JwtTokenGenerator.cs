namespace AwesomeBank.Identity.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using AwesomeBank.BuildingBlocks.Application;
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.BuildingBlocks.Infrastructure.Settings;
    using AwesomeBank.Identity.Domain.Entities;
    using AwesomeBank.Identity.Domain.Interfaces;
    using AwesomeBank.Identity.Domain.Models;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Claim = System.Security.Claims.Claim;

    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private const string PermissionClaimType = "permission";

        private readonly IUserClaimsRepository _userClaimsRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly JwtAuthenticationSettings _jwtAuthenticationSettings;

        public JwtTokenGenerator(IUserClaimsRepository userClaimsRepository, IDateTimeService dateTimeService, IOptions<JwtAuthenticationSettings> jwtAuthenticationSettings)
        {
            _userClaimsRepository = userClaimsRepository;
            _dateTimeService = dateTimeService;
            _jwtAuthenticationSettings = jwtAuthenticationSettings.Value;
        }

        public async Task<JwtToken> GenerateAsync(User user)
        {
            Insist.IsNotNull(user, nameof(user));

            var utcNow = _dateTimeService.GetDateTimeUtcNow();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iat, GetJwtDate(utcNow), ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            };

            var userClaims = await _userClaimsRepository.GetUserClaimsAsync(user.Id);
            claims.AddRange(userClaims.Select(x => new Claim(PermissionClaimType, x)));

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthenticationSettings.SecretKey)), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtAuthenticationSettings.Issuer,
                _jwtAuthenticationSettings.Audience,
                claims,
                utcNow,
                utcNow.AddMinutes(_jwtAuthenticationSettings.AccessTokenLifetimeMinutes),
                signingCredentials);

            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new JwtToken
            {
                AccessToken = stringToken,
                LifetimeInMinutes = _jwtAuthenticationSettings.AccessTokenLifetimeMinutes
            };
        }

        private static string GetJwtDate(DateTime dateTime)
            => EpochTime.GetIntDate(dateTime.ToUniversalTime()).ToString(CultureInfo.InvariantCulture);
    }
}