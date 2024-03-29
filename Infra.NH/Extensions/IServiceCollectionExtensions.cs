﻿using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using Microsoft.Extensions.DependencyInjection;
using MyCloset.Infra.Map.Seed;
using NHibernate;
namespace Infra.NH.Extensions
{

	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddNHibernate(this IServiceCollection serviceCollection, string connectionString, 
			string schema, bool enableSqlVerbose)
		{

			var dataBase = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(connectionString).DefaultSchema(schema);
			if (enableSqlVerbose)
				dataBase.ShowSql().FormatSql();

			var config = Fluently
				.Configure()
				.Database(dataBase)
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<IMap>()
									.Conventions.Add(ForeignKey.EndsWith(string.Empty)))
				.BuildConfiguration();

			var sessionFactory = config.BuildSessionFactory();
			serviceCollection
				.AddSingleton<ISessionFactory>(sessionFactory)
				.AddScoped<ISession>(factory => sessionFactory.OpenSession())
				.AddScoped<IUnitOfWork, NHUnitOfWork>();
			return serviceCollection;
		}
	}
}
