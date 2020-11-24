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

namespace ExpressableToolbar
{
	public class ExpressBrushOpacityTool : ExpressBrushSettingToolBase
	{
		public override void OnToolInitialize(object sender, MouseButtonEventArgs e)
		{
			CurrentValue = PhotoshopManager.GetBrushSettings().Opacity;

			if (CurrentValue == -1)
				return;

			base.OnToolInitialize(sender, e);
		}

		public override void OnToolComplete(object sender, MouseButtonEventArgs e)
		{
			base.OnToolComplete(sender, e);

			PhotoshopManager.SetBrushOpacity(Math.Clamp(GetBrushSettingValue(), 1, 100));
		}

		public override void OnOverlayUpdate()
		{
			BrushBorder.Opacity = Math.Clamp(GetBrushSettingValue(), 1, 100) / 100;

			BrushText.Text = GetBrushSettingValue().ToString() + " %";
		}

		public ExpressBrushOpacityTool() : base()
		{
			MinimumValue = 1;
			MaximumValue = 100;

			ImageSource = "pack://application:,,,/Resources/Icons/Icon_Brush_Opacity.png";

			BrushBorder.Width = 200;
			BrushBorder.Height = 200;

			BrushBorder.CornerRadius = new CornerRadius(BrushBorder.Width / 2);
		}
	}
}