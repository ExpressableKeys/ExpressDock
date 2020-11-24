using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressableToolbar.Properties;

namespace ExpressableToolbar.Model.Settings
{
	class SettingDescriptor
	{
		private string _name;

		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				Type = Properties.Settings.Default[_name].GetType();
			}
		}

		public string Description { get; }
		public Type Type { get; set; }

		public SettingDescriptor(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}
}
