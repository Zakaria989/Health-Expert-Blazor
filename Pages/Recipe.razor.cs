using Health_expert_B.Data;
using Health_expert_B.Models;
using Health_expert_B.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;

namespace Health_expert_B.Pages
{
    public partial class Recipe : ComponentBase
    {     
        private readonly List<Message> _prompt = new();
        private bool _isSendingMessage;       
        [Parameter]
        public string? SelectedRecipe { get; set; }
        [Parameter]
        public string? SelectedPortions { get; set; }

        public string RecipeByAssistant {get; set; }
        private ElementReference textInputRef;
        public string ImageUrl { get; set; }

        protected override Task OnInitializedAsync()
        {
            _prompt.Add(new Message { role = "user", content = Context.RecipeContextFiller(SelectedRecipe,SelectedPortions) });
            _isSendingMessage = true;
            ScrollToBottom();
            return base.OnInitializedAsync();
        }        
        protected override void OnInitialized()
        {
            var uri = NavManager.ToAbsoluteUri(NavManager.Uri);
            var uriParams = QueryHelpers.ParseQuery(uri.Query);
            SelectedRecipe = uriParams.TryGetValue("selectedRecipe", out var selectedRecipe) ? selectedRecipe.FirstOrDefault() : "No recipe";
            SelectedPortions = uriParams.TryGetValue("SelectedPortions", out var selectedPortions) ? selectedPortions.FirstOrDefault() : "";

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)  
        {
            if (firstRender)
            {
                await CreateCompletion();
            }
        }
        private async void ScrollToBottom()
        {
            IJSObjectReference jsRef = await JSRuntime.InvokeAsync<IJSObjectReference>("import","./Pages/Recipe.razor.js");
            await jsRef.InvokeVoidAsync("focusElement", textInputRef);
            StateHasChanged();
        }
        private async Task CreateCompletion()
        {            
            // var picture = await OpenAIService.Image
            var assistantResponse = await OpenAIService.CreateChatCompletion(_prompt);
            RecipeByAssistant = assistantResponse.content.ToString();
            _isSendingMessage = false;

            string dallePrompt = Context.RecipeContextDalle2Filler(SelectedRecipe);
            ImageUrl = await OpenAIService.CreateImageFromPrompt(dallePrompt);
            StateHasChanged();
        }
        private void NavigateToAnotherPage()
        {
            NavManager.NavigateTo("/RecipeCreator");
        }



        [Inject]
        public OpenAIService OpenAIService {get;set;}
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

    }
}