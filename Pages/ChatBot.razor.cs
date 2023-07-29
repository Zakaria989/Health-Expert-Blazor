using Health_expert_B.Data;
using Health_expert_B.Models;
using Health_expert_B.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Health_expert_B.Pages
{
    public partial class ChatBot : ComponentBase
    {
        private string _userQuestion = string.Empty;
        private List<Message> _converstionHistory = new();
        public List<Message> MessagesHistory = new();
        private bool _isSendingMessage;
        ElementReference textAreaRef;
        ElementReference messages;
        private UserInfo userInfo;

        protected override async Task OnInitializedAsync()
        {
            string messageHistory = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "messageHistory");
            string conversationHistory = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "conversationHistory");
            userInfo = User.User;
            if (!string.IsNullOrEmpty(conversationHistory))
            {
                MessagesHistory = JsonSerializer.Deserialize<List<Message>>(messageHistory);
                _converstionHistory = JsonSerializer.Deserialize<List<Message>>(conversationHistory);
                if (_converstionHistory.Count >= 5)
                {
                    while (_converstionHistory.Count >= 5)
                    {
                        _converstionHistory.RemoveAt(1);
                    }
                }
                _converstionHistory.Insert(0, new Message { role = "system", content = Context.ContextFiller(userInfo) });
            }
            else
            {

                _converstionHistory.Add(new Message { role = "system", content = Context.ContextFiller(userInfo) });
            }
            await base.OnInitializedAsync();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            userInfo = User.User;
            if (firstRender)
            {
                // Perform your JavaScript interop calls here
                string messageHistory = JsonSerializer.Serialize(MessagesHistory);
                await JSRuntime.InvokeVoidAsync("localStorage.setItem", "messageHistory", messageHistory);

                ScrollInputArea();

                string conversationHistory = JsonSerializer.Serialize(_converstionHistory);
                await JSRuntime.InvokeVoidAsync("localStorage.setItem", "conversationHistory", conversationHistory);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task HandleKeyPress(KeyboardEventArgs e)
        {
            if (e.Key is not "Enter") return;
            await SendMessage();
        }

        private async Task SendMessage()
        {
            if (string.IsNullOrWhiteSpace(_userQuestion)) return;
            AddUserQuestionToConverstion();
            StateHasChanged();
            await CreateCompletion();
            ClearInput();
            StateHasChanged();

            ScrollToBottom();

            string messageHistory = JsonSerializer.Serialize(MessagesHistory);
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", "messageHistory", messageHistory);
            string converstionHistory = JsonSerializer.Serialize(_converstionHistory);
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", "conversationHistory", converstionHistory);
        }


        private async void ScrollInputArea()
        {
            IJSObjectReference jsRef = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./Pages/ChatBot.razor.js");
            await jsRef.InvokeVoidAsync("focusElement", textAreaRef);
            StateHasChanged();
        }

        private async void ScrollToBottom()
        {
            IJSObjectReference jsRef = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./Pages/ChatBot.razor.js");
            await jsRef.InvokeVoidAsync("focusElement", messages);
            StateHasChanged();
        }
        private void ClearConverstions()
        {
            ClearInput();
            _converstionHistory.Clear();
            _converstionHistory.Insert(0, new Message { role = "system", content = Context.ContextFiller(userInfo) });
            MessagesHistory.Clear();
        }
        private void ClearInput()
         => _userQuestion = string.Empty;

        private async Task CreateCompletion()
        {
            _isSendingMessage = true;
            var assistantResponse = await OpenAIService.CreateChatCompletion(_converstionHistory);
            _converstionHistory.Add(assistantResponse);
            MessagesHistory.Add(assistantResponse);
            _isSendingMessage = false;
            TokenLimiter(_converstionHistory);

        }


        public void TokenLimiter(List<Message> conversionHistory)
        {
            if (conversionHistory.Count >= 5)
            {
                conversionHistory.RemoveAt(1);
                conversionHistory.RemoveAt(1);
            }
        }

        private void AddUserQuestionToConverstion()
        {
            _converstionHistory.Add(new Message { role = "user", content = _userQuestion });
            MessagesHistory.Add(new Message { role = "user", content = _userQuestion });
        }


        [Inject]
        public OpenAIService OpenAIService { get; set; }
        [Inject]
        public UserInfoService User { get; set; }
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }


        public List<Message> Messages => MessagesHistory.Where(c => c.role is not "system").ToList();
    }


}
