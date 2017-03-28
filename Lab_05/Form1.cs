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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;

//think about creating an Interface for a payment method passing in 
//then the interface can take care
namespace Employee_Database
{
    /// <summary>
    /// Window form that gets created
    /// </summary>
    public partial class FrmEmpPanel : Form
    {

        /// <summary>
        /// declare and set employee reference to default values
        /// </summary>
        string requiredMsg = "Field Required";
        string invalidField = "Field Invalid";
        Validate valid;
        TestData testData;
        BusinessRules businessRules;
        int testIndex = 0;

        /// <summary>
        /// initializes and sets data for the form
        /// </summary>
        public FrmEmpPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// loads information to populate dropdown combo boxes
        /// </summary>
        private void FrmEmpPanel_Load(object sender, EventArgs e)
        {
            valid = new Validate();
            testData = new TestData();
            businessRules = BusinessRules.Instance;
            //eList = default(SortedDictionary<uint, Employee>);
            CBxEmpType.Items.AddRange(Enum.GetNames(typeof(EType)));
            CBxState.Items.AddRange(Enum.GetNames(typeof(UStates)));
        }
        
        /// <summary>
        /// handles the combo box whenever it is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CBxEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtPayAmount.Clear();
            TxtSecondInput.Clear();
            ActivatePayInput(CBxEmpType.SelectedIndex);
        }
        /// <summary>
        /// auto loads hard coded employee data for quick insertion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTestData_Click(object sender, EventArgs e)
        {
            LblAfterSubmitMsg.Text = "";
            ClearNewEmpForm();
            int count = testData.fName.Count();
            if (testIndex < count)
            {
                TxtID.Text = testData.empId[testIndex];
                TxtFirstName.Text = testData.fName[testIndex];
                TxtLastName.Text = testData.lName[testIndex];
                TxtAddress.Text = testData.address[testIndex];
                TxtCity.Text = testData.city[testIndex];
                TxtZip.Text = testData.zip[testIndex];
                CBxState.SelectedItem = testData.state[testIndex];
                TxtHireDate.Text = testData.hireDate[testIndex];
                TxtJobTitle.Text = testData.jobtitle[testIndex];
                TxtDept.Text = testData.department[testIndex];
                CBxMarriageStatus.SelectedIndex = testData.marriageStatus[testIndex];
            }
            else
            {
                testIndex = 0;
            }
        }


        /// <summary>
        /// based on what employee type is picked, this method makes visible or hides
        /// textboxes to input more information
        /// </summary>
        /// <param name="_empType"></param>
        private void ActivatePayInput(int _empType)
        {
            if (_empType == (int)EType.SALES || _empType == (int)EType.HOURLY)
            {
                if (_empType == (int)EType.HOURLY)
                {
                    LblPayAmount.Text = "Hourly Rate:";
                    LblSecondInput.Text = "Hours Worked:";
                    TxtPayAmount.Text = testData.rate[testIndex];
                    TxtSecondInput.Text = testData.hrsWorked[testIndex];
                }
                else
                {
                    LblPayAmount.Text = "Commission:";
                    LblSecondInput.Text = "Gross Sales:";
                    TxtPayAmount.Text = testData.commission[testIndex];
                    TxtSecondInput.Text = testData.grossSales[testIndex];
                }
                LblSecondInput.Visible = true;
                TxtSecondInput.Visible = true;
                LblPayAmount.Visible = true;
                TxtPayAmount.Visible = true;
            }
            else if (_empType == (int)EType.SALARY || _empType == (int)EType.CONTRACT)
            {
                if (_empType == (int)EType.SALARY)
                {
                    LblPayAmount.Text = "Salary:";
                    TxtPayAmount.Text = testData.monthlySalary[testIndex];
                }
                else
                {
                    LblPayAmount.Text = "Contract Rate:";
                    TxtPayAmount.Text = testData.rate[testIndex];
                }
                LblSecondInput.Visible = false;
                TxtSecondInput.Visible = false;
                LblPayAmount.Visible = true;
                TxtPayAmount.Visible = true;
            }
            else
            {
                LblPayAmount.Visible = false;
                TxtPayAmount.Visible = false;
                LblSecondInput.Visible = false;
                TxtSecondInput.Visible = false;
            }
        }

