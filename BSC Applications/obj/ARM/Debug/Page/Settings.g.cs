﻿#pragma checksum "C:\Users\jghr\OneDrive\Bit\BSC-Applications\BSC Applications\Page\Settings.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6A2A755612938EA60215358AAF5A9577"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BSC_Applications.Page
{
    partial class Settings : 
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
            case 2: // Page\Settings.xaml line 16
                {
                    this.SoundToggle = (global::Windows.UI.Xaml.Controls.ToggleSwitch)(target);
                    ((global::Windows.UI.Xaml.Controls.ToggleSwitch)this.SoundToggle).Toggled += this.Sound_Toggled;
                }
                break;
            case 3: // Page\Settings.xaml line 20
                {
                    this.About = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.About).Click += this.About_Click;
                }
                break;
            case 4: // Page\Settings.xaml line 28
                {
                    this.website = (global::Windows.UI.Xaml.Controls.HyperlinkButton)(target);
                }
                break;
            case 5: // Page\Settings.xaml line 29
                {
                    this.docs = (global::Windows.UI.Xaml.Controls.HyperlinkButton)(target);
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
