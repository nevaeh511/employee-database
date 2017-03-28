// Project Prolog
// Name: Aaron Merrill
// CS 3260 Section 001
// Project: Lab08
// Date: 03/25/2016
// 
// I declare that the following code was written by me or provided 
// by the instructor for this project. I understand that copying source
// code from any other source constitutes cheating, and that I will receive
// a zero on this project if I am found in violation of this policy.
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Database
{
    [Serializable]
    public sealed class Hourly : Employee
    {
        private const int MAX_CREDITS = 1;
        private const string LOWEST_GRADE = "B";
        public double HourlyRate { get; set; }
        public double HoursWorked { get; set; }

        //public bool OverTime { get; set; }
        //public bool EduBenefits { get; set; }

        /// <summary>
        /// default constructor initializing properties to default values
        /// </summary>
        public Hourly()
        {
            HoursWorked = default(double);
            HourlyRate = default(double);
            maxCredits = MAX_CREDITS;
            lowestGrade = LOWEST_GRADE;
        }

        /// <summary>
        /// parameterized constructor that inherits from base Employee
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="type"></param>
        /// <param name="_fname"></param>
        /// <param name="_lname"></param>
        /// <param name="_hR"></param>
        /// <param name="_hW"></param>
        public Hourly(string _empId, int _type, string _fname, string _lname,
            string _address, string _city, string _state, string _zip, string _hireDate, string _marriageStatus, string _jobTitle, string _dept,
            double _hR, double _hW) : base(_empId, _type, _fname, _lname, _address, _city, _state, _zip, _hireDate, _marriageStatus, _jobTitle, _dept)
        {
            HourlyRate = _hR;
            HoursWorked = _hW;
            HasOverTime = true;
            HasEducationalBenefits = true;
            maxCredits = MAX_CREDITS;
            lowestGrade = LOWEST_GRADE;
        }

        /// <summary>
        /// groups class information for output
        /// </summary>
        /// <returns>a string value</returns>
        public override string ToString()
        {
            string thisInfo = "Rate:".PadRight(20, '.') + $"{HourlyRate:C}\n" + "Hours:".PadRight(20, '.') + $"{HoursWorked}\n";
            return base.ToString() + thisInfo;
        }
    }
}
