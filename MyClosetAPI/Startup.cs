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

namespace MyClosetAPI
{
	public class Startup
	{
		ISettingsModel Settings;
		IPathConfig PathConfig;
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			Settings = SettingsReader.Get();
			PathConfig = new PathConfig(Settings.DefaultUserPath, Settings.DefaultBasePath);
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddSingleton(Settings)
				.AddSingleton(PathConfig)
				.AddSingleton<IContextTools, ContextTools>()
				.AddNHibernate(SettingsReader.Get().ConnectionString)
				.AddScoped<NHibernateUnitOfWorkActionFilter>()
				.AddScoped<IContextTools, ContextTools>()
				.AddScoped<ITags, Tags>()
				.AddScoped<IFiles, Files>()
				.AddScoped<IPieces, Pieces>()
				.AddScoped<IPieceService, PieceService>()
				.AddScoped<ITagService, TagService>()
				.AddControllers(options
			   => options.Filters.Add(typeof(NHibernateUnitOfWorkActionFilter)));
			services.AddSwaggerGen(c => c.SwaggerDoc(name: "v1", new OpenApiInfo() { Title = "MyCloset", Version = "v0.1" }));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();

			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "MyCloset");
			});

			app.UseHttpsRedirection();
			app.UseForwardedHeaders();

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
