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
		public override void OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			CurrentValue = PhotoshopManager.GetBrushSettings().Hardness;

			if (CurrentValue == -1)
				return;

			base.OnMouseDown(sender, e);
		}

		public override void OnMouseUp(object sender, MouseButtonEventArgs e)
		{
			base.OnMouseUp(sender, e);

			PhotoshopManager.SetBrushHardness(GetBrushSettingValue());
		}

		public override void OnOverlayUpdate()
		{
			var value = 100 - GetBrushSettingValue();

			BrushBorder.Effect.SetValue(BlurEffect.RadiusProperty, value);

			BrushBorder.Width = 200 - (value / 2);
			BrushBorder.Height = 200 - (value / 2);

			BrushText.Text = GetBrushSettingValue().ToString();
		}

		public ExpressBrushHardnessTool() : base()
		{
			// Set brush value settings
			MinimumValue = 1;
			MaximumValue = 100;

			// Set icon
			var uriSource = new Uri("Resources/Icons/Icon_Brush_Hardness.png", UriKind.Relative);

			var image = new Image
			{
				Source = new BitmapImage(uriSource),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
			};

			ButtonControl.Content = image;

			BrushBorder.Width = 200;
			BrushBorder.Height = 200;

			BrushBorder.Margin = new Thickness(100);
			BrushBorder.CornerRadius = new CornerRadius(BrushBorder.Width / 2);

			BrushBorder.Effect = new BlurEffect();
		}
	}
}