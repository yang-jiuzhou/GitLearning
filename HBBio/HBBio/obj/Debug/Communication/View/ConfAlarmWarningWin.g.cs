﻿#pragma checksum "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FE678F687C12573B0100934CBD57645C9E1A39841D748EC5610654B40D469151"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using HBBio.Communication;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace HBBio.Communication {
    
    
    /// <summary>
    /// ConfAlarmWarningWin
    /// </summary>
    public partial class ConfAlarmWarningWin : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgvAlarmWarning;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn colLL;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn colL;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn colH;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn colHH;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCommun;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOK;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Bio-LabChrom;component/communication/view/confalarmwarningwin.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml"
            ((HBBio.Communication.ConfAlarmWarningWin)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dgvAlarmWarning = ((System.Windows.Controls.DataGrid)(target));
            
            #line 15 "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml"
            this.dgvAlarmWarning.CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.dgvAlarmWarning_CellEditEnding);
            
            #line default
            #line hidden
            return;
            case 3:
            this.colLL = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 4:
            this.colL = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 5:
            this.colH = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 6:
            this.colHH = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 7:
            this.btnCommun = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml"
            this.btnCommun.Click += new System.Windows.RoutedEventHandler(this.btnCommun_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnOK = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml"
            this.btnOK.Click += new System.Windows.RoutedEventHandler(this.btnOK_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\..\Communication\View\ConfAlarmWarningWin.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

