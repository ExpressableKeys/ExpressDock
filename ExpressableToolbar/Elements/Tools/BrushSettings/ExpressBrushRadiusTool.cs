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
	public class ExpressBrushRadiusTool : ExpressBrushSettingToolBase
	{
		public override void OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			base.OnMouseDown(sender, e);

			// PhotoshopManager.Initialize();
			// CurrentBrushSize = PhotoshopManager.GetBrushDiameter();
		}

		public override void OnMouseUp(object sender, MouseButtonEventArgs e)
		{
			base.OnMouseUp(sender, e);

			PhotoshopManager.SetBrushDiameter(CurrentBrushValue);
			PhotoshopManager.GetBrushDiameter();
		}

		public override void OnOverlayUpdate()
		{
			var brushRadiusBorder = BrushPopup.Child as Border;

			brushRadiusBorder.Width = GetBrushSettingValue();
			brushRadiusBorder.Height = brushRadiusBorder.Width;

			brushRadiusBorder.CornerRadius = new CornerRadius(brushRadiusBorder.Width / 2);
		}

		public ExpressBrushRadiusTool() : base()
		{
			PhotoshopManager.Initialize();

			// Set icon
			var uriSource = new Uri("Resources/Icons/Icon_Brush_Radius.png", UriKind.Relative);

			var image = new Image
			{
				Source = new BitmapImage(uriSource),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
			};

			ButtonControl.Content = image;
		}
	}
}