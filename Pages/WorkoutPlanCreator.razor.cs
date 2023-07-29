using Health_expert_B.Data;
using Health_expert_B.Models;
using Health_expert_B.Services;
using Microsoft.AspNetCore.Components;

namespace Health_expert_B.Pages
{
    public partial class WorkoutPlanCreator: ComponentBase
    {

        private string errorMessage = string.Empty;
        private UserWorkoutPreference userWorkoutPreference = new UserWorkoutPreference();
        List<string> SelectedWorkoutDaysPreference = new List<string>();
        List<string> SelectedWorkoutIntensityPreference = new List<string>();
        List<string> SelectedWorkoutDurationPreference = new List<string>();
        List<string> SelectedCardioPreference = new List<string>();
        List<string> SelectedStrengthPreference = new List<string>();
        string OtherFactors = string.Empty;
        [Inject]
        UserWorkoutPrefrenceService UserWorkoutPrefrenceService { get;set; }

        void UpdateSelectedWorkoutDaysPreference(ChangeEventArgs e)
        { 
            string value = e.Value.ToString();
            if (value != string.Empty)
            {
                SelectedWorkoutDaysPreference.Add(value);
            }
            else
            {
                SelectedWorkoutDaysPreference.Remove(value);
            }
        }
        void UpdateSelectedWorkoutIntensityPreference(ChangeEventArgs e)
        {
            string value = e.Value.ToString();
            if (value!= string.Empty)
            {
                SelectedWorkoutIntensityPreference.Add(value);
            }
            else
            {
                SelectedWorkoutIntensityPreference.Remove(value);
            }
        }
        void UpdateSelectedWorkoutDurationPreference(ChangeEventArgs e)
        {
            string value = e.Value.ToString();
            if (value!=string.Empty)
            {
                SelectedCardioPreference.Add(value);
            }
            else
            {
                SelectedCardioPreference.Remove(value);
            }
        }
        void UpdateSelectedStrengthPreference(ChangeEventArgs e) 
        {
            string value = e.Value.ToString();
            if (value != string.Empty)
            {
                SelectedStrengthPreference.Add(value);
            }
            else
            {
                SelectedStrengthPreference.Remove(value);
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
                string selectedWorkoutDaysPreference = string.Join(", ", SelectedWorkoutDaysPreference);
                string selectedWorkoutIntensityPreference = string.Join(", ", SelectedWorkoutIntensityPreference);
                string selectedWorkoutDurationlPreference = string.Join(" ", SelectedWorkoutDurationPreference);
                string selectedCardioPreference = string.Join(", ", SelectedCardioPreference);
                string selectedStrengthlPreference = string.Join(" ", SelectedStrengthPreference);


                userWorkoutPreference.WorkoutDaysPreference = selectedWorkoutDaysPreference;
                userWorkoutPreference.WorkoutIntensityPreference = selectedWorkoutIntensityPreference;
                userWorkoutPreference.WorkoutDurationPreference = selectedWorkoutDurationlPreference;
                userWorkoutPreference.CardioPreference = selectedCardioPreference;
                userWorkoutPreference.StrengthPreference = selectedStrengthlPreference;
                userWorkoutPreference.OtherFactors = OtherFactors;

                UserWorkoutPrefrenceService.UserWorkoutPreference = userWorkoutPreference;
                NavManager.NavigateTo("/WorkoutPlan");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
