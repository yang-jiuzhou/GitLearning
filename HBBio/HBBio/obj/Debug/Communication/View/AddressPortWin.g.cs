﻿#pragma checksum "..\..\..\..\Communication\View\AddressPortWin.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A41B462869F4E58B43B99F94A8B86EBE229E3CD890EECA489CDE136061908D83"
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
using HBBio.Share;
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
    /// AddressPortWin
    /// </summary>
    public partial class AddressPortWin : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\..\Communication\View\AddressPortWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgvAddress;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Communication\View\AddressPortWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HBBio.Share.IconButton btnAddAddress;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Communication\View\AddressPortWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HBBio.Share.IconButton btnDelAddress;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Communication\View\AddressPortWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgvPort;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Communication\View\AddressPortWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HBBio.Share.IconButton btnAddPort;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Communication\View\AddressPortWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HBBio.Share.IconButton btnDelPort;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\Communication\View\AddressPortWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOK;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\Communication\View\AddressPortWin.xaml"
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
            System.Uri resourceLocater = new System.Uri("/Bio-LabChrom;component/communication/view/addressportwin.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Communication\View\AddressPortWin.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            
            #line 9 "..\..\..\..\Communication\View\AddressPortWin.xaml"
            ((HBBio.Communication.AddressPortWin)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dgvAddress = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.btnAddAddress = ((HBBio.Share.IconButton)(target));
            return;
            case 4:
            this.btnDelAddress = ((HBBio.Share.IconButton)(target));
            return;
            case 5:
            this.dgvPort = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.btnAddPort = ((HBBio.Share.IconButton)(target));
            return;
            case 7:
            this.btnDelPort = ((HBBio.Share.IconButton)(target));
            return;
            case 8:
            this.btnOK = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\..\Communication\View\AddressPortWin.xaml"
            this.btnOK.Click += new System.Windows.RoutedEventHandler(this.btnOK_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\..\Communication\View\AddressPortWin.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
