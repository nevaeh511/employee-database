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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Windows.Forms;

namespace Employee_Database
{
    /// <summary>
    /// Interface for file IO access
    /// </summary>
    public interface IFileAccess
    {
        void WriteDB();
        void ReadDB();
        void OpenDB();
        void CloseDB();
        SortedDictionary<uint, Employee> DB { get; set; }

    }//end IFileAccess interface

    public class FileIO : object, IFileAccess
    {
        /// <summary>
        /// Data Members
        /// </summary>
        private const string EXTENTIONS = "All Files (*.*) | *.* | Text Files (*.txt) | *.txt | Serialized Files (*.ser) | *.ser";
        private SortedDictionary<uint, Employee> employeeDatabase;
        private BinaryFormatter binaryFormatter;
        private OpenFileDialog openFile;
        private SaveFileDialog saveFile;
        private string fileName;
        private Stream stream;

        /// <summary>
        /// Parameterized constructor 
        /// </summary>
        /// <param name="_emp"></param>
        public FileIO(SortedDictionary<uint, Employee> _emp)
        {
            employeeDatabase = _emp;
            binaryFormatter = null;
            openFile = null;
            saveFile = null;
            fileName = null;
            stream = null;
        }
        /// <summary>
        /// indexer that gets or sets a Sorted Dicionary
        /// </summary>
        public SortedDictionary<uint, Employee> DB
        {
            get
            {
                return employeeDatabase;
            }

            set
            {
                employeeDatabase = value;
            }
        }
        /// <summary>
        /// Saves the database to the given file name
        /// </summary>
        public void CloseDB()
        {
            try
            {
                saveFile = new SaveFileDialog();
                saveFile.Filter = EXTENTIONS;
                saveFile.FilterIndex = 3;
                saveFile.DefaultExt = "ser";
                DialogResult saveResult = saveFile.ShowDialog();
                fileName = saveFile.FileName;
                if (stream == null && saveResult != DialogResult.Cancel)
                {
                    using (stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        WriteDB();
                    }
                    stream = null;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        /// <summary>
        /// Opens a file containing the database to read
        /// </summary>
        public void OpenDB()
        {
            try
            {
                openFile = new OpenFileDialog();
                openFile.Filter = EXTENTIONS;
                openFile.FilterIndex = 3;
                openFile.DefaultExt = "ser";
                DialogResult result = openFile.ShowDialog();
                fileName = openFile.FileName;
                binaryFormatter = new BinaryFormatter();
                if (stream == null)
                {
                    if (openFile.CheckFileExists && result != DialogResult.Cancel && fileName != null)
                    {
                        using(stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            ReadDB();
                        }
                        stream = null;
                    }
                }
                
            }
            catch (Exception exp)
            {
                throw exp;
            }
            
        }
        /// <summary>
        /// Deserializes the stream containing the database 
        /// </summary>
        public void ReadDB()
        {
            if(stream != null)
            {
                employeeDatabase = (SortedDictionary<uint, Employee>)binaryFormatter.Deserialize(stream);
                BusinessRules.Instance.EmployeeList = employeeDatabase;
            }
        }
        /// <summary>
        /// Serializes the sorted dictionary to a stream
        /// </summary>
        public void WriteDB()//when saving the database
        {
            if(stream != null)
            {
                //List<KeyValuePair<uint, Employee>> list = employeeDatabase.ToList();
                binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, employeeDatabase);
            }
        }
 
    }//end class FileIO
}
