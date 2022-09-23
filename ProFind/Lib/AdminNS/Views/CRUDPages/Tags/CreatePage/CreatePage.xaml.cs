﻿using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.Tags.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CreatePage : Page
    {
        
        public CreatePage()
        {
            this.InitializeComponent();
        }

        private async void Create_btn_Click_1(object sender, RoutedEventArgs e)
        {
            if (Name_tb.Text.Length <= 3)
            {
                var dialog = new MessageDialog("The name must be valid");
                await dialog.ShowAsync();
                return;
            }

            try
            {
                var toCreateTag = new Tag { IdT = "", NameT = Name_tb.Text };
                var result = await APIConnection.GetConnection.PostTagAsync(toCreateTag);
               
            }
            catch (ProFindServicesException ex)
            {
                if (ex.StatusCode == 200 || ex.StatusCode == 201 || ex.StatusCode == 204)
                {
                    var dialog = new MessageDialog("The Tags has been created.");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("There was an error creating the Tags, try again later.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                new InAppNavigationController().NavigateTo(typeof(ProFind.Lib.AdminNS.Views.CRUDPages.Tags.ListPage.ListPage));
            }
        }

        private void Name_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Name_tb_KeyDown(object sender, KeyRoutedEventArgs e)
        {

        }
    }
}
