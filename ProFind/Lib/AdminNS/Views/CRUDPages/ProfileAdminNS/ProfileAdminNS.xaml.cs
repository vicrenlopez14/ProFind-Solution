﻿using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.ClientNS.Controllers;
using ProFind.Lib.Global.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProfileAdminNS
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ProfileAdminNS : Page
    {
        public ProfileAdminNS()
        {
            this.InitializeComponent();
            InitializeData();
        }

        private async void InitializeData()
        {
            var loggendAdmin = LoggedAdminStore.LoggedAdmin;
            var client = await APIConnection.GetConnection.GetAdminsAsync();
            var RelatedAdmin = new List<Admin>();

            var RelatedClient2 = client.Where(c => c.IdA == loggendAdmin.IdA).ToList();

            RelatedAdmin.AddRange(RelatedClient2);
            ListViewProfileAdmin.ItemsSource = RelatedAdmin;
        }
    }
}
