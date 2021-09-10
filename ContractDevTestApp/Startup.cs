using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContractDevTestApp.Application;
using ContractDevTestApp.Filters;
using ContractDevTestApp.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace ContractDevTestApp
{
	public class Startup
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IConfiguration _configuration;

		public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
		{
			_webHostEnvironment = webHostEnvironment;
			_configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddApplication(_configuration);
			services.AddPersistence(_configuration);
			services.AddAuthorization();
			services.AddHttpClient();

			services.ConfigureCors(_configuration);
			services.ConfigureJwtAuth(_configuration);
			services.ConfigureSwagger();
			services.ConfigureJsonOptions();

			services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

			services.AddControllers(options =>
			{
				options.Filters.Add(new ExceptionFilter());
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();
			app.UseCors();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/ContractDevTestApp_API/swagger.json", "ContractDevTestApp API v1");
				c.RoutePrefix = string.Empty;
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
