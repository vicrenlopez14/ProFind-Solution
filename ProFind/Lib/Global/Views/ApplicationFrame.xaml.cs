﻿using UWP_ProFind.Lib.Global.Controllers;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_ProFind.Lib.Global.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ApplicationFrame : Page
    {
        public ApplicationFrame()
        {
            this.InitializeComponent();
            new GlobalNavigationController().Init(ApplicationFrameControl, typeof(InitPage.InitPage));
        }
    }
}
