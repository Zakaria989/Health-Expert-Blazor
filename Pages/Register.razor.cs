using Health_expert_B.Data;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using Health_expert_B.Services;

namespace Health_expert_B.Pages
{
    public partial class Register: ComponentBase
    {
        private string errorMessage = string.Empty;
        private UserInfo userInfo = new UserInfo();
        [Inject]
        public UserInfoService UserInfoService { get; set; }

        private void SubmitForm()
        {
            try
            {
                if (IsValidForm())
                {
                    userInfo.UserInfoSaved = true;
                    UserInfoService.User = userInfo;
                    NavManager.NavigateTo("/ChatBot");
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        private bool IsValidForm()
        {   
            try
            {
                if (userInfo.errorMessage == string.Empty)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
