using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExpressableToolbar.Properties;

namespace ExpressableToolbar.View.Controls
{
	public partial class SettingsEntryBoolControl : UserControl
	{
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(String), typeof(SettingsEntryBoolControl), new FrameworkPropertyMetadata("Title"));
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(String), typeof(SettingsEntryBoolControl), new FrameworkPropertyMetadata("Description"));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(SettingsEntryBoolControl), new FrameworkPropertyMetadata(false));
        
        public string Title
        {
            get => GetValue(TitleProperty) as string;
            set => SetValue(TitleProperty, value);
        }

        public string Description
        {
            get => GetValue(DescriptionProperty) as string;
            set => SetValue(DescriptionProperty, value);
        }
        
        public bool Value
        {
            get => (bool)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public SettingsEntryBoolControl()
		{
			InitializeComponent();
		}
	}
}
