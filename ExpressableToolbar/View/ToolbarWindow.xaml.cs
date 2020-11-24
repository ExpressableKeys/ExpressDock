using ExpressableToolbar.Properties;
using ExpressableToolbar.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace ExpressableToolbar.View
{
	public partial class ToolbarWindow : Window
    {
		private readonly ToolbarShadow ShadowWindow;

		public Point StartPoint { get; private set; }
		public bool IsDragging { get; private set; }

		public ToolbarWindow()
        {
            InitializeComponent();
			DataContext = new ToolbarViewModel();

			ShadowWindow = new ToolbarShadow();
			ShadowWindow.Show();
		}

		public void StartDragging(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left && !Settings.Default.Pinned)
			{
				StartPoint = PointToScreen(Mouse.GetPosition(this));

				IsDragging = true;
				Mouse.Capture(ToolbarBorder);
			}
		}

		public void StopDragging(object sender, MouseButtonEventArgs e)
		{
			if (IsDragging)
			{
				Mouse.Capture(null);
				IsDragging = false;

				Settings.Default.StartPosition = new System.Drawing.Point((int)Left, (int)Top);
			}
		}

		public void DragMove(object sender, MouseEventArgs e)
		{
			if (IsDragging && !Settings.Default.Pinned)
			{
				Point newPoint = PointToScreen(Mouse.GetPosition(this));

				int diffX = (int)(newPoint.X - StartPoint.X);
				int diffY = (int)(newPoint.Y - StartPoint.Y);

				if (Math.Abs(diffX) > 1 || Math.Abs(diffY) > 1)
				{
					Left += diffX;
					Top += diffY;

					ShadowWindow.Update(this);

					StartPoint = newPoint;
				}
			}
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);
			WindowConfig.SetWindowNoActivate(this);
		}

		private void OnContentRendered(object sender, EventArgs e)
		{
			ShadowWindow.Update(this);
			ShadowWindow.FadeIn();
		}
	}
}
