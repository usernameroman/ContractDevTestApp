﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ContractDevTestApp.Swagger
{
	/// <summary>
	/// Filter to add swagger pad locks only to authorize endpoints
	/// </summary>
	public class AuthResponsesOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
				.Union(context.MethodInfo.GetCustomAttributes(true))
				.OfType<AuthorizeAttribute>();

			if (authAttributes.Any())
			{
				var securityRequirement = new OpenApiSecurityRequirement()
				{
					{
						// Put here you own security scheme, this one is an example
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header,
						},
						new List<string>()
					}
				};
				operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
			}
		}
	}
}