        /// <summary>
        /// clears all text boxes upon click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearNewEmpForm();
        }

        /// <summary>
        /// actually clears each textbox in the form
        /// </summary>
        private void ClearNewEmpForm()
        {
            EpForm.Clear();
            TxtID.Clear();
            TxtFirstName.Clear();
            TxtLastName.Clear();
            TxtAddress.Clear();
            TxtCity.Clear();
            CBxState.SelectedIndex = -1;
            CBxEmpType.SelectedIndex = -1;
            TxtZip.Clear();
            TxtHireDate.Clear();
            CBxMarriageStatus.SelectedIndex = -1;
            TxtDept.Clear();
            TxtJobTitle.Clear();
            LblAfterSubmitMsg.Text = "";
            TxtPayAmount.Visible = false;
            LblPayAmount.Visible = false;
            LblSecondInput.Visible = false;
            TxtSecondInput.Visible = false;
        }

        /// <summary>
        /// validate user input controls
        /// </summary>
        /// <returns>true or false</returns>
        private bool IsValidTextBoxes()
        {
            string[] textBoxes = { TxtID.Text, TxtFirstName.Text, TxtLastName.Text, TxtAddress.Text, TxtCity.Text, TxtZip.Text, TxtHireDate.Text, TxtJobTitle.Text, TxtDept.Text};
            string[] regExStrings = { "\\b\\d{6}\\b", "\\b[A-Z][a-z]+\\b", "\\b[A-Z][a-z]+\\b", "\\w+", "\\b[A-Z][a-z]+\\b", "\\b\\d{5}\\b", "\\b\\d{2}[/]\\d{2}[/]\\d{4}\\b", "\\w+", "\\w+" };
            TextBox[] controls = { TxtID, TxtFirstName, TxtLastName, TxtAddress, TxtCity, TxtZip, TxtHireDate, TxtJobTitle, TxtDept };
            Match match;
            int i = 0;
            bool isValid = true;

            //checks all textboxes not including type testboxes
            foreach (var item in textBoxes)
            {
                match = Regex.Match(item, regExStrings[i]);
                if (!match.Success || item == "")
                {
                    if (item == "")
                    {
                        EpForm.SetError(controls[i], requiredMsg);
                    }
                    else
                    {
                        EpForm.SetError(controls[i], invalidField);                        
                    }
                    isValid = false;
                }
                i++;
            }
            if (CBxMarriageStatus.SelectedIndex == -1)
            {
                isValid = false;
                EpForm.SetError(CBxMarriageStatus, requiredMsg);
            }
            if (CBxEmpType.SelectedIndex == -1 || CBxState.SelectedIndex == -1)
            {
                isValid = false;
                if (CBxEmpType.SelectedIndex == -1)
                {
                    EpForm.SetError(CBxEmpType, requiredMsg);
                }
                if(CBxState.SelectedIndex == -1)
                {
                    EpForm.SetError(CBxState, requiredMsg);
                }
            }
            else //type combobox has been selected so now validate the textboxes
            {
                match = Regex.Match(TxtPayAmount.Text, @"(?:\d*\.)?\d+");
                if (!match.Success)
                {
                    isValid = false;
                    EpForm.SetError(TxtPayAmount, invalidField);
                }
                if (CBxEmpType.SelectedIndex != (int)EType.SALARY || CBxEmpType.SelectedIndex != (int)EType.CONTRACT)
                {
                    if (!match.Success)
                    {
                        isValid = false;
                        EpForm.SetError(TxtSecondInput, invalidField);
                    }
                }
            }

            return isValid;
        }

        /// <summary>
        /// submit employee object into database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            int typeIndex = CBxEmpType.SelectedIndex;
            EpForm.Clear();
            LblAfterSubmitMsg.Text = "";
            //validate all user input before submit
            if (IsValidTextBoxes())
            {
                try
                {
                    businessRules.InstantiateEmployeeType(typeIndex, TxtFirstName.Text,
                        TxtLastName.Text, TxtID.Text, TxtAddress.Text, TxtCity.Text, CBxState.Text, TxtZip.Text,
                        TxtHireDate.Text, CBxMarriageStatus.Text, TxtJobTitle.Text, TxtDept.Text, TxtPayAmount.Text, TxtSecondInput.Text);
                    ClearNewEmpForm();
                    BlinkLabel("Employee Sucessfully Added!", Color.Blue, LblAfterSubmitMsg);
                    testIndex++;
                }
                catch (Exception expt)
                {
                    BlinkLabel(expt.Message, Color.Red, LblAfterSubmitMsg);
                }
            }
            else
            {
                BlinkLabel("Employee Was Not Added!", Color.Red, LblAfterSubmitMsg);
            }
        }
        
        /// <summary>
        /// closes the form window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
            businessRules.GetFileIO.CloseDB();
        }

        /// <summary>
        /// Opens the database from file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShowEmps_Click(object sender, EventArgs e)
        {
            GetDatabase();
        }
        
        /// <summary>
        /// Opens the database from file
        /// </summary>
        private void GetDatabase()
        {
            try
            {
                businessRules.GetFileIO.OpenDB();
                DisplayAllEmployeesInDataBase();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        
        /// <summary>
        /// clear all text boxes to be empty or default values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearList_Click(object sender, EventArgs e)
        {
            RTBxEmpInfo.Clear();
        }
        
        /// <summary>
        /// On click of Menu Open, calls open database from file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuOpen_Click(object sender, EventArgs e)
        {
            GetDatabase();
        }
        
        /// <summary>
        /// Using an indexer, displays current database to a textbox
        /// </summary>
        private void DisplayAllEmployeesInDataBase()
        {
            RTBxEmpInfo.Clear();
            RTBxEmpInfo.AppendText(businessRules.DisplayEmployees());
        }
        
        /// <summary>
        /// Menu Save on click calls save database to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnuSave_Click(object sender, EventArgs e)
        {
            SaveDatabase();
        }
        
        /// <summary>
        /// on click calls save database to file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveDatabase();
        }
        
        /// <summary>
        /// Saves the current database to file
        /// </summary>
        private void SaveDatabase()
        {
            try
            {
                businessRules.GetFileIO.CloseDB();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }


        /// <summary>
        /// Starts Search for employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            RTBxEmpInfo.Clear();
            EpForm.Clear();
            string output;
            
            if (RBtnSearchID.Checked || RBtnSearchLastName.Checked)
            {
                if (RBtnSearchID.Checked) {
                    if (valid.ValidIDFormat(TxtSearchEmployee.Text))
                    {
                        uint key = uint.Parse(TxtSearchEmployee.Text);
                        Employee emp = businessRules[key];
                        output = businessRules.QueryEmpID(TxtSearchEmployee.Text);
                        if (ChBxBenefits.Checked && ChBxCourses.Checked) { output = businessRules.QueryEmpID(TxtSearchEmployee.Text) + emp.DisplayBenefits() + emp.DisplayCourses(); }
                        else if (ChBxBenefits.Checked) { output = businessRules.QueryEmpID(TxtSearchEmployee.Text) + emp.DisplayBenefits(); }
                        else {
                            if (ChBxCourses.Checked) {
                                output = businessRules.QueryEmpID(TxtSearchEmployee.Text) + emp.DisplayCourses();
                            }
                        }
                        RTBxEmpInfo.AppendText(output);
                    }
                    else { EpForm.SetError(TxtSearchEmployee, "Invalid ID"); }
                }
                else {
                    if (valid.ValidLastNameFormat(TxtSearchEmployee.Text))
                    {
                        output = businessRules.QueryEmpLastName(TxtSearchEmployee.Text);
                        if (ChBxBenefits.Checked && ChBxCourses.Checked) { output = businessRules.QueryLastNameBenefitsCourses(TxtSearchEmployee.Text); }
                        else if (ChBxBenefits.Checked) { output = businessRules.QueryLastNamesWithBenefits(TxtSearchEmployee.Text); }
                        else { output = businessRules.QueryLastNameWithCourses(TxtSearchEmployee.Text); }
                        RTBxEmpInfo.AppendText(output);
                    }
                    else { EpForm.SetError(TxtSearchEmployee, "Invalid Last Name"); }
                }
            }
            else 
            {
                if (!(RBtnSearchID.Checked || RBtnSearchLastName.Checked))
                {
                    EpForm.SetError(RBtnSearchID, "Choose One");
                    EpForm.SetError(RBtnSearchLastName, "Choose One");
                }
            }
        }
        /// <summary>
        /// Submits a Course
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSubmitCourse_Click(object sender, EventArgs e)
        {
            string grade, date = "", school, courseId, courseName, credits, empId;
            Control[] txtControls = { TxtCourseEmpId, TxtCollegeName, TxtCourseId, TxtCourseName};
            //Control[] OtherControls = { CBxCredits, CBxGrades };
            List<Control> txtControlErrors = valid.validateCourseData(txtControls);

            try
            {
                if (txtControlErrors.Count > 0 || !IsCourseControlsChecked())
                {
                    if (txtControlErrors.Count > 0)
                    {
                        foreach (var item in txtControlErrors)
                        {
                            if (item == TxtCollegeName) { EpForm.SetError(item, "Utah Valley University Only"); }
                            else if (item == TxtCourseEmpId) { EpForm.SetError(item, "Invalid or non-existant ID"); }
                            else if (item == TxtCourseId) { EpForm.SetError(item, "Invalid Course ID"); }
                            else {
                                if (item == TxtCourseName) { EpForm.SetError(item, "Invalid Course Name"); }
                            }
                        }
                    }
                    BlinkLabel("Course Was Not Added!", Color.Red, LblSucessCourse);
                }
                else
                {
                    if (ChBxApproved.Checked)
                    {
                        date = DTPrApprovedData.Text;
                    }
                    else { date = "Course Not Approved"; }
                    empId = TxtCourseEmpId.Text;
                    school = TxtCollegeName.Text;
                    courseId = TxtCourseId.Text;
                    courseName = TxtCourseName.Text;
                    credits = CBxCredits.SelectedItem.ToString();
                    grade = CBxGrades.SelectedItem.ToString();
                    businessRules.AddCourse(empId, school, courseId, courseName, credits, grade, date);
                    ClearAddCourseInfo();

                    BlinkLabel("Course Sucessfully Added!", Color.Blue, LblSucessCourse);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        /// <summary>
        /// Flashes a label
        /// </summary>
        private async void BlinkLabel(string _message, Color _color, Label _lable)
        {
            //flash result for 3 seconds
            _lable.ForeColor = _color;
            for (int i = 0; i < 5; i++)
            {
                _lable.Text = _message;
                await Task.Delay(300);
                _lable.Text = "";
                await Task.Delay(300);
            }
        }
    
        /// <summary>
        /// Validates if Comboboxes have been clicked
        /// </summary>
        /// <returns>bool</returns>
        private bool IsCourseControlsChecked()
        {
            if (!(CBxCredits.SelectedIndex > -1) || !(CBxGrades.SelectedIndex > -1))
            {
                if (!(CBxCredits.SelectedIndex > -1)) { EpForm.SetError(CBxCredits, "Credits not Selected"); }
                else { EpForm.SetError(CBxGrades, "Grade not Selected"); }
                return false;
            }
            return true;
        }
        /// <summary>
        /// Event to enable or dispable approved date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChBxApproved_CheckedChanged(object sender, EventArgs e)
        {
            if (ChBxApproved.Checked) { DTPrApprovedData.Enabled = true; }
            else { DTPrApprovedData.Enabled = false; }
        }

        /// <summary>
        /// Clears/set to default values for course fields
        /// </summary>
        private void ClearAddCourseInfo()
        {
            TxtCourseEmpId.Clear();
            TxtCollegeName.Clear();
            TxtCourseId.Clear();
            TxtCourseName.Clear();
            ChBxApproved.Checked = false;
            CBxCredits.SelectedIndex = -1;
            CBxGrades.SelectedIndex = -1;
            DTPrApprovedData.Enabled = false;
        }
        /// <summary>
        /// Course Test Data for input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTestDataAddCourse_Click(object sender, EventArgs e)
        {
            ClearAddCourseInfo();
            int count = 6;
            if(testIndex < count)
            {
                TxtCourseEmpId.Text = testData.empId[testIndex];
                TxtCollegeName.Text = "Utah Valley University";
                TxtCourseId.Text = testData.courseId[testIndex];
                TxtCourseName.Text = testData.courseName[testIndex];
                ChBxApproved.Checked = testData.approved[testIndex];
                CBxCredits.SelectedIndex = 2;
                CBxGrades.SelectedIndex = testData.grades[testIndex];
                DTPrApprovedData.Text = testData.approvedDate[testIndex];
                testIndex++;
            }
            else { testIndex = 0; }
        }
        /// <summary>
        /// Activates selected query
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CBxQueries_SelectedIndexChanged(object sender, EventArgs e)
        {
            RTBxEmpInfo.Clear();
            switch (CBxQueries.SelectedIndex)
            {
                case 0:
                    RTBxEmpInfo.AppendText(businessRules.QueryEmpSortedLastName());
                    break;
                case 1:
                    RTBxEmpInfo.AppendText(businessRules.QueryEmpSortFirstName());
                    break;
                case 2:
                    RTBxEmpInfo.AppendText(businessRules.QueryHasAnyBenefits());
                    break;
                case 3:
                    RTBxEmpInfo.AppendText(businessRules.QueryEmpHasCourses());
                    break;
                case 4:
                    RTBxEmpInfo.AppendText(businessRules.QueryEmpWhoAreMarried());
                    break;
                case 5:
                    RTBxEmpInfo.AppendText(businessRules.QueryEmpWhoAreSingle());
                    break;
                default:
                    break;
            }
        }
    }//end Main
}//end namespace