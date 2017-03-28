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
    public abstract class Employee
    {
        protected string lowestGrade;
        protected int maxCredits;
        protected SortedDictionary<string, Course> courseList;

        public SortedDictionary<string, Course> GetCourses { get; }
        public string EmpId { get; set; }
        public int EmpType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string CityName { get; set; }
        public string State { get; set; }
        public string HireDate { get; set; }
        public string MaritalStatus { get; set; }
        public string JobTitle { get; set; }
        public string DepartmentName { get; set; }
        public bool HasCourses
        {
            get
            {
                if(courseList.Count != 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool HasOverTime { get; set; }
        public bool HasBenefits { get; set; }
        public bool IsCommision { get; set; }
        public bool HasEducationalBenefits { get; set; }
        /// <summary>
        /// default constructord
        /// </summary>
        public Employee()
        {
            MaritalStatus = default(string);
            JobTitle = default(string);
            DepartmentName = default(string);
            EmpId = default(string);
            EmpType = default(int);
            FirstName = default(string);
            LastName = default(string);
            Address = default(string);
            ZipCode = default(string);
            CityName = default(string);
            State = default(string);
            HireDate = default(string);
            HasOverTime = default(bool);
            HasBenefits = default(bool);
            IsCommision = default(bool);
            HasEducationalBenefits = default(bool);
            maxCredits = 0;
            lowestGrade = default(string);
            courseList = new SortedDictionary<string, Course>();
        }

        /// <summary>
        /// parameterized constructor
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="type"></param>
        /// <param name="_fname"></param>
        /// <param name="_lname"></param>
        public Employee(string _empId, int _type, string _fname, string _lname, string _address,
            string _city, string _state, string _zip, string _hireDate, string _marriageStatus, string _jobTitle, string _dept)
        {
            EmpId = _empId;
            EmpType = _type;
            FirstName = _fname;
            LastName = _lname;
            Address = _address;
            CityName = _city;
            State = _state;
            ZipCode = _zip;
            HireDate = _hireDate;
            HasOverTime = default(bool);
            HasBenefits = default(bool);
            IsCommision = default(bool);
            HasEducationalBenefits = default(bool);
            MaritalStatus = _marriageStatus;
            JobTitle = _jobTitle;
            DepartmentName = _dept;
            maxCredits = 0;
            lowestGrade = default(string);
            courseList = new SortedDictionary<string, Course>();
        }
        /// <summary>
        /// Determines How many course credits company will pay for
        /// </summary>
        /// <returns>int</returns>
        public int CreditsPaidFor() {
            int addedCredits = 0;
            
            if (HasEducationalBenefits)
            {
                string strGrade;
                int curGrade, lowIntGrade;
                foreach (var course in courseList)
                {
                    strGrade = course.Value.Grade;
                    curGrade = course.Value.ConvertGradeToNumber(strGrade);
                    lowIntGrade = course.Value.ConvertGradeToNumber(lowestGrade);
                    if (curGrade <= lowIntGrade)
                    {
                        addedCredits += course.Value.Credits;
                    }
                }
                if(addedCredits >= maxCredits)
                {
                    return maxCredits;
                }
            }
            return addedCredits;

        }
        /// <summary>
        /// Add a course to an employee
        /// </summary>
        /// <param name="_course"></param>
        public void AddCourse(Course _course)
        {
            courseList.Add(_course.CourseId, _course);
        }
        /// <summary>
        /// groups class information for output
        /// </summary>
        /// <returns>a string value</returns>
        public override string ToString()
        {
            string ID = "ID:".PadRight(20, '.') + EmpId + "\n";
            string name = "Name:".PadRight(20, '.') + $"{FirstName} {LastName}" + "\n";
            string type = "Type:".PadRight(20, '.') + $"{EmpType}" + "\n";
            string title = "Title:".PadRight(20, '.') + JobTitle + "\n";
            string dept = "Dept:".PadRight(20, '.') + DepartmentName + "\n";
            string maritalStatus = "Marital Status:".PadRight(20, '.') + MaritalStatus + "\n";
            string startDate = "Start Date:".PadRight(20, '.') + HireDate + "\n";
            string address = "Address:".PadRight(20, '.') + Address + "\n";
            string city = "City:".PadRight(20, '.') + CityName + "\n";
            string state = "State:".PadRight(20, '.') + State + "\n";
            string zip = "ZipCode:".PadRight(20, '.') + ZipCode + "\n";
            string display = ID + name + type + title + dept + maritalStatus + startDate + address + city + state + zip;
            return display;


            //string baseInfo = $"\nID:  \t{EmpId}\nType:  \t{EmpType}\nName:  \t{FirstName} {LastName}\nAddress:  \t{Address}\nCity:  \t{CityName}\nState:  \t{state}\nZip Code:   {ZipCode}\nHire Date:  {HireDate}\n";
            //return baseInfo;
        }
        /// <summary>
        /// Display if employe has benefits
        /// </summary>
        /// <returns>string</returns>
        public string DisplayBenefits() {
            string benefits = "General Benefits:".PadRight(15, '.') + $"{HasBenefits}\n";
            string education = "Edu. Benefits:".PadRight(15, '.') + $"{HasEducationalBenefits}\n";

            return (benefits + education);
        }
        /// <summary>
        /// Displays all employee courses
        /// </summary>
        /// <returns>string</returns>
        public string DisplayCourses()
        {
            string num, output = "";
            int count = 1;
            if (courseList.Count != 0)
            {
                foreach (var course in courseList)
                {
                    num = $"{count++}:\n";
                    output = output + num + course.Value.ToString();
                }
            }
            return output;
        }
    }
}
