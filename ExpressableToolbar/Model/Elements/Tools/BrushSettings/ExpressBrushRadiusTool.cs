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
		private double CurrentZoom;

		public override void OnToolInitialize(object sender, MouseButtonEventArgs e)
		{
			Trace.WriteLine("Sup");

			CurrentValue = PhotoshopManager.GetBrushSettings().Diameter;

			if (CurrentValue == -1)
				return;

			CurrentZoom = PhotoshopManager.GetZoom();
			CurrentValue *= CurrentZoom;

			base.OnToolInitialize(sender, e);
		}

		public override void OnToolComplete(object sender, MouseButtonEventArgs e)
		{
			base.OnToolComplete(sender, e);

			PhotoshopManager.SetBrushDiameter(CurrentValue / CurrentZoom);
		}

		public override void OnOverlayUpdate()
		{
			BrushBorder.Width = Math.Floor(GetBrushSettingValue());
			BrushBorder.Height = BrushBorder.Width;

			BrushBorder.CornerRadius = new CornerRadius(Math.Floor(BrushBorder.Width / 2));

			BrushText.Text = Math.Floor((GetBrushSettingValue() / CurrentZoom)).ToString() + " px";
		}

		public ExpressBrushRadiusTool() : base()
		{
			MinimumValue = 1;
			MaximumValue = 5000;

            ImageSource = "pack://application:,,,/Resources/Icons/Icon_Brush_Radius.png";
		}
	}
}