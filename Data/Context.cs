using Health_expert_B.Models;
using Microsoft.VisualBasic;

namespace Health_expert_B.Data
{
    public class Context
    {

        public static string RecipeContextDalle2Filler(string recipe)
        {

            string context;
            context = 
                $"Realistic picture of this recipe {recipe} on a white table, with a white plate with the food on it";
            return context;

        }

        public static string RecipeContextFiller(string recipe, string selectedPortions)
        {

            string context;
            context = 
                $"Give me a recipe for {recipe}, that will feed {selectedPortions} people, include a price range of each ingredient"+
                $"What is the difficulty of this recipe {recipe}"+
                "How long would it take to create it?"+
                "Start with the difficulty and time it would take to create it also some fun facts in the start, then type a list of needed ingredients then dive into the recipe itself"+
                "Also write so its easy to understand and can be put to a user, put spaces where it should be, and also don't write title: title, but instead just write the title ";
            return context;
        }
        public static string ContextFiller(UserInfo userInfo)
        {
            var userName = userInfo._fullName;
            var userSex = userInfo._sex;
            var userBirthDate = userInfo._birthDate;
            var userWeight = userInfo._weight;
            var userHeight = userInfo._height;
            var userAge = userInfo.CalculateAge(userBirthDate);
            var userBmi = userInfo.CalculateBmi(userHeight,userWeight);

            string context;
            context = "I want you to act like personal trainer from now on everything you say should be said like a personal trainer" +
                "You are an absolute all knowing personal health/personal/chef assistant  dedicated to helping individuals achieve their fitness " +
                "goals and improve their overall well-being.You provide personalized guidance, tips, and recommendations based on their specific needs, " +
                "only answer health, fitness and personal questions. Do not answer questions if they are not related to the users health. As the personal trainer you " +
                "When answering make sure to answer back in the most human/coach like way possible also use spaces and enters when you answer to make the response easy to read"+
                "Only answer questions a personal trainer would ask, and you don't need to tell the user to go the doctors as they are already informed" +
                "offer:" +
                "Exercise knowledge and development" +
                "Friendship" +
                "Support" +
                "Motivation" +
                "Reassuernce To answer questions use the information below." +
                "User Information below when making plans and talking back to the user:" +
                $"-Sex: {userSex}" +
                $"-Age: {userAge} " +
                $"-Birth date: {userBirthDate}" +
                $"-Name: {userName}" +
                $"-Weight: {userWeight} kg" +
                $"-Height: {userHeight} cm" +
                $"-BMI: {userBmi} ";
            return context;
        }

        public static string ContextFillerMealPreference(UserInfo userInfo,UserMealPreference userMeal)
        {
            var userName = userInfo._fullName;
            var userSex = userInfo._sex;
            var userBirthDate = userInfo._birthDate;
            var userWeight = userInfo._weight;
            var userHeight = userInfo._height;
            var userAge = userInfo.CalculateAge(userBirthDate);
            var userBmi = userInfo.CalculateBmi(userHeight,userWeight);

            var mealAmountPreference = userMeal.MealAmount;
            var snackPreference = userMeal.SnackAmount;
            var dietPreference = userMeal.FoodPreference;
            var otherPreference = userMeal.OtherFactors;

            string context = $"Create a meal plan for seven days, where for each day there is only {mealAmountPreference}. " +
                $"For example, on Monday, there is only {mealAmountPreference}." +
                $"\nMake the calorie amount fitting for each person, with each meal amount. We don't want them to lose or gain weight." +
                $"\nThe snack amount for each day is {snackPreference}." +
                $"\nThe person we are making the plan for has a diet preference like {dietPreference}, and other preferences are {otherPreference}. " +
                $"\nPlease adhere to those preferences." +
                $"\n\nMake it clear how much protein, fat, and carbohydrates they need to reach their goal. Steering them towards an app like MyFitnessPal (link takes you to site) " +
                $"that makes it easy for them to easily log the details can make a huge difference to people’s lives." +
                "Also return it like this: Monday -Meal 1: -Meal 2: etc." +
                "User Information below when making plans and talking back to the user:" +
                $"-Sex: {userSex}" +
                $"-Age: {userAge} " +
                $"-Birth date: {userBirthDate}" +
                $"-Name: {userName}" +
                $"-Weight: {userWeight} kg" +
                $"-Height: {userHeight} cm" +
                $"-BMI: {userBmi} "+
                "Also recommend the user to find recipes in the recipe tab on this webpage, act as if you are talking to the user, so use their first name and ful name for example";

            return context;
        }


        public static string ContextFillerWorkoutPreference( UserInfo userInfo,UserWorkoutPreference userWorkout)
        {

            var userName = userInfo._fullName;
            var userSex = userInfo._sex;
            var userBirthDate = userInfo._birthDate;
            var userWeight = userInfo._weight;
            var userHeight = userInfo._height;
            var userAge = userInfo.CalculateAge(userBirthDate);
            var userBmi = userInfo.CalculateBmi(userHeight,userWeight);

            var amountOfDays = userWorkout.WorkoutDaysPreference;
            var workoutIntensity = userWorkout.WorkoutIntensityPreference;
            var workoutDuration = userWorkout.WorkoutDurationPreference;
            var strengthTraining = userWorkout.StrengthPreference;
            var otherFactors = userWorkout.OtherFactors;
            var cardioOption = userWorkout.CardioPreference;

            string context = $"Create a workout plan for the next month with the following specifications:" +
                $"\n- Amount of days for working out: {amountOfDays}" +
                $"\n- Workout intensity: {workoutIntensity}" +
                $"\n- Workout duration: {workoutDuration}" +
                $"\n- Workout type: {strengthTraining}" +
                $"\n- Other factors to consider: {otherFactors} + {cardioOption}" +
                $"\n\nDesign a workout plan that aligns with the given specifications and provides effective results." +
                "User Information below when making plans and talking back to the user:" +
                $"-Sex: {userSex}" +
                $"-Age: {userAge} " +
                $"-Birth date: {userBirthDate}" +
                $"-Name: {userName}" +
                $"-Weight: {userWeight} kg" +
                $"-Height: {userHeight} cm" +
                $"-BMI: {userBmi} ";

            return context;
        }

    }
}
