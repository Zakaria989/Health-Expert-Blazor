using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.RegularExpressions;

namespace Health_expert_B.Data
{
    public class UserInfo
    {
        public string _fullName = string.Empty;
        public string _sex = "Male";
        public DateOnly? _birthDate = null;
        public double? _weight = null;
        public double? _height = null;
        public double? _bmi = 0.0;
        public int _age = 0;
        public string errorMessage = string.Empty;
        public bool UserInfoSaved { get; set; } = false;


        public string FullName
        {
            get { return _fullName; }
            set
            {
                if (!IsValidFullName(value))
                {
                    // _fullName = value;
                    errorMessage = "Type your name in the correct format: John Doe, John Adam Doe, or John";
                }
                else
                {
                    _fullName = value;
                    errorMessage = string.Empty;
                }
            }
        }

        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        public DateOnly? BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (!IsValidBirthDate(value))
                {
                    _birthDate = value;
                    errorMessage = "Invalid date, type your birth date in the correct format: dd.mm.yyyy";
                }
                else
                {
                    _birthDate = value;
                    errorMessage = string.Empty;
                    CalculateAge(_birthDate);

                }
            }
        }

        public double? Weight
        {
            get { return _weight; }
            set
            {
                if (!IsValidWeight(value))
                {
                    // _weight = value;
                    errorMessage = "Invalid weight, please type a number between 30 Kg and 200 Kg";
                }
                else
                {
                    _weight = value;
                    errorMessage = string.Empty;
                }
            }
        }

        public double? Height
        {
            get { return _height; }
            set
            {
                if (!IsValidHeight(value))
                {
                    // _height = value;
                    errorMessage = "Invalid height, please type a number between 130 cm and 272 cm";
                }
                else
                {
                    _height = value;
                    CalculateBmi(_height, _weight);
                    errorMessage = string.Empty;
                }
            }
        }

        public double? Bmi
        {
            get
            {
                return _bmi;
            }
            set
            {
                
            }
        }
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {

            }
        }

        private bool IsValidFullName(string fullName)
        {
            string namePattern = @"^[A-Za-z]+(?:\s[A-Za-z]+)*$";
            return Regex.IsMatch(fullName, namePattern);
        }

        private bool IsValidBirthDate(DateOnly? birthDate)
        {
            string datePattern = @"^(0[1-9]|1[0-9]|2[0-9]|3[01])\.(0[1-9]|1[012])\.\d{4}$";
            string dateSlashPattern = @"^(0[1-9]|1[012])/(0[1-9]|[12][0-9]|3[01])/\d{4}$";
            string dateDashPattern = @"^(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])-\d{4}$";
            string birthDateStr = birthDate.ToString();


            return Regex.IsMatch(birthDateStr, datePattern) ||
                    Regex.IsMatch(birthDateStr, dateSlashPattern) ||
                    Regex.IsMatch(birthDateStr, dateDashPattern);
        }

        private bool IsValidWeight(double? weight)
        {
            return weight >= 30.0 && weight <= 200.0;
        }

        private bool IsValidHeight(double? height)
        {
            return height >= 130.0 && height <= 272.0;
        }
        public int CalculateAge(DateOnly? birthDate)
        {
            int age = 0;
            age = DateTime.Today.Year - birthDate.Value.Year;
            return age;
        }
        public double? CalculateBmi(double? height, double? weight)
        {
            double? bmi = 0.0;
            if (height == 0.0 || weight == 0.0)
               {
                _bmi = 0.0;
                }
            bmi = (weight / (height * height)) * 10000;
            return bmi;
        }


    }
}
