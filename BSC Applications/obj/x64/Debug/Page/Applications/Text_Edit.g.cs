﻿#pragma checksum "C:\Users\jghr\OneDrive\Bit\BSC-Applications\BSC Applications\Page\Applications\Text_Edit.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8C74BDB15397BF452740816DBFE1BE60"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BSC_Applications.Page.Applications
{
    partial class File_View : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Page\Applications\Text_Edit.xaml line 24
                {
                    this.Text = (global::Windows.UI.Xaml.Controls.RichEditBox)(target);
                }
                break;
            case 3: // Page\Applications\Text_Edit.xaml line 18
                {
                    this.New = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.New).Click += this.New_Click;
                }
                break;
            case 4: // Page\Applications\Text_Edit.xaml line 19
                {
                    this.Save = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.Save).Click += this.Save_Click;
                }
                break;
            case 5: // Page\Applications\Text_Edit.xaml line 20
                {
                    this.Open = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.Open).Click += this.Open_Click;
                }
                break;
            case 6: // Page\Applications\Text_Edit.xaml line 21
                {
                    this.Help = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.Help).Click += this.Help_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
