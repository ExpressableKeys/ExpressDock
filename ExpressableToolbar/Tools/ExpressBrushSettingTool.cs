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
using Image = System.Windows.Controls.Image;
using Point = System.Windows.Point;

namespace ExpressableToolbar
{
	public class ExpressBrushSettingTool : ExpressTool
	{
		private readonly Popup BrushRadiusPopup;
		private Point InitMousePosition;
		private double MouseDelta;
		private double CurrentBrushSize = 0;

		private Point ScreenCenter;

		private bool UseDeltaResize = false;

		public override void OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			InitMousePosition = e.GetPosition(Application.Current.MainWindow);

			//PhotoshopManager.Initialize();
			//CurrentBrushSize = PhotoshopManager.GetBrushDiameter();

			Mouse.Capture(ButtonControl);

			WpfScreenHelper.Screen scr = WpfScreenHelper.Screen.FromHandle(Process.GetCurrentProcess().MainWindowHandle);

			var winX = Application.Current.MainWindow.Left;
			var winY = Application.Current.MainWindow.Top;

			var area = scr.WorkingArea;

			ScreenCenter =
				new Point((area.Width / 2) + (area.Left) - winX,
						  (area.Height / 2) + (area.Top) - winY);

			Trace.WriteLine(ScreenCenter.X + " " + ScreenCenter.Y);

			BrushRadiusPopup.PlacementRectangle =
				new Rect(area.Left,
						 area.Top,
						 area.Width,
						 area.Height);

			BrushRadiusPopup.IsOpen = true;
		}

		public override void OnMouseUp(object sender, MouseButtonEventArgs e)
		{
			Mouse.Capture(null);
			BrushRadiusPopup.IsOpen = false;
			CurrentBrushSize = GetBrushSize();
			PhotoshopManager.SetBrushDiameter(CurrentBrushSize);
		}

		public override void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (Mouse.Captured == ButtonControl)
			{
				SetMouseDelta(e);

				var brushRadiusBorder = BrushRadiusPopup.Child as Border;

				brushRadiusBorder.Width = GetBrushSize();
				brushRadiusBorder.Height = brushRadiusBorder.Width;

				brushRadiusBorder.CornerRadius = new CornerRadius(brushRadiusBorder.Width / 2);
			}
		}

		public void SetMouseDelta(MouseEventArgs e)
		{
			var currMousePosition = e.GetPosition(Application.Current.MainWindow);

			if (UseDeltaResize)
			{
				MouseDelta = (currMousePosition - InitMousePosition).X * 2;
			}
			else
			{
				MouseDelta = (currMousePosition - ScreenCenter).X * 2;
			}
		}


		public double GetBrushSize()
		{
			if (UseDeltaResize)
			{
				return Math.Abs(CurrentBrushSize + MouseDelta);
				
			}

			return Math.Abs(MouseDelta);
		}

		public ExpressBrushSettingTool() : base()
		{
			PhotoshopManager.Initialize();

			BrushRadiusPopup = new Popup
			{
				AllowsTransparency = true,
				Placement = PlacementMode.Center
			};

			var border = new Border
			{
				Background = Brushes.Red,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center,
				Margin = new Thickness(20)
			};

			BrushRadiusPopup.Child = border;

			var uriSource = new Uri("teiost.png", UriKind.Relative);

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