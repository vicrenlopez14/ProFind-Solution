﻿using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ProfessionalNS.Views.CRUDPage.NotificationNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CreatePage : Page
    {
        private byte[] imageBytes;

        Notification toManipulate = new Notification();
        Professional id; 

        public CreatePage()
        {
            this.InitializeComponent();
        }

        private async void Create_btn_Click(object sender, RoutedEventArgs e)
        {
           
            if (string.IsNullOrEmpty(Title_tb1.Text))
            {

                var dialog = new MessageDialog("The field is empty");
                await dialog.ShowAsync();
                return;
            }
            else if (string.IsNullOrEmpty(Description_tb.Text))
            {
                var dialog = new MessageDialog("The field is empty");
                await dialog.ShowAsync();
                return;
            }

          


            try
            {
              
                byte[] da = toManipulate.PictureN = await (await PickFileHelper.PickImage()).ToByteArrayAsync();

                var toCreateClien = new Notification { IdN = "", TitleN = Title_tb1.Text, DescriptionN = Description_tb.Text, IdPj2 = Project_cb.Text, PictureN = imageBytes, DateTimeIssuedN = Caledar.Date, IdPj2Navigation = toManipulate.IdPj2Navigation };


                var result = await APIConnection.GetConnection.PostNotificationAsync(toCreateClien);



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
               
            }

        }

        private async void PictureSelection_btn_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private async void PictureSelection_btn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Title_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
 

        }

        private void Description_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
