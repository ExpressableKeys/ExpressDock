using Expressable;
using ExpressableToolbar.Properties;
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
	public abstract class ExpressBrushSettingToolBase : ExpressTool
	{
		protected readonly Popup BrushPopup;
		protected readonly Border BrushBorder;
		protected readonly TextBlock BrushText;

		protected Point InitMousePosition;
		protected double MouseDelta;

		protected double CurrentValue = 0;

		protected double MaximumValue = 5000;
		protected double MinimumValue = 1;

		protected Point ScreenCenter;

		public override void OnMouseDown(object sender, MouseButtonEventArgs e)
		{
			InitMousePosition = e.GetPosition(Application.Current.MainWindow);

			Mouse.Capture(ButtonControl);

			WpfScreenHelper.Screen scr = WpfScreenHelper.Screen.FromHandle(Process.GetCurrentProcess().MainWindowHandle);

			var winX = Application.Current.MainWindow.Left;
			var winY = Application.Current.MainWindow.Top;

			var area = scr.WorkingArea;

			ScreenCenter =
				new Point((area.Width / 2) + (area.Left) - winX,
						  (area.Height / 2) + (area.Top) - winY);

			BrushPopup.PlacementRectangle =
				new Rect(area.Left,
						 area.Top,
						 area.Width,
						 area.Height);

			BrushPopup.IsOpen = true;
		}

		public override void OnMouseUp(object sender, MouseButtonEventArgs e)
		{
			Mouse.Capture(null);
			BrushPopup.IsOpen = false;

			CurrentValue = GetBrushSettingValue();
		}

		public override void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (Mouse.Captured == ButtonControl)
			{
				SetMouseDelta(e);
				OnOverlayUpdate();
			}
		}

		public abstract void OnOverlayUpdate();

		public void SetMouseDelta(MouseEventArgs e)
		{
			var currMousePosition = e.GetPosition(Application.Current.MainWindow);

			if (Settings.Default.UseDelta)
			{
				MouseDelta = (currMousePosition - InitMousePosition).X * 2 * Settings.Default.MouseSensitivity;
			}
			else
			{
				MouseDelta = (currMousePosition - ScreenCenter).X * 2 * Settings.Default.MouseSensitivity;
			}
		}

		public double GetBrushSettingValue()
		{
			double value;

			if (Settings.Default.UseDelta)
			{
				value = CurrentValue + MouseDelta;
			}
			else
			{
				value = MouseDelta;
			}

			return Math.Clamp(value, MinimumValue, MaximumValue);
		}

		public ExpressBrushSettingToolBase() : base()
		{
			PhotoshopManager.Initialize();

			// Create popup that draws brush radius
			BrushPopup = new Popup
			{
				AllowsTransparency = true,
				Placement = PlacementMode.Center
			};

			BrushBorder = new Border
			{
				Background = Brushes.Red,
				VerticalAlignment = VerticalAlignment.Center,
				HorizontalAlignment = HorizontalAlignment.Center,
				Margin = new Thickness(20)
			};

            BrushText = new TextBlock
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var grid = new Grid();

			grid.Children.Add(BrushBorder);
			grid.Children.Add(BrushText);

			BrushPopup.Child = grid;

			ButtonControl.Template = (ControlTemplate)Application.Current.MainWindow.Resources["RoundButton"];
		}
	}
}