using Health_expert_B.Data;
using Health_expert_B.Models;
using Health_expert_B.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Health_expert_B.Pages
{
    public partial class MealPlan: ComponentBase
    {
        private readonly List<Message> _prompt = new();
        private bool _isSendingMessage;
        public string MealPlanByAssistant { get; set; }
        private ElementReference textareaRef;

        protected override Task OnInitializedAsync()
        {
            UserMealPreference userMeal = UserPrefrence.UserMealPreference;
            UserInfo userInfo = UserInfo.User;
            _prompt.Add(new Message { role = "user", content = Context.ContextFillerMealPreference(userInfo,userMeal) });
            _isSendingMessage = true;
            return base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                ScrollToBottom();
                await CreateCompletion();
            }
        }

        private async void ScrollToBottom()
        {
            IJSObjectReference jsRef = await JSRuntime.InvokeAsync<IJSObjectReference>("import","./Pages/MealPlan.razor.js");
            await jsRef.InvokeVoidAsync("focusElement", textareaRef);
            StateHasChanged();
        }        
        private void NavigateToAnotherPage()
        {
            NavManager.NavigateTo("/MealPlanner");
        }
        private async Task CreateCompletion()
        {
            var assistantResponse = await OpenAIService.CreateChatCompletion(_prompt);
            MealPlanByAssistant = assistantResponse.content.ToString();
            _isSendingMessage = false;
            StateHasChanged();
        }


        [Inject]
        public OpenAIService OpenAIService { get; set; }
        [Inject]
        UserMealPrefrenceService UserPrefrence { get; set; }
        [Inject]
        UserInfoService UserInfo { get; set; }
        [Inject]
        NavigationManager NavManager { get; set; }  
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
    }
}
