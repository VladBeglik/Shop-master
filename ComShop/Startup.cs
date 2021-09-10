using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComShop.helpers;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Infrastructure.Data;
using ComShop.Extensions;
using ComShop.Middleware;

namespace ComShop
{
	public class Startup
	{
		private readonly IConfiguration _config;
		public Startup(IConfiguration config)
		{
			_config = config;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddAutoMapper(typeof(MappingProfiles));
			services.AddControllers();
			services.AddDbContext<StoreContext>(x =>
				x.UseSqlServer(_config.GetConnectionString("DefaultConnection")));

			services.AddApplicationServices();
			services.AddSwaggerDocumentation();

			services.AddCors(opt =>
			{
				opt.AddPolicy("CorsPolicy", policy =>
				{
					policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
				});
			});

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseMiddleware<ExceptionMiddleware>();
			app.UseStatusCodePagesWithReExecute("/errors/{0}");

			app.UseHttpsRedirection();

			app.UseRouting();
			app.UseStaticFiles();

			app.UseCors("CorsPolicy");

			app.UseAuthorization();

			app.UseSwaggerDocumention();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}