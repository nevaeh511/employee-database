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
    public sealed class Sales : Employee
    {
        private const int MAX_CREDITS = 3;
        private const string LOWEST_GRADE = "C+";
        public double Commission { get; set; }
        public double GrossSales { get; set; }
        public double MonthlyPay { get; set; }
        

        /// <summary>
        /// default constructor
        /// </summary>
        public Sales()
        {
            Commission = default(double);
            GrossSales = default(double);
            MonthlyPay = default(double);
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
        /// <param name="_comm"></param>
        /// <param name="_gSales"></param>
        public Sales(string empId, int type, string _fname,string _lname,
            string _address, string _city, string _state, string _zip, string _hireDate,
            string _marriageStatus, string _jobTitle, string _dept,
            double _comm, double _gSales) : base(empId, type, _fname, _lname, _address, _city, _state, _zip, _hireDate,
                _marriageStatus, _jobTitle, _dept)
        {
            Commission = _comm;
            GrossSales = _gSales;
            IsCommision = true;
            HasBenefits = true;
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
            string thisInfo = "Commission:".PadRight(20, '.') + $"{Commission:P}\n" + "Gross Sales:".PadRight(20, '.') + $"{ GrossSales:C}\n";
            return base.ToString() + thisInfo;
        }
    }
}
