﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Services;
using Profession = ProFind.Lib.Global.Services.Profession;
using ProFind.Lib.Global.Helpers;
using Windows.UI.Popups;
using System;
using ProFind.Lib.AdminNS.Controllers;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.AdminNS.Views.CRUDPages.ProfessionNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Profession_Create : Page
    {
        private string picturestring;
        private string bannerstring;
        public Profession_Create()
        {
            this.InitializeComponent();
        }

        private async void Create_btn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Title_tb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Name_tb_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
           

        }

        private async void Create_btn_Click_1(object sender, RoutedEventArgs e)
        {
            if (!FieldsChecker.CheckName(Name_tb.Text))
            {
                var dialog = new MessageDialog("The name must be valid");
                await dialog.ShowAsync();
                return;
            }
            

            try
            {
                var toCreateClien = new Profession { IdPfs = 0, NamePfs = Name_tb.Text, DescriptionPfs = Description_tb.Text, BannerPfs = bannerstring, PicturePfs = picturestring };
                var result = await APIConnection.GetConnection.PostProfessionAsync(toCreateClien);
                var dialog = new MessageDialog("The profession has been created");
                await dialog.ShowAsync();
            }
            catch (ProFindServicesException ex)
            {
                if(ex.StatusCode>=200 && ex.StatusCode <= 205)
                {
                    var dialog = new MessageDialog("The profession has been created");
                    await dialog.ShowAsync();
                }
                else
                {
                    var dialog = new MessageDialog("There was a problem while creating the profession, try again later.");
                    await dialog.ShowAsync();
                }
            }
            finally
            {
                new InAppNavigationController().NavigateTo(typeof(ListPage.List_Page));
            }
        }

        private async void LoadPicture_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                var selectedImage = await PickFileHelper.PickImage();
                if (selectedImage != null)
                {
                    picturestring = await selectedImage.ToBase64StringAsync();
                    
                    ProfessionPicture_img.Source = await picturestring.FromBase64String();
                }


            }
            catch (Exception ex)
            {
                // friendly error dialog
                var dialog = new MessageDialog("Image has not been loaded");
                await dialog.ShowAsync();

                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }

        private async void LoadBanner_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var selectedbanner = await PickFileHelper.PickImage();
                if (selectedbanner != null)
                {
                    bannerstring = await selectedbanner.ToBase64StringAsync();

                    ProfessionBanner_img.Source = await bannerstring.FromBase64String();
                }
            }
            catch (Exception ex)
            {
                // friendly error dialog
                var dialog = new MessageDialog("Banner has not been loaded");
                await dialog.ShowAsync();

                Console.WriteLine(ex.Message);
            }
            finally
            {
            }
        }
    }
}
