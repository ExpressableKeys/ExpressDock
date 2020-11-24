using Expressable;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Windows.Media.Color;
using Image = System.Windows.Controls.Image;
using Point = System.Windows.Point;
using System.Windows.Media.Effects;

namespace ExpressableToolbar
{
	public class ExpressBrushHardnessTool : ExpressBrushSettingToolBase
	{
		public override void OnToolInitialize(object sender, MouseButtonEventArgs e)
		{
			CurrentValue = PhotoshopManager.GetBrushSettings().Hardness;

			if (CurrentValue == -1)
				return;

			base.OnToolInitialize(sender, e);
		}

		public override void OnToolComplete(object sender, MouseButtonEventArgs e)
		{
			base.OnToolComplete(sender, e);

			PhotoshopManager.SetBrushHardness(GetBrushSettingValue());
		}

		public override void OnOverlayUpdate()
		{
			var value = 100 - GetBrushSettingValue();

			BrushBorder.Effect.SetValue(BlurEffect.RadiusProperty, value);

			BrushBorder.Width = 200 - (value / 2);
			BrushBorder.Height = 200 - (value / 2);

			BrushText.Text = GetBrushSettingValue().ToString() + " %";
		}

		public ExpressBrushHardnessTool() : base()
		{
			MinimumValue = 0;
			MaximumValue = 100;

			ImageSource = "pack://application:,,,/Resources/Icons/Icon_Brush_Hardness.png";

			BrushBorder.Width = 200;
			BrushBorder.Height = 200;

			BrushBorder.Margin = new Thickness(100);
			BrushBorder.CornerRadius = new CornerRadius(BrushBorder.Width / 2);

			BrushBorder.Effect = new BlurEffect();
		}
	}
}