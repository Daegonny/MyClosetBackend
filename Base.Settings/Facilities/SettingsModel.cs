using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Settings.Facilities
{
	public interface ISettingsModel
	{
		string ConnectionString { get; }
		string DefaultUserPath { get; set; }
		string DefaultBasePath { get; set; }
	}
	public class SettingsModel : ISettingsModel
	{
		public string ConnectionString { get; set; }
		public string DefaultUserPath { get; set; }
		public string DefaultBasePath { get; set; }
	}
}
