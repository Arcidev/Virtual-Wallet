using Cimbalino.Toolkit.Controls;
using System.Threading.Tasks;
using VirtualWallet.Pages;
using Windows.UI.Xaml;

namespace VirtualWallet.Helpers
{
    public static class MenuHelper
    {
        private static HamburgerFrame RootFrame { get { return Window.Current.Content as HamburgerFrame; } }

        private static HamburgerTitleBar Header { get { return RootFrame?.Header as HamburgerTitleBar; } }

        private static HamburgerPaneControl Pane { get { return RootFrame?.Pane as HamburgerPaneControl; } }

        public static void SetHeader(string header)
        {
            var headerElement = Header;
            if (headerElement == null)
                return;

            headerElement.Title = header;
        }

        public static async Task ReloadData()
        {
            await Pane?.ReloadData();
        }

        public static void ReloadTexts(string header)
        {
            Pane.ReloadTexts();
            SetHeader(header);
        }
    }
}
