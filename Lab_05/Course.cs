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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Employee_Database
{
    [Serializable]
    public class Course
    {
        private string school;
        private string courseId;
        private string courseName;
        private int credits;
        private string grade;
        private string approvedDate;
        
        /// <summary>
        /// Gets if course is approved
        /// </summary>
        public bool IsCourseApproved
        {
            get
            {
                if (approvedDate == "") { return false; }
                else { return true; }
            }
        }
        /// <summary>
        /// Gets or sets the school name
        /// </summary>
        public string SchoolName
        {
            get { return school; }
            set { school = value; }
        }
        /// <summary>
        /// Get or sets approved date of course
        /// </summary>
        public string ApprovedDate
        {
            get { return approvedDate; }
            set { approvedDate = value; }
        }
        /// <summary>
        /// Gets or sets
        /// </summary>
        public string CourseId
        {
            get { return courseId; }
            set { courseId = value; }
        }
        /// <summary>
        /// Gets or sets Course name
        /// </summary>
        public string CourseName
        {
            get { return courseName; }
            set { courseName = value; }
        }
        /// <summary>
        /// Gets or set course credits
        /// </summary>
        public int Credits
        {
            get { return credits; }
            set { credits = value; }
        }
        /// <summary>
        /// Gets or set course grade
        /// </summary>
        public string Grade
        {
            get { return grade; }
            set { grade = value; }
        }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Course()
        {
            school = default(string);
            courseId = default(string);
            courseName = default(string);
            credits = default(int);
            grade = default(string);
            approvedDate = default(string);
        }
        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="_school"></param>
        /// <param name="_id"></param>
        /// <param name="_name"></param>
        /// <param name="_credits"></param>
        /// <param name="_grade"></param>
        /// <param name="_date"></param>
        public Course(string _school, string _id, string _name, int _credits, string _grade, string _date)
        {
            school = _school;
            courseId = _id;
            courseName = _name;
            credits = _credits;
            grade = _grade;
            approvedDate = _date;
        }
        /// <summary>
        /// Converts a grade string value to a integer value
        /// </summary>
        /// <param name="strGrade"></param>
        /// <returns>int</returns>
        public int ConvertGradeToNumber(string strGrade)
        {
            int intGrade = -1;
            switch (strGrade)
            {
                case "A":
                    intGrade = 0;
                    break;
                case "A-":
                    intGrade = 1;
                    break;
                case "B+":
                    intGrade = 2;
                    break;
                case "B":
                    intGrade = 3;
                    break;
                case "B-":
                    intGrade = 4;
                    break;
                case "C+":
                    intGrade = 5;
                    break;
                case "C":
                    intGrade = 6;
                    break;
                case "C-":
                    intGrade = 7;
                    break;
                case "D+":
                    intGrade = 8;
                    break;
                case "D":
                    intGrade = 9;
                    break;
                case "D-":
                    intGrade = 10;
                    break;
                case "E":
                    intGrade = 11;
                    break;
                default:
                    break;
            }
            return intGrade;
        }
        /// <summary>
        /// Constructs a string of Course objects relevant information
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string ID, name, credits, grade, approvedCourse, date, output;

            ID = ("ID:".PadRight(20, '.') + CourseId).PadLeft(10) + "\n";
            name = ("Course Name:".PadRight(20, '.') + CourseName).PadLeft(20) + "\n";
            credits = ("Credits:".PadRight(20, '.') + $"{Credits}").PadLeft(20) + "\n";
            grade = ("Grade:".PadRight(20, '.') + Grade).PadLeft(20) + "\n";
            approvedCourse = ("Approved:".PadRight(20, '.') + $"{IsCourseApproved}").PadLeft(20) + "\n";
            date = ("Approved Date:".PadRight(20, '.') + ApprovedDate).PadLeft(20) + "\n\n";
            output = ID + name + credits + grade + approvedCourse + date;
            return output;
        }

    }//end course class
}//end namespace
