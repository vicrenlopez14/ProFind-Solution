using Azure;
using Azure.Communication;
using Azure.Communication.Chat;
using ProFind.Lib.AdminNS.Controllers;
using ProFind.Lib.ClientNS.Controllers;
using ProFind.Lib.ProfessionalNS.Controllers;
using Syncfusion.UI.Xaml.Controls.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Popups;
using ChatMessage = Azure.Communication.Chat.ChatMessage;

namespace ProFind.Lib.Global.Services
{
    public class ProFindChatClient
    {
        private string professionalId, clientId;
        private string _threadId;
        private ChatClient _client;
        private ChatThreadClient _chatThreadClient;
        private AsyncPageable<ChatThreadItem> _threadsOfLoggedUser;

        public ProFindChatClient(string professionalId, string clientId)
        {
            this.professionalId = professionalId;
            this.clientId = clientId;
        }

        public async void StartChat()
        {
            // Fetch the communication id for the logged user, professional-and-client-agnostic
            string communicationId = GetLoggedUserCommunicationId();

            // Fetch tokens and related info
            var info = await APIConnection.GetConnection.GetChatAccessInfoAsync(communicationId);

            // Get the thread id for the chat between these two users
            _threadId = await APIConnection.GetConnection.GetThreadIdForChatAsync(token: info.Token, professionalId: professionalId, clientId: clientId);

            // Create the chat client connection
            _client = new ChatClient(new Uri(info.EndPointUri), new Azure.Communication.CommunicationTokenCredential(info.Token));

            // Create the THREAD connection
            _chatThreadClient = _client.GetChatThreadClient(threadId: _threadId);
        }

        #region Extractors
        private static string GetLoggedUserCommunicationId()
        {
            var communicationId = "";

            if (LoggedAdminStore.LoggedAdmin != null)
            {
                communicationId = LoggedAdminStore.LoggedAdmin.CommunicationIdA;
            }

            if (LoggedProfessionalStore.LoggedProfessional != null)
            {
                communicationId = LoggedProfessionalStore.LoggedProfessional.CommunicationIdP;
            }

            if (LoggedClientStore.LoggedClient != null)
            {
                communicationId = LoggedClientStore.LoggedClient.CommunicationIdC;
            }

            return communicationId;
        }
        private static (string, string) GetLoggedUserIdAndDisplayName()
        {
            var displayName = "Unidentified user";
            var communicationId = "";
            if (LoggedAdminStore.LoggedAdmin != null)
            {
                communicationId = LoggedAdminStore.LoggedAdmin.CommunicationIdA;
                displayName = LoggedAdminStore.LoggedAdmin.NameA;
            }

            if (LoggedProfessionalStore.LoggedProfessional != null)
            {
                communicationId = LoggedProfessionalStore.LoggedProfessional.CommunicationIdP;
                displayName = LoggedProfessionalStore.LoggedProfessional.NameP;
            }

            if (LoggedClientStore.LoggedClient != null)
            {
                communicationId = LoggedClientStore.LoggedClient.CommunicationIdC;
                displayName = LoggedClientStore.LoggedClient.NameC;
            }

            return (displayName, communicationId);
        }
        #endregion

        public async Task<bool> SendMessage(string text)
        {
            SendChatMessageOptions sendChatMessageOptions = new SendChatMessageOptions()
            {
                Content = text,
                MessageType = ChatMessageType.Text
            };

            try
            {
                SendChatMessageResult sendChatMessageResult = await _chatThreadClient.SendMessageAsync(sendChatMessageOptions);

                string messageId = sendChatMessageResult.Id;

                return true;
            }
            catch
            {
                // Message dialog
                await new MessageDialog("Message could not be sent").ShowAsync();

                return false;
            }
        }

        public async void HandleMessages(Func<ChatMessage, dynamic> OnMessageArrived)
        {
            try
            {
                AsyncPageable<ChatMessage> allMessages = _chatThreadClient.GetMessagesAsync();
                var enumerator = allMessages.GetAsyncEnumerator();
                while (await enumerator.MoveNextAsync())
                {
                    ChatMessage message = enumerator.Current;

                    OnMessageArrived(message);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error while handling messages >>>" + ex.Message);
            }
        }
    }
}
