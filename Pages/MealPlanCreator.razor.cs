using Health_expert_B.Data;
using Microsoft.AspNetCore.Components;
using Health_expert_B.Services;

namespace Health_expert_B.Pages
{
    public partial class MealPlanCreator: ComponentBase
    {
        private string errorMessage = string.Empty;
        private  UserMealPreference mealPreference = new UserMealPreference();
        List<string> SelectedFoodsPrefernce = new List<string>();
        List<string> SelectedSnacksPrefernce = new List<string>();
        List<string> SelectedMealPrefernce = new List<string>();
        string OtherFactors = string.Empty;
        [Inject]
        UserMealPrefrenceService UserMealPrefrenceService { get; set; }

        void UpdateSelectedFoodPreference (ChangeEventArgs e, string value)
        {
            if ((bool)e.Value)
            {
                SelectedFoodsPrefernce.Add(value);
            }
            else
            {
                SelectedFoodsPrefernce.Remove(value);
            }
        }
        void UpdateSelectedSnacksPreference(ChangeEventArgs e)
        {
            string value = e.Value.ToString();
            if (value != string.Empty)
            {
                SelectedSnacksPrefernce.Add(value);
            }
            else
            {
                SelectedSnacksPrefernce.Remove(value);
            }
        }
        void UpdateSelectedMealPreference(ChangeEventArgs e)
        {
            string value = e.Value.ToString();
            if (value != string.Empty)
            {
                SelectedMealPrefernce.Add(value);
            }
            else
            {
                SelectedMealPrefernce.Remove(value);
            }
        }
        void UpdateOtherFactors(ChangeEventArgs e)
        {
            OtherFactors = e.Value.ToString();
        }

        private void SubmitForm()
        {
            try
            {
                string selectedFoodPreference = string.Join(", ", SelectedFoodsPrefernce);
                string selectedSnacksPreference = string.Join (", ", SelectedSnacksPrefernce);
                string selectedMealPreference = string.Join (" ", SelectedMealPrefernce);
                mealPreference.FoodPreference = selectedFoodPreference;
                mealPreference.SnackAmount = selectedSnacksPreference;
                mealPreference.MealAmount = selectedMealPreference;
                mealPreference.OtherFactors = OtherFactors;

                UserMealPrefrenceService.UserMealPreference = mealPreference;


                NavManager.NavigateTo("/MealPlan");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
