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
    public sealed class Contract : Employee
    {

        public double MonthlyPay { get; set; }

        /// <summary>
        /// creates a default object for contract
        /// </summary>
        public Contract()
        {
            MonthlyPay = default(double);
        }

        /// <summary>
        /// parameterized constructor inheriting from base Employee
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="type"></param>
        /// <param name="_fname"></param>
        /// <param name="_lname"></param>
        /// <param name="_cW"></param>
        public Contract(string empId, int type, string _fname, string _lname, string _address,
            string _city, string _state, string _zip, string _hireDate, string _marriageStatus, string _jobTitle, string _dept, double _monthlyPay) : base(empId, type, _fname, _lname, _address, _city, _state, _zip, _hireDate, _marriageStatus, _jobTitle, _dept)
        {
            MonthlyPay = _monthlyPay;
        }

        /// <summary>
        /// groups class information for output
        /// </summary>
        /// <returns>a string value</returns>
        public override string ToString()
        {
            string thisInfo = "Rate:".PadRight(20, '.') + $"{MonthlyPay:C}\n";
            return base.ToString() + thisInfo;
        }

    }
}
