namespace Health_expert_B.Models
{
    public class UserWorkoutPreference
    {
        public string _workoutDaysPreference = string.Empty;
        public string _workoutIntensityPreference = string.Empty;
        public string _workoutDurationPreference = string.Empty;
        public string _cardioPreference = string.Empty;
        public string _strengthPreference = string.Empty;
        public string _otherFactors = string.Empty;

        public string WorkoutDaysPreference
        {
            get { return _workoutDaysPreference; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _workoutDaysPreference = value;
                }
                else
                {
                    _workoutDaysPreference = "No workout days preference";
                }
            }
        }

        public string WorkoutIntensityPreference
        {
            get { return _workoutIntensityPreference; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _workoutIntensityPreference = value;
                }
                else
                {
                    _workoutIntensityPreference = "No workout intensity preference";
                }
            }
        }

        public string WorkoutDurationPreference
        {
            get { return _workoutDurationPreference; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _workoutDurationPreference = value;
                }
                else
                {
                    _workoutDurationPreference = "No workout duration preference";
                }
            }
        }

        public string CardioPreference
        {
            get { return _cardioPreference; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _cardioPreference = value;
                }
                else
                {
                    _cardioPreference = "No cardio preference";
                }
            }
        }

        public string StrengthPreference
        {
            get { return _strengthPreference; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _strengthPreference = value;
                }
                else
                {
                    _strengthPreference = "No strength preference";
                }
            }
        }

        public string OtherFactors
        {
            get { return _otherFactors; }
            set
            {
                if (!string.IsNullOrEmpty(value))
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
