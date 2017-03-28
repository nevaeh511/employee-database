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
using System.Linq;

namespace Employee_Database
{
    public class BusinessRules
    {
        private SortedDictionary<uint, Employee> employees;
        private static BusinessRules instance;
        private FileIO fileIO;
        private string existsMsg = "Employee already exists!";
        
        /// <summary>
        /// a property that allows access to retreive the the instatiated object
        /// </summary>
        public static BusinessRules Instance{
            get{
                if(instance == null){
                    instance = new BusinessRules();
                }
                return instance;
            }
        }
        /// <summary>
        /// Sets or gets an Employee object
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Employee object</returns>
        public Employee this[uint key]
        {
            get
            {
                //checks if key is actual key
                if (employees.ContainsKey(key))
                {
                    return employees[key];
                }
                else
                {
                    //check if key is an index
                    uint tempKey = employees.ElementAt((int)key).Key;
                    if (employees.ContainsKey(tempKey))
                    {
                        return employees[tempKey];
                    }
                    else { throw new Exception("Employee cannot be found."); }
                }
            }
            set
            {
                if (!employees.ContainsKey(key))
                {
                    employees[key] = value;
                }
                else
                {
                    throw new Exception("Employee already exists!");
                }
            }
        }

        /// <summary>
        /// returns the size of the sorted dictionary
        /// </summary>
        public int Size { get { return employees.Count(); } }

        /// <summary>
        /// gets a list of employee objects
        /// </summary>
        public SortedDictionary<uint, Employee> EmployeeList
        {
            get { return employees; }
            set { employees = value; }
        }
        /// <summary>
        /// Gets a FileIO object
        /// Creates FileIO object if null
        /// </summary>
        public FileIO GetFileIO
        {
            get
            {
                if(fileIO == null)
                {
                    return fileIO = new FileIO(employees);
                }
                return fileIO;
            }
        }
        /// <summary>
        /// constructor that gets the instance of an employee array
        /// </summary>
        private BusinessRules() {
            employees = new SortedDictionary<uint, Employee>();
        }
        /// <summary>
        /// method version to add an employee
        /// </summary>
        /// <param name="e"></param>
        public void AddEmployee(Employee e)
        {
            uint id = uint.Parse(e.EmpId);
            if (employees.ContainsKey(id))
            {
                throw new Exception(existsMsg);
            }
            else
            {
                employees.Add(id, e);
            }
            
        }
        /// <summary>
        /// Finds an all employees with given last name
        /// </summary>
        /// <param name="_lastName"></param>
        /// <returns>List of Employees</returns>
        public List<Employee> SearchByLastName(string _lastName)
        {
            List<Employee> lastNames = new List<Employee>();
            foreach (var employee in employees)
            {
                if(employee.Value.LastName == _lastName)
                {
                    lastNames.Add(employee.Value);
                }
            }
            return lastNames;
        }
        
