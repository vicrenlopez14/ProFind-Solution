using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.Map
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Map : Page
    {
        public string location;
       
        public Map()
        {
            this.InitializeComponent();
            AddMapIcon();
        }

       

        private void AddMapIcon()
        {
            BasicGeoposition location = new BasicGeoposition();
            location.Latitude = 13.72334145;
            location.Longitude = -89.20497241145983;

            MapControl1.Center = new Geopoint(location);

        }

       
    }
}
