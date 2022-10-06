using ProFind.Lib.AdminNS.Views.Operations.PasswordChangePage;
using ProFind.Lib.Global.Controllers;
using ProFind.Lib.Global.Services;
using ProFind.Lib.Global.Views.FirstUsePage;
using ProFind.Lib.Global.Views.InitPage;
using ProFind.Lib.Global.Views.ServerNotAvailable;
using Syncfusion.XPS;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ProFind
{
    /// <summary>
    /// Proporciona un comportamiento específico de la aplicación para complementar la clase Application predeterminada.
    /// </summary>
    sealed partial class App : Windows.UI.Xaml.Application
    {
        private Windows.System.ProtocolForResultsOperation _operation = null;

        /// <summary>
        /// Inicializa el objeto de aplicación Singleton. Esta es la primera línea de código creado
        /// ejecutado y, como tal, es el equivalente lógico de main() o WinMain().
        /// </summary>
        public App()
        {

            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.UnhandledException += (sender, e) =>
            {
                e.Handled = true;
                // Message dialog
                var dialog = new Windows.UI.Popups.MessageDialog("This is an unimplented feature or a bug, please contact our team at support@profind.com, or wait for newer versions of ProFind.");
                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok"));
                dialog.ShowAsync();
                System.Diagnostics.Debug.WriteLine(e.Exception);
            };
            
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            if (args.Kind == ActivationKind.Protocol)
            {
                ProtocolActivatedEventArgs eventArgs = args as ProtocolActivatedEventArgs;

                InitializeFromActivated(eventArgs);
            }
        }

        private void InitializeFromActivated(IActivatedEventArgs args)
        {
            // Window management
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                rootFrame = new Frame();
                Window.Current.Content = rootFrame;
            }

            // Code specific to launch for results
            var protocolForResultsArgs = (ProtocolForResultsActivatedEventArgs)args;

            if (protocolForResultsArgs.Uri.Segments[1] == "recover-password")
            {
                switch (protocolForResultsArgs.Uri.Segments[2])
                {
                    //for admins
                    case "admin":
                        rootFrame.Navigate(typeof(Lib.AdminNS.Views.Operations.PasswordChangePage.PasswordChangePage), protocolForResultsArgs);
                        new GlobalNavigationController().Init(rootFrame, typeof(PasswordChangePage), protocolForResultsArgs.Uri.Segments[3]);
                        break;
                    //for professionals
                    case "professional":
                        rootFrame.Navigate(typeof(Lib.ProfessionalNS.Views.Operations.PasswordChangePage.PasswordChangePage), protocolForResultsArgs);
                        new GlobalNavigationController().Init(rootFrame, typeof(PasswordChangePage), protocolForResultsArgs.Uri.Segments[3]);
                        break;
                    // for clients
                    case "client":
                        rootFrame.Navigate(typeof(Lib.ClientNS.Views.Operations.PasswordChangePage.PasswordChangePage), protocolForResultsArgs);
                        new GlobalNavigationController().Init(rootFrame, typeof(PasswordChangePage), protocolForResultsArgs.Uri.Segments[3]);
                        break;
                }

                _operation = protocolForResultsArgs.ProtocolForResultsOperation;
                _operation.ReportCompleted(new ValueSet { { "redirected", true } });
            }

            Window.Current.Activate();
        }

        /// <summary>
        /// Se invoca cuando la aplicación la inicia normalmente el usuario final. Se usarán otros puntos
        /// de entrada cuando la aplicación se inicie para abrir un archivo específico, por ejemplo.
        /// </summary>
        /// <param name="e">Información detallada acerca de la solicitud y el proceso de inicio.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // No repetir la inicialización de la aplicación si la ventana tiene contenido todavía,
            // solo asegurarse de que la ventana está activa.
            if (rootFrame == null)
            {
                // Crear un marco para que actúe como contexto de navegación y navegar a la primera página.
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Cargar el estado de la aplicación suspendida previamente
                }

                // Poner el marco en la ventana actual.
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // Cuando no se restaura la pila de navegación, navegar a la primera página,
                    // configurando la nueva página pasándole la información requerida como
                    //parámetro de navegación
                    try
                    {
                        var admins = await APIConnection.GetConnection.GetAdminsAsync();
                        var areThereAdmins = admins == null ? false : admins.Count() > 0;

                        try
                        {
                            if (areThereAdmins)
                            {
                                //rootFrame.Navigate(typeof(Lib.ClientNS.Views.InitPage.InitPage), e.Arguments);
                                new GlobalNavigationController().Init(rootFrame, typeof(Lib.ClientNS.Views.InitPage.InitPage));

                            }
                            else
                            {
                                //rootFrame.Navigate(typeof(FirstUsePage), e.Arguments);
                                new GlobalNavigationController().Init(rootFrame, typeof(FirstUsePage));
                            }
                        }
                        catch (NetworkInformationException)
                        {

                            new GlobalNavigationController().Init(rootFrame, typeof(ServerNotAvailablePage));

                        }


                    }
                    catch (Exception ex)
                    {
                        new GlobalNavigationController().Init(rootFrame, typeof(ServerNotAvailablePage));

                    }                    }
                    // Asegurarse de que la ventana actual está activa.
                    Window.Current.Activate();
            }
        }


        /// <summary>
        /// Se invoca cuando la aplicación la inicia normalmente el usuario final. Se usarán otros puntos
        /// </summary>
        /// <param name="sender">Marco que produjo el error de navegación</param>
        /// <param name="e">Detalles sobre el error de navegación</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Se invoca al suspender la ejecución de la aplicación. El estado de la aplicación se guarda
        /// sin saber si la aplicación se terminará o se reanudará con el contenido
        /// de la memoria aún intacto.
        /// </summary>
        /// <param name="sender">Origen de la solicitud de suspensión.</param>
        /// <param name="e">Detalles sobre la solicitud de suspensión.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Guardar el estado de la aplicación y detener toda actividad en segundo plano
            deferral.Complete();
        }

    }
}
