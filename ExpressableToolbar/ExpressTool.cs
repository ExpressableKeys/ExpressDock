using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExpressableToolbar
{
	public abstract class ExpressTool
	{
		public Button ButtonControl;
		abstract public void OnMouseDown(object sender, MouseButtonEventArgs e);
		abstract public void OnMouseUp(object sender, MouseButtonEventArgs e);
		abstract public void OnMouseMove(object sender, MouseEventArgs e);

		public ExpressTool()
		{
			ButtonControl = new Button();			

			ButtonControl.PreviewMouseLeftButtonDown += OnMouseDown;
			ButtonControl.PreviewMouseLeftButtonUp += OnMouseUp;
			ButtonControl.PreviewMouseMove += OnMouseMove;
		}

		private void Btn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}