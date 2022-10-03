using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace ProFind.Lib.Global.Helpers
{
    public class URLOpenerUtil
    {
        public static async void OpenURL(string url)
        {
            try
            {
                // The URI to launch
                string uriToLaunch = url;

                // Create a Uri object from a URI string 
                var uri = new Uri(uriToLaunch);

                // Launch the URI
                async void DefaultLaunch()
                {
                    // Launch the URI
                    var success = await Windows.System.Launcher.LaunchUriAsync(uri);

                    if (success)
                    {
                        // URI launched
                    }
                    else
                    {
                        // URI launch failed
                    }
                }

                DefaultLaunch();
            }
            catch

                (Exception ex)
            {
                var dialog = new MessageDialog("An error has occured during the process.");
                await dialog.ShowAsync();
            }
        }
    }
}
