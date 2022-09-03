﻿using ProFind.Lib.Global.Services;
using ProFind.Lib.Global.Services.Models;
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

namespace ProFind.Lib.AdminNS.Views.CRUDPages.RankNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Create_Page : Page
    {
        public Create_Page()
        {
            this.InitializeComponent();
        }

        private async void Title_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var toCreateClien = new Rank(int.Parse(""), Name_tb.Text);


            var result = await APIConnection.GetConnection.PostRankAsync(toCreateClien);
        }

        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
