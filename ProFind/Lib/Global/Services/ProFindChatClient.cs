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
        private string _threadId;
        private ChatClient _client;
        private ChatThreadClient _chatThreadClient;
        private AsyncPageable<ChatThreadItem> _threadsOfLoggedUser;

        public ProFindChatClient()
        {

        }

        public async void StartChatWith()
        {
            string communicationId = GetLoggedUserId();

            var info = await APIConnection.GetConnection.GetChatAccessInfoAsync(communicationId);

            _client = new ChatClient(new Uri(info.EndPointUri), new Azure.Communication.CommunicationTokenCredential(info.Token));

            CreateChatThread();
        }

        #region Extractors
        private static string GetLoggedUserId()
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

        public async void CreateChatThread()
        {
            (string displayName, string communicationId) = GetLoggedUserIdAndDisplayName();

            var chatParticipant = new ChatParticipant(identifier: new CommunicationUserIdentifier(id: communicationId))
            {
                DisplayName = displayName
            };

            CreateChatThreadResult createChatThreadResult = await _client.CreateChatThreadAsync(topic: $"Chat with {displayName}", participants: new List<ChatParticipant> { chatParticipant });
            ChatThreadClient chatThreadClient = _client.GetChatThreadClient(threadId: createChatThreadResult.ChatThread.Id);
            _threadId = chatThreadClient.Id;
        }



        public void GetChatThreadClient()
        {
            _chatThreadClient = _client.GetChatThreadClient(threadId: _threadId);
        }

        public void ListChatThreads()
        {
            _threadsOfLoggedUser = _client.GetChatThreadsAsync();
           
        }

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
