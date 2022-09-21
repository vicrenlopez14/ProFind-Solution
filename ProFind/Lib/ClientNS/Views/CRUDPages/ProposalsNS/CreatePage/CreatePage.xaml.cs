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

namespace ProFind.Lib.ClientNS.Views.CRUDPages.ProposalsNS.CreatePage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CreatePage : Page
    {
        private byte[] imageBytes;
        Client IdC;
        Professional IdP;
        public CreatePage()
        {
            this.InitializeComponent();
            AddEvents();
        }
        private void AddEvents()
        {
            Title_tb.OnEnterNextField();
            Description_tb.OnEnterNextField();

        }

        private async void PictureSelection_btn_Click(object sender, RoutedEventArgs e)
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

        private void Title_tb_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (FieldsChecker.OnlyLetters(e)) e.Handled = true;
            else e.Handled = false;

        }

        private void Description_tb_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (FieldsChecker.OnlyLetters(e)) e.Handled = true;
            else e.Handled = false;

        }

        private async void Create_btn_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {


                var toCreateAdmin = new Proposal { IdPp = "", TitlePp = Title_tb.Text, DescriptionPp = Description_tb.Text, SuggestedStart = ExpectedBegin_dp.Date, SuggestedEnd = Theend.Date, PicturePp = imageBytes, Seen = false, IdC3 = IdC.IdC, IdP3 = IdP.IdP };




                var result = await APIConnection.GetConnection.PostProposalAsync(toCreateAdmin);
                new GlobalNavigationController().NavigateTo(typeof(ProFind.Lib.ClientNS.Views.CRUDPages.ProposalsNS.ListPage.ListPAge));


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
               
            }
        }

        private void Reset_btn_Click(object sender, RoutedEventArgs e)
        {
            Title_tb.Text = "";
            Description_tb.Text = "";

        }
    }
}
