   M S B A M L   `   `   `  ����M  IExpressableToolbar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:ExpressableToolbarExpressableToolbar  5   	   #    ExpressableToolbar.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Qshell?http://schemas.microsoft.com/winfx/2006/xaml/presentation/shell     5   	   2local clr-namespace:ExpressableToolbar     5   	   Nshell19http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   -   5          � WindowStartupLocation$  Manual=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � Topmost$
 True��5   	    � Title$ 
MainWindow��5   	   $��53.512��5   	   $	��300��5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��5   	    � SizeToContent$ WidthAndHeight=�5   	   ��5   	   Z�  � 	Resources 5         RoundButton �� 
TargetType	 J� Effect3  -System.Windows.Media.Effects.DropShadowEffect
   
BlurRadius   Opacity   ShadowDepth   	Direction  HamburgerButton��%�  &	        &	 �     �� 5   
   " ��5      .R�5      � .+�5      A� ���#30373A	 5        5      $
 13Q�6+   $ .5Q�5   +   $ 3Q�5   +   $	 251Q�5   +   5       �� 5!      $��Center=�6#   $��Center=�5"   #   5#      5$      �� 5%   
   " ��5&      .R�5'      � $��52��6   $��10,10,10,10q�5(      .+�5)      �� $(��"Resources/Icons/Icon_Hamburger.png��6   5*      5+      5,      .��5-      �� -   5         ��Name$ ToolbarBorder��5-      $��52��51       �� CornerRadius$ 25,25,25,25��52        ExpressBackground#��[ 53      $	��Top=�54       J� Opacity$ 0Q�55       �� Padding$ 0,0,0,0q�56      $
}�True��57       � Triggers 58      :� 59   
    :� RoutedEvent$ Border.Loaded��6   .[�5:      �� .��5;      �� .��5<      W�  �� TargetProperty$ Opacity�6     W� From 05=       	 W� To 15>        g� Duration$ 0:0:0.1D�5?       5@      5A      5B      ._�5D   
   � -   5         �Name$ ToolbarGrid��5D      $��Stretch=�5E      $
}�True��5F       � ColumnDefinitions 5G      5I      5J      5K      