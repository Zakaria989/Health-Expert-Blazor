using Microsoft.AspNetCore.Components;

namespace Health_expert_B.Pages
{


    public partial class RecipeCreator : ComponentBase
    {
        [Parameter]
        public string? SelectedRecipe { get; set; }
        [Parameter]
        public string? SelectedPortions { get; set; }
        public string? errorMessage { get; set; }

        private void SubmitForm()
        {
            try
            {
                string queryString = $"?SelectedRecipe={SelectedRecipe}&selectedPortions={SelectedPortions}";
                NavManager.NavigateTo("/Recipe" + queryString);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }




    }
}