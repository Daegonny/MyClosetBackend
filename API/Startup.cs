using Auth;
using Auth.Abstractions;
using Base.Settings.Filters;
using Infra.NH.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using MyCloset.Infra.NH.FilterResolvers;
using System.Linq;

namespace API
{
	public class Startup
	{
		public IPathConfig PathConfig { get; set; }
		public IHashConfig HashConfig { get; set; }
		public ITokenConfig TokenConfig { get; set; }
		public IImageFileConfig ImageFileConfig { get; set; }
		public string ConnectionString { get; set; }
		public string Schema { get; set; }
		public bool EnableSwagger { get; set; }
		public bool EnableSqlVerbose { get; set; }
		public Startup(IConfiguration configuration)
		{
			var pathSettings = configuration.GetSection("Path");
			var tokenSettings = configuration.GetSection("Token");
			var imageFileSettings = configuration.GetSection("ImageFile");
			
			TokenConfig = new TokenConfig(
				int.Parse(tokenSettings.GetSection("ExpirationTimeInSeconds").Value), 
				tokenSettings.GetSection("SecretGUID").Value, 
				tokenSettings.GetSection("Issuer").Value, 
				tokenSettings.GetSection("Audience").Value);
			PathConfig = new PathConfig(
				pathSettings.GetSection("DefaultUser").Value, 
				pathSettings.GetSection("DefaultBase").Value);
			ImageFileConfig = new ImageFileConfig(
				int.Parse(imageFileSettings.GetSection("PixeLimit").Value),
				imageFileSettings.GetSection("Extension").Value,
				imageFileSettings.GetSection("ContentType").Value
			);
			ConnectionString = configuration.GetSection("ConnectionString").Value;
			Schema = configuration.GetSection("Schema").Value;
			EnableSwagger = bool.Parse(configuration.GetSection("EnableSwagger").Value);
			EnableSqlVerbose = bool.Parse(configuration.GetSection("EnableSqlVerbose").Value);
			Schema = configuration.GetSection("Schema").Value;
			HashConfig = new HashConfig(configuration.GetSection("Secret").Value);
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services
				.AddSingleton(PathConfig)
				.AddSingleton(TokenConfig)
				.AddSingleton(HashConfig)
				.AddSingleton(ImageFileConfig)
				.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
				.AddScoped<IAccountProvider, AccountProvider>()
				.AddNHibernate(ConnectionString, Schema, EnableSqlVerbose)
				.AddScoped<NHibernateUnitOfWorkActionFilter>()
				.AddScoped<IContextTools, ContextTools>()
				.AddScoped<ITags, Tags>()
				.AddScoped<IFiles, Files>()
				.AddScoped<IPieces, Pieces>()
				.AddScoped<IAccounts, Accounts>()
				.AddScoped<ISecretCodes, SecretCodes>()
				.AddScoped<IPieceService, PieceService>()
				.AddScoped<IAccountService, AccountService>()
				.AddScoped<ITagService, TagService>()
				.AddScoped<ITokenService, TokenService>()
				.AddScoped<IAuthService, AuthService>()
				.AddScoped<ISecretCodeService, SecretCodeService>()
				.AddScoped<PieceFilterResolver>()
				.AddControllers(options => {
					options.Filters.Add(typeof(NHibernateUnitOfWorkActionFilter));
					options.Filters.Add(typeof(HttpResponseExceptionFilter));
				});
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
				var securityScheme = new OpenApiSecurityScheme
				{
					Name = "JWT Authentication",
					Description = "Enter JWT Bearer token **_only_**",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "bearer",
					BearerFormat = "JWT",
					Reference = new OpenApiReference
					{
						Id = JwtBearerDefaults.AuthenticationScheme,
						Type = ReferenceType.SecurityScheme
					}
				};
				c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{securityScheme, new string[] { }}
				});
			});

			services.AddAuthentication(a =>
			{
				a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = true;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = TokenConfig.Issuer,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConfig.SecretGUID)),
					ValidAudience = TokenConfig.Audience,
					ValidateAudience = false
				};
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			if (EnableSwagger)
			{
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

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
