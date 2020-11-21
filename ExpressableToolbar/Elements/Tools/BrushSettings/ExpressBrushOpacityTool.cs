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
		public override void OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			CurrentValue = PhotoshopManager.GetBrushSettings().Opacity;

			if (CurrentValue == -1)
				return;

			base.OnMouseDown(sender, e);
		}

		public override void OnMouseUp(object sender, MouseButtonEventArgs e)
		{
			base.OnMouseUp(sender, e);

			PhotoshopManager.SetBrushOpacity(Math.Clamp(GetBrushSettingValue(), 1, 100));
		}

		public override void OnOverlayUpdate()
		{
			BrushBorder.Opacity = Math.Clamp(GetBrushSettingValue(), 1, 100) / 100;
		}

		public ExpressBrushOpacityTool() : base()
		{
			// Set icon
			var uriSource = new Uri("Resources/Icons/Icon_Brush_Opacity.png", UriKind.Relative);

			var image = new Image
			{
				Source = new BitmapImage(uriSource),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
			};

			ButtonControl.Content = image;

			BrushBorder.Width = 200;
			BrushBorder.Height = 200;

			BrushBorder.CornerRadius = new CornerRadius(BrushBorder.Width / 2);
		}
	}
}