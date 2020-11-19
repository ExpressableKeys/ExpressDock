using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExpressableToolbar
{
	public abstract class ExpressTool : ExpressElement
	{
		abstract public void OnMouseDown(object sender, MouseButtonEventArgs e);
		abstract public void OnMouseUp(object sender, MouseButtonEventArgs e);
		abstract public void OnMouseMove(object sender, MouseEventArgs e);

		public ExpressTool() : base()
		{
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