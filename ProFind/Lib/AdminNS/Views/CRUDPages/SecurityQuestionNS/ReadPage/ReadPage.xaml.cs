using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Controllers;
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

namespace ProFind.Lib.AdminNS.Views.CRUDPages.SecurityQuestionNS.ReadPage
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class ReadPage : Page
    {
        Securityquestion toManipulate = new Securityquestion();

        public ReadPage()
        {
            this.InitializeComponent();
            InitializeData();   
        }

        private async void InitializeData()
        {
            QuestionsListView.ItemsSource = await APIConnection.GetConnection.GetSecurityquestionsAsync();
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            new GlobalNavigationController().NavigateTo(typeof(Lib.AdminNS.Views.CRUDPages.SecurityQuestionNS.UpdatePage.UpdatePage));
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            await APIConnection.GetConnection.DeleteSecurityquestionAsync(toManipulate.IdSq);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            new InAppNavigationController().GoBack();
        }
    }
}
