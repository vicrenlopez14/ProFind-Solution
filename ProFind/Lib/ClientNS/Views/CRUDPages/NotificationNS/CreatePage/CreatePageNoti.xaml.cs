﻿using ProFind.Lib.Global.Controllers;
using ProFind.Lib.Global.Helpers;
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

namespace ProFind.Lib.ClientNS.Views.CRUDPages.NotificationNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CreatePageNoti : Page
    {
        private byte[] imageBytes;
        public CreatePageNoti()
        {
            this.InitializeComponent();
        }

        private async void PictureSelection_btn_Checked(object sender, RoutedEventArgs e)
        {
            try
            {


                var file = await PickFileHelper.PickImage();

                if (file != null)
                {
                    SelectedPicture_tbk.Text = file.Name;
                    imageBytes = await file.ToByteArrayAsync();

                    //SelectedPicture_pp.ProfilePicture = imageBytes.ToBitmapImage();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                PictureSelection_btn.IsChecked = false;
            }
        }

        private void Title_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Description_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

               

                var toCreateClien = new Notification { TitleN = Title_tb.Text, DescriptionN = Description_tb.Text, PictureN = imageBytes };

            

                var result = await APIConnection.GetConnection.PostNotificationAsync(toCreateClien);

                new GlobalNavigationController().NavigateTo(typeof(ProFind.Lib.ClientNS.Views.CRUDPages.NotificationNS.ReadPage.ReadPage));


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }
    }
}