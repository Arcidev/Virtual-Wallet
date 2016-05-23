using Cimbalino.Toolkit.Controls;
using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;

namespace BL.Service.Menu
{
    public class MenuUnil
    {
        private static ResourceLoader resources;

        public static void setHeader(String header)
        {
            if (resources == null)
            {
                resources = ResourceLoader.GetForCurrentView();
            }
            var rootFrame = Window.Current.Content as HamburgerFrame;
            var headerElement = rootFrame.Header as HamburgerTitleBar;
            headerElement.Title = resources.GetString(header);
        }
    }
}
