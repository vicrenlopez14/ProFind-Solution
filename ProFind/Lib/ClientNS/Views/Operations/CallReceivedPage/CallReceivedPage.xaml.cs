﻿using Azure.Communication.Calling;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.Global.Helpers;
using ProFind.Lib.Global.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProFind.Lib.ClientNS.Views.Operations.CallReceivedPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CallReceivedPage : Page
    {
        CallClient callClient;
        CallAgent callAgent;
        Call call;
        DeviceManager deviceManager;
        LocalVideoStream[] localVideoStream;
        Dictionary<String, RemoteParticipant> remoteParticipantDictionary;
        private IncomingCall _incomingCall;

        private bool IsMicOn = false;
        private bool IsCameraOn = false;
        private Professional professionalWhoCalled;


        public CallReceivedPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter.GetType() == typeof((Professional, IncomingCall)))
            {
                (professionalWhoCalled, _incomingCall) = ((Professional, IncomingCall))e.Parameter;

                LoggedUserName_tb.Text = professionalWhoCalled.NameP;
                LoggedUser_pp.ProfilePicture = await professionalWhoCalled.PictureP.FromBase64String();

                Agent_OnIncomingCall(null, _incomingCall);
            }
            else
            {
                new InAppNavigationController().GoBack();
                return;
            }
        }

        private async void Agent_OnIncomingCall(object sender, IncomingCall incomingcall)
        {
            Debug.Assert(deviceManager.Microphones.Count > 0);
            Debug.Assert(deviceManager.Speakers.Count > 0);
            Debug.Assert(deviceManager.Cameras.Count > 0);

            if (deviceManager.Cameras.Count > 0)
            {
                VideoDeviceInfo videoDeviceInfo = deviceManager.Cameras[0];
                localVideoStream = new LocalVideoStream[1];
                localVideoStream[0] = new LocalVideoStream(videoDeviceInfo);

                Uri localUri = await localVideoStream[0].MediaUriAsync();

                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    LocalVideo.Source = localUri;
                    LocalVideo.Play();
                });

            }
            AcceptCallOptions acceptCallOptions = new AcceptCallOptions();
            acceptCallOptions.VideoOptions = new VideoOptions(localVideoStream);

            call = await incomingcall.AcceptAsync(acceptCallOptions);
            call.OnRemoteParticipantsUpdated += Call_OnRemoteParticipantsUpdated;
            call.OnStateChanged += Call_OnStateChanged;
            
            IsMicOn = true;
            IsCameraOn = true;
        } 
        
        private async void Call_OnStateChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (((Call)sender).State)
            {
                case CallState.Disconnected:
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        LocalVideo.Source = null;
                        RemoteVideo.Source = null;
                    });
                    break;

                case CallState.Connected:
                    foreach (var remoteParticipant in call.RemoteParticipants)
                    {
                        String remoteParticipantMRI = remoteParticipant.Identifier.ToString();
                        remoteParticipantDictionary.TryAdd(remoteParticipantMRI, remoteParticipant);
                        await AddVideoStreams(remoteParticipant.VideoStreams);
                        remoteParticipant.OnVideoStreamsUpdated += Call_OnVideoStreamsUpdated;
                    }
                    break;

                default:
                    break;
            }
        }

        #region VideoMethods
        private async void Call_OnVideoStreamsUpdated(object sender, RemoteVideoStreamsEventArgs args)
        {
            foreach (var remoteVideoStream in args.AddedRemoteVideoStreams)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                {
                    RemoteVideo.Source = await remoteVideoStream.Start();
                });
            }

            foreach (var remoteVideoStream in args.RemovedRemoteVideoStreams)
            {
                remoteVideoStream.Stop();
            }
        }

        private async void Agent_OnCallsUpdated(object sender, CallsUpdatedEventArgs args)
        {
            foreach (var call in args.AddedCalls)
            {
                foreach (var remoteParticipant in call.RemoteParticipants)
                {
                    String remoteParticipantMRI = remoteParticipant.Identifier.ToString();
                    remoteParticipantDictionary.TryAdd(remoteParticipantMRI, remoteParticipant);
                    await AddVideoStreams(remoteParticipant.VideoStreams);
                    remoteParticipant.OnVideoStreamsUpdated += Call_OnVideoStreamsUpdated;
                }
            }
        }

        private async void Call_OnRemoteParticipantsUpdated(object sender, ParticipantsUpdatedEventArgs args)
        {
            foreach (var remoteParticipant in args.AddedParticipants)
            {
                String remoteParticipantMRI = remoteParticipant.Identifier.ToString();
                remoteParticipantDictionary.TryAdd(remoteParticipantMRI, remoteParticipant);
                await AddVideoStreams(remoteParticipant.VideoStreams);
                remoteParticipant.OnVideoStreamsUpdated += Call_OnVideoStreamsUpdated;
            }

            foreach (var remoteParticipant in args.RemovedParticipants)
            {
                String remoteParticipantMRI = remoteParticipant.Identifier.ToString();
                remoteParticipantDictionary.Remove(remoteParticipantMRI);
            }
        }

        private async Task AddVideoStreams(IReadOnlyList<RemoteVideoStream> streams)
        {

            foreach (var remoteVideoStream in streams)
            {
                var remoteUri = await remoteVideoStream.Start();

                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    RemoteVideo.Source = remoteUri;
                    RemoteVideo.Play();
                });
            }
        }
        #endregion

        private async void Micro_Click_1(object sender, RoutedEventArgs e)
        {
            if (IsMicOn)
            {
                await call.Mute();
            }
            else
            {
                await call.Unmute();
            }

            IsMicOn = !IsMicOn;
        }

        private async void Camera_Click_1(object sender, RoutedEventArgs e)
        {
            if (IsCameraOn)
            {

                await call.StopVideo(localVideoStream[0]);
            }

            else
            {
                await call.StartVideo(localVideoStream[0]);
            }
        }


        private async void HangupButton_Click(object sender, RoutedEventArgs e)
        {
            var hangUpOptions = new HangUpOptions();
            await call.HangUpAsync(hangUpOptions);
        }



    }
}
