﻿#pragma checksum "..\..\..\..\Pages\PagesAdmin\PageEditRoles.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D8AF2D6D107C48BA0C8A2D7DE5C31671A2F4BC6704E2A0FF10BD58092C071010"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Project.Pages.PagesAdmin;
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


namespace Project.Pages.PagesAdmin {
    
    
    /// <summary>
    /// PageEditRoles
    /// </summary>
    public partial class PageEditRoles : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\..\..\Pages\PagesAdmin\PageEditRoles.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbxId;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Pages\PagesAdmin\PageEditRoles.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbxName;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Pages\PagesAdmin\PageEditRoles.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbxAccessLevels;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Pages\PagesAdmin\PageEditRoles.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveChanges;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Pages\PagesAdmin\PageEditRoles.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDelete;
        
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
            System.Uri resourceLocater = new System.Uri("/Project;component/pages/pagesadmin/pageeditroles.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\PagesAdmin\PageEditRoles.xaml"
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
            this.txtbxId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtbxName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtbxAccessLevels = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.btnSaveChanges = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\..\Pages\PagesAdmin\PageEditRoles.xaml"
            this.btnSaveChanges.Click += new System.Windows.RoutedEventHandler(this.btnSaveChanges_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\..\Pages\PagesAdmin\PageEditRoles.xaml"
            this.btnDelete.Click += new System.Windows.RoutedEventHandler(this.btnDelete_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

