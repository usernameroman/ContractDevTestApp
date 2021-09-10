using AutoMapper;
using ContractDevTestApp.Application.Common.Models.Persistent;
using ContractDevTestApp.Application.Common.Models.Transient;
using ContractDevTestApp.Application.Common.Services.Interfaces;
using ContractDevTestApp.Domain.Entities;
using ContractDevTestApp.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using BC = BCrypt.Net.BCrypt;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ContractDevTestApp.Application.Common.Services
{
	public class AuthorizationService : IAuthorizationService
	{
		private readonly IRepository<User> _repository;
		private readonly IConfiguration _configuration;
		private readonly IMapper _mapper;


		public AuthorizationService(IRepository<User> repository, IConfiguration configuration, IMapper mapper)
		{
			_repository = repository;
			_configuration = configuration;
			_mapper = mapper;
		}

		public TokenDto CreateToken(User user, CancellationToken cancellationToken)
		{
			var claims = GetUserClaims(user);
			var newJwtToken = GenerateToken(claims.ToArray());

			return new TokenDto()
			{
				Token = new JwtSecurityTokenHandler().WriteToken(newJwtToken),
				ValidTo = newJwtToken.ValidTo,
				User = _mapper.Map<UserDto>(user),
			};
		}

		public string HashPassword([NotNull] string password)
		{
			return BC.HashPassword(password);
		}

		public bool VerifyPassword([NotNull] string plainPassword, [NotNull] string hashedPassword)
		{
			return BC.Verify(plainPassword, hashedPassword);
		}

		private List<Claim> GetUserClaims(User user)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(nameof(User.Id), user.Id.ToString()),
			}.ToList();

			return claims;
		}

		private JwtSecurityToken GenerateToken(IEnumerable<Claim> claims)
		{
			var symmetricSecurityKey =
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityToken:Key"]));
			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				_configuration["JwtSecurityToken:Audience"],
				_configuration["JwtSecurityToken:Issuer"],
				claims,
				expires: DateTime.UtcNow.AddHours(double.Parse(_configuration["JwtSecurityToken:TokenHoursLifeTime"] ?? "0")),
				signingCredentials: signingCredentials
			);

			return jwtSecurityToken;
		}
	}
}