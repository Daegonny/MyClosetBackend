using Auth;
using Auth.Abstractions;
using Base.Settings.Facilities;
using Base.Settings.Filters;
using Infra.NH.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyCloset.Infra.Abstractions.Repositories;
using MyCloset.Infra.File;
using MyCloset.Infra.NH.Repositories;
using MyCloset.Services.Abstractions.CrudServices;
using MyCloset.Services.CrudServices;
using Util.Config;
using Util.Services;

namespace API
{
	public class Startup
	{
		public ISettingsModel Settings { get; set; }
		public IPathConfig PathConfig { get; set; }
		public ITokenConfig TokenConfig { get; set; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			Settings = SettingsReader.Get();
			TokenConfig = new TokenConfig(120, "BCFB662B-E07B-40E4-AA45-6ECF4B55D68E", "default_issuer", "default_audience");
			PathConfig = new PathConfig(Settings.DefaultUserPath, Settings.DefaultBasePath);
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services
				.AddSingleton(Settings)
				.AddSingleton(PathConfig)
				.AddSingleton(TokenConfig)
				.AddSingleton<IContextTools, ContextTools>()
				.AddNHibernate(SettingsReader.Get().ConnectionString)
				.AddScoped<NHibernateUnitOfWorkActionFilter>()
				.AddScoped<IContextTools, ContextTools>()
				.AddScoped<ITags, Tags>()
				.AddScoped<IFiles, Files>()
				.AddScoped<IPieces, Pieces>()
				.AddScoped<IUsers, Users>()
				.AddScoped<IPieceService, PieceService>()
				.AddScoped<ITagService, TagService>()
				.AddScoped<ITokenService, TokenService>()
				.AddScoped<IAuthService, AuthService>()
				.AddControllers(options => {
					options.Filters.Add(typeof(NHibernateUnitOfWorkActionFilter));
					options.Filters.Add(typeof(HttpResponseExceptionFilter));
				});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors(c =>
			{
				c
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowAnyOrigin();
			});

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
