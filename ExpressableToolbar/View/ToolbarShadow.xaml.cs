using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace ExpressableToolbar.View
{
	public partial class ToolbarShadow : Window
    {
        public ToolbarShadow()
        {
            InitializeComponent();
        }
        public void FadeIn()
        {
            Storyboard fadeInAnimation = Resources["FadeInAnimation"] as Storyboard;
            fadeInAnimation.Begin();
        }
        public void Update(Window parentWindow)
        {
            var margin = 25;

            this.Left = parentWindow.Left - margin;
            this.Top = parentWindow.Top - margin;
            this.Width = parentWindow.Width + (2 * margin);
            this.Height = parentWindow.Height + (2 * margin);
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            WindowConfig.SetWindowTransparent(this);
            WindowConfig.SetWindowHidden(this);
        }
    }
}
