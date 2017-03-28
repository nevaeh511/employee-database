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
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Employee_Database
{
    public class Validate
    {

        public Validate(){ }

        /// <summary>
        /// Validates a last name input text box
        /// </summary>
        /// <param name="_lastname"></param>
        /// <returns>bool</returns>
        public bool ValidLastNameFormat(string _lastname)
        {
            Match match = Regex.Match(_lastname, @"\w+");
            if (match.Success) { return true; }
            else { return false; }
        }
        /// <summary>
        /// Validates employee id format
        /// </summary>
        /// <param name="_id"></param>
        /// <returns>bool</returns>
        public bool ValidIDFormat(string _id)
        {
            Match match = Regex.Match(_id, @"[0-9]{6}");
            if (match.Success) { return true; }
            else { return false; }
        }
        /// <summary>
        /// Validates Course data input
        /// </summary>
        /// <param name="controls"></param>
        /// <returns>List of Controls</returns>
        public List<Control> validateCourseData(Control[] controls)
        {
            List<Control> errors = new List<Control>();
            //"(0[12]|1[012])\\/(0[1-9]|[12][0-9]|3[01])\\/(19|20)\\d\\d" 
            //string[] input = { schoolName, _courseId, _courseName };
            string[] correctFormat = { "\\b\\d{6}\\b", "(Utah Valley University|UVU)", "\\b([A-Za-z]{2,})\\d{3,4}\\b", "\\b\\w{3,}\\b" };
            int index = 0;
            foreach (var item in controls)
            {
                Match match = Regex.Match(item.Text, correctFormat[index++]);
                if (!match.Success || item.Text == "")
                {
                    errors.Add(item);
                }
            }
            return errors;
        }
    }
}
