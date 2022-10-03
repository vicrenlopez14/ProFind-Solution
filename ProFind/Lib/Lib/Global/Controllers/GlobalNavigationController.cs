using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace ProFind.Lib.Global.Controllers
{
    public class GlobalNavigationController : IViewNavigator<Type, Frame>
    {
        private static Stack<Type> pagesStack = new Stack<Type>();
        private static Frame BaseFrame;

        public bool GoBack()
        {
            // Verify if 
            if (pagesStack.Pop() == null) return false;

            NavigateTo(pagesStack.Pop());
            return true;
        }

        public void Init(Frame centralController, Type initialView)
        {
            Init(centralController);
            NavigateTo(initialView);
        }

        public void Init(Frame centralController, Type initialView, object parameter)
        {
            Init(centralController);
            NavigateTo(initialView, parameter);
        }


        public void Init(Frame centralController)
        {
            BaseFrame = centralController;
        }

        public void NavigateTo(Type view)
        {
            BaseFrame.Navigate(view);
        }

        public void NavigateTo(Type view, object parameter)
        {
            BaseFrame.Navigate(view, parameter);
        }
    }

}
