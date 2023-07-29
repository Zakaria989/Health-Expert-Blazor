namespace Health_expert_B.Data
{
    public class UserMealPreference
    {
        public string _foodPreference = string.Empty;
        public string _snackAmount = string.Empty;
        public string _mealAmount = string.Empty;
        public string _otherFactors = string.Empty;



        public string FoodPreference 
        {
            get { return _foodPreference;}
            set
            {
                if (value != string.Empty)
                {
                    _foodPreference = value;
                }
                else
                {
                    _foodPreference = "No food preference";
                }
            }
        }

        public string SnackAmount 
        {
            get { return _snackAmount; }
            set
            {
                if (value != string.Empty)
                {
                    _snackAmount = value;
                }
                else
                {
                    _snackAmount = "No Snacks";
                }
            } 
        }

        public string MealAmount 
        {
            get { return _mealAmount; } 
            set
            {
                if (value != string.Empty)
                {
                    _mealAmount = value;
                }
                else
                {
                    _mealAmount = "Pick a random amount";
                }
            } 
        }
        public string OtherFactors
        {
            get { return _otherFactors; }
            set
            {
                if (value != string.Empty)
                {
                    _otherFactors = value;
                }
                else
                {
                    _otherFactors = "No other factors";
                }
            }
        }
    }
}