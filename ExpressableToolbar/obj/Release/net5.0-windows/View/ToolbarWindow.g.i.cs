﻿#pragma checksum "..\..\..\..\View\ToolbarWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "519189BA364A4B0546E26FBF1D26AA538E5E648F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ExpressableToolbar.View.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ExpressableToolbar.View {
    
    
    /// <summary>
    /// ToolbarWindow
    /// </summary>
    public partial class ToolbarWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\..\View\ToolbarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ExpressableToolbar.View.ToolbarWindow Toolbar;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\View\ToolbarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ToolbarBorder;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\View\ToolbarWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ExpressableToolbar.View.Controls.ToolbarControl ToolbarContainer;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ExpressableToolbar;component/view/toolbarwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\ToolbarWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Toolbar = ((ExpressableToolbar.View.ToolbarWindow)(target));
            
            #line 10 "..\..\..\..\View\ToolbarWindow.xaml"
            this.Toolbar.ContentRendered += new System.EventHandler(this.Window_ContentRendered);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ToolbarBorder = ((System.Windows.Controls.Border)(target));
            
            #line 30 "..\..\..\..\View\ToolbarWindow.xaml"
            this.ToolbarBorder.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.ToolbarBorder_MouseDown);
            
            #line default
            #line hidden
            
            #line 31 "..\..\..\..\View\ToolbarWindow.xaml"
            this.ToolbarBorder.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.ToolbarBorder_MouseUp);
            
            #line default
            #line hidden
            
            #line 32 "..\..\..\..\View\ToolbarWindow.xaml"
            this.ToolbarBorder.MouseMove += new System.Windows.Input.MouseEventHandler(this.ToolbarBorder_MouseMove);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ToolbarContainer = ((ExpressableToolbar.View.Controls.ToolbarControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

