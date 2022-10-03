using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using ProFind.Lib.Global.Controllers;

namespace ProFind.Lib.AdminNS.Controllers
{
    public class InAppNavigationController : IViewNavigator<Type, Frame>
    {
        private static Stack<Type> pagesStack = new Stack<Type>();
        private static Frame BaseFrame;

        public void Init(Frame centralController)
        {
            BaseFrame = centralController;
        }

        #region BackMethods

        public bool GoBack()
        {
            // Verify if 
            if (pagesStack.Pop() == null) return false;

            NavigateTo(pagesStack.Pop());
            return true;
        }

        public bool GoBack(object parameter)
        {
            // Verify if 
            if (pagesStack.Pop() == null) return false;

            NavigateTo(pagesStack.Pop(), parameter);
            return true;
        }

        #endregion

        #region NavigateMethods
        public void NavigateTo(Type view)
        {
            pagesStack.Push(view);
            BaseFrame.Navigate(view);
        }

        public void NavigateTo(Type view, object parameter)
        {
            pagesStack.Push(view);
            BaseFrame.Navigate(view, parameter);
        }
        #endregion

        #region StateManagementMethods

        public static void Clear() => pagesStack.Clear();

        public static bool CanGoBack() => pagesStack.Peek() != null;

        #endregion
    }
}
