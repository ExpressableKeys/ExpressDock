using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExpressableToolbar
{
	public abstract class ExpressTool : ExpressElement
	{
		abstract public void OnToolInitialize(object sender, MouseButtonEventArgs e);
		abstract public void OnToolComplete(object sender, MouseButtonEventArgs e);
		abstract public void OnToolUpdate(object sender, MouseEventArgs e);

		public ExpressTool() : base()
		{
			/*ButtonControl.PreviewMouseLeftButtonDown += OnMouseDown;
			ButtonControl.PreviewMouseLeftButtonUp += OnMouseUp;
			ButtonControl.PreviewMouseMove += OnMouseMove;*/
		}
	}
}