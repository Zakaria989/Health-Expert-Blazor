using Health_expert_B.Data;
using Health_expert_B.Models;
using Health_expert_B.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Health_expert_B.Pages
{
    public partial class WorkoutPlan: ComponentBase
    {
        private readonly List<Message> _prompt = new();
        private bool _isSendingMessage;
        public string WorkoutPlanByAssistant { get; set; }
        private ElementReference textInputRef;

        protected override Task OnInitializedAsync()
        {
            UserWorkoutPreference UserWorkout = UserPrefrence.UserWorkoutPreference;
            UserInfo userInfo = UserInfo.User;
            _prompt.Add(new Message { role = "user", content = Context.ContextFillerWorkoutPreference(userInfo,UserWorkout) });
            _isSendingMessage = true;
            ScrollToBottom();
            return base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            
            if (firstRender)
            {
                ScrollToBottom();
                await CreateCompletion();
            }
            ScrollToBottom();
        }
        private async void ScrollToBottom()
        {
            IJSObjectReference jsRef = await JSRuntime.InvokeAsync<IJSObjectReference>("import","./Pages/WorkoutPlan.razor.js");
            await jsRef.InvokeVoidAsync("focusElement", textInputRef);
            StateHasChanged();
        }
        private void NavigateToAnotherPage()
        {
            NavManager.NavigateTo("/WorkoutPlanner"); 
        }

        private async Task CreateCompletion()
        {
            var assistantResponse = await OpenAIService.CreateChatCompletion(_prompt);
            WorkoutPlanByAssistant = assistantResponse.content.ToString();
            _isSendingMessage = false;
            StateHasChanged();
        }


        [Inject]
        public OpenAIService OpenAIService { get; set; }
        [Inject]
        UserWorkoutPrefrenceService UserPrefrence { get; set; }
        [Inject]
        UserInfoService UserInfo { get; set; }
        [Inject]
        NavigationManager NavManager { get; set; }
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
    }
}