        /// <summary>
        /// Takes input from the form and and creates an employee
        /// </summary>
        /// <param name="index"></param>
        /// <param name="_first"></param>
        /// <param name="_last"></param>
        /// <param name="_id"></param>
        /// <param name="_add"></param>
        /// <param name="_city"></param>
        /// <param name="_st"></param>
        /// <param name="_zip"></param>
        /// <param name="_date"></param>
        /// <param name="_input1"></param>
        /// <param name="_input2"></param>
        public void InstantiateEmployeeType(int index, string _first, string _last, string _id, string _add, string _city, 
            string _st, string _zip, string _date, string _marriageStatus, string _jobTitle, string _dept, string _input1, 
            string _input2)
        {
            Employee emp = default(Employee);
            string first = _first;
            string last = _last;
            string empId = _id;
            string address = _add;
            string city = _city;
            string state = _st;
            string zip = _zip;
            string hireDate = _date;
            string marriageStatus = _marriageStatus;
            string jobTitle = _jobTitle;
            string dept = _dept;
            double monthlySalary, contractWage, hourlyRate, hoursWorked, comm, gSales;
            int typeId;

            switch (index)
            {
                case (int)EType.CONTRACT:
                    contractWage = double.Parse(_input1);
                    typeId = (int)EType.CONTRACT;
                    emp = new Contract(empId, typeId, first, last, address, city, state, zip, hireDate, marriageStatus, jobTitle, dept, contractWage);
                    break;
                case (int)EType.HOURLY:
                    typeId = (int)EType.HOURLY;
                    hourlyRate = double.Parse(_input1);
                    hoursWorked = double.Parse(_input2);
                    emp = new Hourly(empId, typeId, first, last, address, city, state, zip, hireDate, marriageStatus, jobTitle, dept, hourlyRate, hoursWorked);
                    break;
                case (int)EType.SALES:
                    typeId = (int)EType.SALES;
                    comm = double.Parse(_input1);
                    gSales = double.Parse(_input2);
                    emp = new Sales(empId, typeId, first, last, address, city, state, zip, hireDate, marriageStatus, jobTitle, dept, comm, gSales);
                    break;
                case (int)EType.SALARY:
                    typeId = (int)EType.SALARY;
                    monthlySalary = double.Parse(_input1);
                    emp = new Salary(empId, typeId, first, last, address, city, state, zip, hireDate, marriageStatus, jobTitle, dept, monthlySalary);
                    break;
                default:
                    break;
            }

            try
            {
                AddEmployee(emp);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        /// <summary>
        /// Adds a course to employee in employee list
        /// </summary>
        /// <param name="_empId"></param>
        /// <param name="_school"></param>
        /// <param name="_courseid"></param>
        /// <param name="_courseName"></param>
        /// <param name="_credits"></param>
        /// <param name="_grade"></param>
        /// <param name="_date"></param>
        public void AddCourse(string _empId, string _school, string _courseid, string _courseName, string _credits, string _grade, string _date)
        {
            uint empKey = uint.Parse(_empId);
            int credits = int.Parse(_credits);
            if (employees.ContainsKey(empKey))
            {
                //Employee employee = employees[empKey];
                Course course = new Course(_school, _courseid, _courseName, credits, _grade, _date);
                employees[empKey].AddCourse(course);
            }
            else
            {
                throw new Exception("Employee Does Not Exist");
            }
        }
        /// <summary>
        /// Displays all employees
        /// </summary>
        /// <returns>string</returns>
        public string DisplayEmployees()
        {
            string output = "";
            if(employees.Count != 0)
            {
                foreach (var employee in employees)
                {
                    output = output + employee.Value.ToString() + "__________________________\n\n";
                }
            }
            else { output = "Database Is Empty"; }
            
            return output;
        }
        /// <summary>
        /// Finds employee with given id
        /// </summary>
        /// <param name="_id"></param>
        /// <returns>string</returns>
        public string QueryEmpID(string _id)
        {
            string output = "";
            var query = from d in employees
                        where d.Value.EmpId == _id
                        select d.Value.ToString();
            foreach (var item in query)
            {
                output = output + item;
            }
            return output;
        }
        /// <summary>
        /// Finds all employees with given last name
        /// </summary>
        /// <param name="_last"></param>
        /// <returns>string</returns>
        public string QueryEmpLastName(string _last)
        {
            string output = "";
            var query = from d in employees
                        where d.Value.LastName == _last
                        select d;
            foreach (var item in query)
            {
                output = output + item.Value.ToString() + "__________________________\n\n";
            }
            return output;
        }
        /// <summary>
        /// Finds all employees with last name with their benefits
        /// </summary>
        /// <param name="_last"></param>
        /// <returns>string</returns>
        public string QueryLastNamesWithBenefits(string _last)
        {
            string output = "";
            var query = from d in employees
                        where d.Value.LastName == _last
                        select d;
            foreach (var item in query)
            {
                output = output + item.Value.ToString() + item.Value.DisplayBenefits() + "__________________________\n\n"; ;
            }
            return output;
        }
        /// <summary>
        /// Finds all employees with last name and their benefits and courses
        /// </summary>
        /// <param name="_last"></param>
        /// <returns>string</returns>
        public string QueryLastNameBenefitsCourses(string _last)
        {
            string output = "";
            var query = from d in employees
                        where d.Value.LastName == _last
                        select d;
            foreach (var item in query)
            {
                output = output + item.Value.ToString() + item.Value.DisplayBenefits() + item.Value.DisplayCourses() + "__________________________\n\n";
            }
            return output;
        }
        /// <summary>
        /// Finds all employees with last name with their courses
        /// </summary>
        /// <param name="_last"></param>
        /// <returns>string</returns>
        public string QueryLastNameWithCourses(string _last)
        {
            string output = "";
            var query = from d in employees
                        where d.Value.LastName == _last
                        select d;
            foreach (var item in query)
            {
                output = output + item.Value.ToString() + item.Value.DisplayCourses() + "__________________________\n\n";
            }
            return output;
        }
        /// <summary>
        /// sorts employees by last name
        /// </summary>
        /// <returns>string</returns>
        public string QueryEmpSortedLastName()
        {
            string output = "";
            var query = from d in employees
                        orderby d.Value.LastName
                        select d;
            foreach (var item in query)
            {
                output = output + item.Value.ToString() + "__________________________\n\n";
            }
            return output;
        }
        /// <summary>
        /// Finds any employees with any benefits
        /// </summary>
        /// <returns>string</returns>
        public string QueryHasAnyBenefits()
        {
            string output = "";
            var query = from d in employees
                        where d.Value.HasBenefits == true && d.Value.HasEducationalBenefits == true
                        select d;
            foreach (var item in query)
            {
                string id = "ID:".PadRight(15, '.') + item.Value.EmpId + "\n";
                string name = "Name:".PadRight(15, '.') + $"{item.Value.FirstName} {item.Value.LastName}\n";
                string bene = "Benefits:".PadRight(15, '.') + $"{item.Value.HasBenefits}\n";
                string eduBen = "Edu. Benefits:".PadRight(15, '.') + $"{item.Value.HasEducationalBenefits}\n";
                output = output + id + name + bene + eduBen + "__________________________\n\n";
            }
            return output;
        }
        /// <summary>
        /// Sorts employees by first name
        /// </summary>
        /// <returns>string</returns>
        public string QueryEmpSortFirstName()
        {
            string output = "";
            var query = from d in employees
                        orderby d.Value.FirstName
                        select d;
            foreach (var item in query)
            {
                output = output + item.Value.ToString() + "__________________________\n\n";
            }
            return output;
        }
        /// <summary>
        /// Finds employees with courses
        /// </summary>
        /// <returns>string</returns>
        public string QueryEmpHasCourses()
        {
            string output = "No Courses";
            var query = from d in employees
                        where d.Value.HasCourses == true
                        select d;
            foreach (var item in query)
            {
                string id = "ID:".PadRight(15, '.') + item.Value.EmpId + "\n";
                string name = "Name:".PadRight(15, '.') + $"{item.Value.FirstName} {item.Value.LastName}\n";
                output = output + id + name + item.Value.DisplayCourses() + "__________________________\n\n";
            }
            return output;
        }
        /// <summary>
        /// Finds all employees who are married
        /// </summary>
        /// <returns>string</returns>
        public string QueryEmpWhoAreMarried()
        {
            string output = "";
            var query = from d in employees
                        where d.Value.MaritalStatus.ToUpper() == "MARRIED"
                        select d;
            foreach (var item in query)
            {
                string id = "ID:".PadRight(15, '.') + item.Value.EmpId + "\n";
                string name = "Name:".PadRight(15, '.') + $"{item.Value.FirstName} {item.Value.LastName}\n";
                string status = "Marital Status:".PadRight(15, '.') + item.Value.MaritalStatus + "\n";
                output = output + id + name + status + "__________________________\n\n";
            }
            return output;
        }
        /// <summary>
        /// Finds all employees who are single
        /// </summary>
        /// <returns>string</returns>
        public string QueryEmpWhoAreSingle()
        {
            string output = "";
            var query = from d in employees
                        where d.Value.MaritalStatus.ToUpper() == "SINGLE"
                        select d;
            foreach (var item in query)
            {
                string id = "ID:".PadRight(15, '.') + item.Value.EmpId + "\n";
                string name = "Name:".PadRight(15, '.') + $"{item.Value.FirstName} {item.Value.LastName}\n";
                string status = "Marital Status:".PadRight(15, '.') + item.Value.MaritalStatus + "\n";
                output = output + id + name + status + "__________________________\n\n";
            }
            return output;
        }

    }//end Class
}//end namespace
