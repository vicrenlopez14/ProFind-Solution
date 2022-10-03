using ProFind.Lib.Global.Controllers;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.Global.Views.ServerNotAvailable
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ServerNotAvailablePage : Page
    {
        public ServerNotAvailablePage()
        {
            this.InitializeComponent();

            Retry();
        }

        public static async void Retry()
        {

            while (true)
            {
                try
                {
                    // Retry
                   // await WebAPIConnection.IsServerAlive();

                    // If everything went ok, go back
                    new GlobalNavigationController().GoBack();
                }
                catch
                {
                    // If exception is thrown again, try back
                    continue;
                }
            }
        }

        private void Hyperlink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {

        }
    }
}
