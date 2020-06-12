using Caliburn.Micro;
using program_mvm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace program_mvm.ViewModels
{


    public class EmployeeViewModel : Conductor<Object>
    {

        private MainAtt mainAtt;
        private BindableCollection<Employee> _employees;
        List<Employee> temp;
        private List<Employee> _fullEmployee;
        public BindableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                NotifyOfPropertyChange(() => Employees);
            }
        }
        private Employee _employee;

        public Employee Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;

            }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => Username);
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        private string _employeeEmail;

        public string EmployeeEmail
        {
            get { return _employeeEmail; }
            set
            {
                _employeeEmail = value;
                NotifyOfPropertyChange(() => EmployeeEmail);
            }

        }


        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                NotifyOfPropertyChange(() => Address);
            }
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                NotifyOfPropertyChange(() => PhoneNumber);
            }
        }

        private string _dob;

        public string Dob
        {
            get { return _dob; }
            set
            {
                _dob = value;
                NotifyOfPropertyChange(() => Dob);
            }
        }

        private string _roleID;

        public string RoleID
        {
            get { return _roleID; }
            set
            {
                _roleID = value;
                NotifyOfPropertyChange(() => RoleID);
            }
        }

        private double _employeeSalary;

        public double EmployeeSalary
        {
            get { return _employeeSalary; }
            set
            {
                _employeeSalary = value;
                NotifyOfPropertyChange(() => EmployeeSalary);
            }
        }

        private string _workShift;

        public string WorkShift
        {
            get { return _workShift; }
            set
            {
                _workShift = value;
                NotifyOfPropertyChange(() => WorkShift);
            }
        }



        private Employee _selectedRow;

        public Employee SelectedRow
        {
            get { return _selectedRow; }
            set
            {
                _selectedRow = value;
                NotifyOfPropertyChange("SelectedRow");
                if (_selectedRow != null)
                {
                    Username = _selectedRow.EmployeeName;
                    EmployeeEmail = _selectedRow.EmployeeEmail;
                    Password = _selectedRow.EmployeePassword;
                    Address = _selectedRow.EmployeeAddress;
                    PhoneNumber = _selectedRow.EmployeePhoneNumber;
                    Dob = (_selectedRow.EmployeeDOB).ToString();
                    RoleID = _selectedRow.RoleID;
                    EmployeeSalary = (double)_selectedRow.EmployeeSalary;
                    WorkShift = _selectedRow.WorkShift;
                }

            }
        }

        public void initDB()
        {
            temp = mainAtt.Mydb.Employees.Where(a => a.IsActive == 1).ToList();
            Employees = new BindableCollection<Employee>(temp);
            _fullEmployee = mainAtt.Mydb.Employees.ToList();
        }


        private MenuItem _transactionMenu;

        public MenuItem TransactionMenu
        {
            get { return _transactionMenu; }
            set { _transactionMenu = value; }
        }


        public EmployeeViewModel()
        {
            mainAtt = MainAtt.getInstance();
            initDB();
            DateTime timeNow = DateTime.Now;
            //DateTime aaa = timeNow.Date;
            //System.Windows.MessageBox.Show(aaa.ToString());
        }

        private DataGridCellInfo _cellInfo;
        private Employee _employeeTemp;

        public Employee EmployeeTemp
        {
            get { return EmployeeTemp; }
            set { EmployeeTemp = value; }
        }

        public void DoaddEmployee()
        {
            string tempo = "";
            //System.Windows.MessageBox.Show(_username);
            if (_fullEmployee.Count == 0)
            {
                tempo = "EM001";
            }
            else
            {

                tempo = (_fullEmployee[_fullEmployee.Count - 1]).EmployeeId.ToString();
                int last = temp.Count - 1;
                //int numberID = Convert.ToInt32(tempo[2].ToString() + tempo[3].ToString() + tempo[4] + 1.ToString());
                int numberID = Convert.ToInt32(tempo[2].ToString() + tempo[3].ToString() + tempo[4].ToString()) + 1;
                //int numberID = 33;
                int numberID2 = numberID;
                int count = 0;
                while (numberID2 > 0)
                {
                    count++;
                    numberID2 /= 10;

                }
                if (count == 1)
                {
                    tempo = "EM00" + numberID.ToString();

                }
                else if (count == 2)
                {
                    tempo = "EM0" + numberID.ToString();
                }
                else
                {
                    tempo = "EM" + numberID.ToString();
                }
            }
            Employee = new Employee();

            Employee.EmployeeId = tempo;
            //System.Windows.MessageBox.Show(Employee.EmployeeId);
            Employee.EmployeeName = _username;
            Employee.EmployeePassword = _password;
            Employee.EmployeeEmail = _employeeEmail;
            Employee.EmployeePhoneNumber = _phoneNumber;
            try
            {
                Employee.EmployeeDOB = Convert.ToDateTime(_dob);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Please input valid date");
                return;
                //throw;
            }

            Employee.EmployeeAddress = _address;


            try
            {
                Employee.EmployeeSalary = (decimal)_employeeSalary;
                if (_employeeSalary <= 0)
                {
                    System.Windows.MessageBox.Show("Please put a valid salary");
                    return;
                }
            }
            catch (Exception)
            {

                System.Windows.MessageBox.Show("Please put a valid salary");
                return;
            }
            Employee.WorkShift = _workShift;
            Employee.RoleID = "RO001";
            Employee.IsActive = 1;
            mainAtt.Mydb.Employees.Add(Employee);

            //            System.Windows.MessageBox.Show(Employee.EmployeeId);
            //Employee tempEmployee = mydb.Employees.FirstOrDefault(x => x.EmployeeId.Equals("EM007"));
            mainAtt.Mydb.SaveChanges();
            initDB();
        }


        public void DoUpdateEmployee()
        {
            if (_selectedRow == null)
            {
                System.Windows.MessageBox.Show("Need to select data");
            }
            else
            {
                Employee empTemp = mainAtt.Mydb.Employees.Single(x => x.EmployeeId == _selectedRow.EmployeeId);
                empTemp.EmployeeName = Username;
                System.Windows.MessageBox.Show(empTemp.EmployeeName);
                empTemp.EmployeeEmail = EmployeeEmail;
                empTemp.EmployeePassword = Password;
                empTemp.EmployeeAddress = Address;
                empTemp.EmployeePhoneNumber = PhoneNumber;
                try
                {
                    empTemp.EmployeeDOB = Convert.ToDateTime(Dob);
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Please input valid date");
                    return;
                    //throw;
                }
                empTemp.RoleID = RoleID;
                try
                {
                    empTemp.EmployeeSalary = (decimal)_employeeSalary;
                    if (_employeeSalary <= 0)
                    {
                        System.Windows.MessageBox.Show("Please put a valid salary");
                        return;
                    }
                }
                catch (Exception)
                {

                    System.Windows.MessageBox.Show("Please put a valid salary");
                    return;
                }
                empTemp.WorkShift = WorkShift;
                mainAtt.Mydb.SaveChanges();
                initDB();
            }
        }
        public void DoAddViolation()
        {
            if (_selectedRow == null)
            {
                System.Windows.MessageBox.Show("Must select a employee");
                return;
            }
            otherView.ShowWindow(new ViolationViewModel(_selectedRow.EmployeeId), null, null);

        }
        public void DoTest()
        {
            System.Windows.MessageBox.Show(SelectedRow.EmployeeId);
        }

        public void DoDeleteEmployee()
        {
            if (_selectedRow == null)
            {
                System.Windows.MessageBox.Show("Need to select data");
            }
            else
            {
                Employee tempEmployee = mainAtt.Mydb.Employees.FirstOrDefault(p => p.EmployeeId == _selectedRow.EmployeeId);
                tempEmployee.IsActive = 0;
                mainAtt.Mydb.SaveChanges();

                initDB();
                System.Windows.MessageBox.Show("Scucess " + tempEmployee.EmployeeId);
            }
        }

        IWindowManager otherView = new WindowManager();
        Screen viewModel = null;
        public void GoToTransactionMenu()
        {
            if (viewModel == null)
            {
                viewModel = new TransactionViewModel();
                otherView.ShowWindow(viewModel, null, null);
                this.TryClose();
            }
        }

        public void GoToCustomerMenu()
        {
            if (viewModel == null)
            {
                viewModel = new CustomerViewModel();
                otherView.ShowWindow(viewModel, null, null);
                this.TryClose();
            }
        }

        public void GoToVoucherMenu()
        {
            if (viewModel == null)
            {
                viewModel = new VoucherViewModel();
                otherView.ShowWindow(viewModel, null, null);
                this.TryClose();
            }
        }

        public void GoToReportingMenu()
        {
            if (viewModel == null)
            {
                viewModel = new ReportingViewModel();
                otherView.ShowWindow(viewModel, null, null);
                this.TryClose();
            }
        }


        public void GoToEmployeeMenu()
        {
            if (viewModel == null)
            {
                viewModel = new EmployeeViewModel();
                otherView.ShowWindow(viewModel, null, null);
                this.TryClose();
            }
        }
        public void GoToProductMenu()
        {
            if (viewModel == null)
            {
                viewModel = new ProductViewModel();
                otherView.ShowWindow(viewModel, null, null);
                this.TryClose();
            }
        }
        public void GoToRecruiterMenu()
        {
            if (viewModel == null)
            {
                viewModel = new RecruiterViewModel();
                otherView.ShowWindow(viewModel, null, null);
                this.TryClose();
            }
        }
        public void GoToJournalMenu()
        {
            if (viewModel == null)
            {
                viewModel = new JournalViewModel();
                otherView.ShowWindow(viewModel, null, null);
                this.TryClose();
            }

        }
    }
}
