using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BSC_Applications.Page.lib
{
    class Assets
    {
        public static Uri IncompleteFeature
        {
            get 
            { 
                if(Application.Current.RequestedTheme == 0)
                {
                    return new Uri("https://raw.githubusercontent.com/BitSoftwareCo/BSC-Applications/main/Assets/Light/Incomplete%20Feature-Light.png");
                } else
                {
                    return new Uri("https://raw.githubusercontent.com/BitSoftwareCo/BSC-Applications/main/Assets/Dark/Incomplete%20Feature-Dark.png");
                }
            }
        }
    }
}