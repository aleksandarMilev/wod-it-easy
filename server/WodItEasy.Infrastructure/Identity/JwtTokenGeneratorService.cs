﻿namespace WodItEasy.Infrastructure.Identity
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Application;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using static Constants;

    public class JwtTokenGeneratorService : IJwtTokenGeneratorSerivce
    {
        private readonly ApplicationSettings applicationSettings;

        public JwtTokenGeneratorService(IOptions<ApplicationSettings> applicationSettings) 
            => this.applicationSettings = applicationSettings.Value;

        public string GenerateJwtToken(
            string userId,
            string username,
            string email,
            bool rememberMe = false,
            bool isAdmin = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.applicationSettings.Secret);

            var claimList = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Name, username),
                new(ClaimTypes.Email, email)
            };

            if (isAdmin)
            {
                claimList.Add(new(ClaimTypes.Role, AdministratorRoleName));
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claimList),
                Expires = rememberMe
                    ? DateTime.UtcNow.AddDays(ExtendedTokenExpirationTime)
                    : DateTime.UtcNow.AddDays(DefaultTokenExpirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
