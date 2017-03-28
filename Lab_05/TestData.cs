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
    /// <summary>
    /// Test data for testing purposes
    /// </summary>
    public class TestData
    {
        public string[] address = { "511 S 190 W", "524 W 456 S", "852 N 951 W", "753 E 154 W", "45 Denver ST", "245 East 500 South" };
        public string[] city = { "Santaquin", "Westville", "Northville", "Provo", "Ogden", "Springville" };
        public string[] fName = { "Bob", "Ruth", "Jared", "Bill", "Ramsey", "Aaron" };
        public string[] lName = { "Miner", "Westington", "Robertson", "Harry", "Butler", "Harry" };
        public string[] state = { $"{UStates.AZ}", $"{UStates.DE}", $"{UStates.CA}", $"{UStates.MI}", $"{UStates.NH}", $"{UStates.CO}" };
        public string[] zip = { "84658", "75489", "12546", "35487", "78549", "84566" };
        public string[] hireDate = { "01/15/2015", "05/30/2001", "12/25/1980", "09/23/1999", "12/05/2010", "02/12/2000" };
        public string[] rate = { "15.25", "20.45", "16.75", "19.50", "25.40", "45.50" };
        public string[] commission = { "0.05", "0.15", "0.08", "0.01", "0.2", "0.5" };
        public string[] monthlySalary = { "50000", "60000", "70500", "105000", "500125", "100200" };
        public string[] hrsWorked = { "38", "40", "25", "39", "46", "80" };
        public string[] grossSales = { "6152.50", "5050.12", "10500.50", "4500.23", "450500", "45000" };
        public string[] empId = { "000000", "000001", "000002", "000003", "000004", "000005" };
        public int[] marriageStatus = { 1, 0, 2, 1, 0, 1 };
        public string[] jobtitle = { "Programmer", "Drafter", "Accountant", "Mail Carrier", "Engineer", "Software Developer" };
        public string[] department = { "Development", "Engineering", "Accounting", "Mail", "Engineering", "Development" };

        public string[] courseId = { "CS1400", "cs1410", "CS2420", "cs2250", "CS3260", "INFO2400" };
        public string[] courseName = { "Fundamentals of Programming", "Object Oriented Programming", "Data Structures", "Web Programming", "C# Software Development", "Internet Security" };
        public int[] grades = { 0, 1, 2, 3, 4, 5 };
        public int[] credits = { 0, 1, 2, 3, 4, 5 };
        public bool[] approved = { true, false, true, true, false, true };
        public string[] approvedDate = { "03/25/2001", "02/15/2015", "01/26/2015", "12/30/2011", "08/21/2005", "07/23/2006" };

        /// <summary>
        /// test data constructor nothing to construct
        /// </summary>
        public TestData()
        {
        }

    }
}
