using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace program_mvm.ViewModels
{
    public class RecruiterViewModel: Conductor<Object>
    {
        private MainAtt mainAtt;
        private List<Recruiter> _recruiters;
        private List<Recruiter> _totalRecruiter;
        private Recruiter _selectedRow;

        public Recruiter SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value;
                //System.Windows.MessageBox.Show(_selectedRow.RecruiterName);
                if(_selectedRow != null)
                {
                    RecruiterID = _selectedRow.RecruiterID;
                    RecruiterName = _selectedRow.RecruiterName;
                    RecruiterEmail = _selectedRow.RecruiterEmail;
                    RecruiterPhoneNumber = _selectedRow.recruiterPhoneNumber;
                    RecruiterDOB = Convert.ToDateTime(_selectedRow.recruiterDOB);
                    RecruiterAddress = _selectedRow.recruiterAddress;
                    RecruiterGender = _selectedRow.recruiterGender;
                    NotifyOfPropertyChange(() => RecruiterGender);
                }
            }
        }

        private List<string> _genders;

        public List<string> Genders
        {
            get { return _genders; }
            set { _genders = value; }
        }

        public List<Recruiter> Recruiters
        {
            get { return _recruiters; }
            set { _recruiters = value;
                NotifyOfPropertyChange(()=>Recruiters);
            }
        }


        public RecruiterViewModel()
        {
            mainAtt = MainAtt.getInstance();
            initDB();
            Genders = new List<string>();
            Genders.Add("Male");
            Genders.Add("Female");
        }

        private string _recruiterID;

        public string RecruiterID
        {
            get { return _recruiterID; }
            set { _recruiterID = value; }
        }

        private string _recruiterName;

        public string RecruiterName
        {
            get { return _recruiterName; }
            set { _recruiterName = value;
                NotifyOfPropertyChange(() => RecruiterName);
            }
        }

        private string _recruiterEmail;

        public string RecruiterEmail
        {
            get { return _recruiterEmail; }
            set { _recruiterEmail = value;

                NotifyOfPropertyChange(() => RecruiterEmail);
            }
        }

        private string _recruiterPhoneNumber;

        public string RecruiterPhoneNumber
        {
            get { return _recruiterPhoneNumber; }
            set { _recruiterPhoneNumber = value;

                NotifyOfPropertyChange(() => RecruiterPhoneNumber);
            }
        }


        private DateTime _recruiterDOB;

        public DateTime RecruiterDOB
        {
            get { return _recruiterDOB; }
            set { _recruiterDOB = value;

                NotifyOfPropertyChange(() => RecruiterDOB);
            }
        }


        private string _recruiterAddress;

        public string RecruiterAddress
        {
            get { return _recruiterAddress; }
            set { _recruiterAddress = value;

                NotifyOfPropertyChange(() => RecruiterAddress);
            }
        }

        private string _recruiterGender;

        public string RecruiterGender
        {
            get { return _recruiterGender; }
            set { _recruiterGender = value;
                

            }
        }


        public void initDB()
        {
            Recruiters = mainAtt.Mydb.Recruiters.Where(a=> a.isActive==1).ToList();
            _totalRecruiter = mainAtt.Mydb.Recruiters.ToList();
        }


        public void DoAddRecruiter()
        {
            string tempo = "";
            //System.Windows.MessageBox.Show(_username);
            if (_totalRecruiter.Count == 0)
            {
                tempo = "RC001";
            }
            else
            {

                tempo = (_totalRecruiter[_totalRecruiter.Count - 1]).RecruiterID.ToString();
                int numberID = Convert.ToInt32(tempo[2].ToString() + tempo[3].ToString() + tempo[4].ToString()) + 1;
              
                int numberID2 = numberID;
                int count = 0;
                while (numberID2 > 0)
                {
                    count++;
                    numberID2 /= 10;

                }
                if (count == 1)
                {
                    tempo = "RC00" + numberID.ToString();

                }
                else if (count == 2)
                {
                    tempo = "RC0" + numberID.ToString();
                }
                else
                {
                    tempo = "RC" + numberID.ToString();
                }
            }
            Recruiter r = new Recruiter();
            r.RecruiterID = tempo;
            r.RecruiterName = RecruiterName;
            r.RecruiterEmail = RecruiterEmail;
            try
            {

                r.recruiterDOB = Convert.ToDateTime( RecruiterDOB);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("wrong format recuriterDOB");
                throw;
            }
            r.recruiterPhoneNumber = RecruiterPhoneNumber;
            r.recruiterAddress = RecruiterAddress;
            r.isActive = 1;
            mainAtt.Mydb.Recruiters.Add(r);
            System.Windows.MessageBox.Show("add success");
            mainAtt.Mydb.SaveChanges();
            initDB();
        }

        public void DoDeleteRecruiter()
        {
            if(SelectedRow == null)
            {
                System.Windows.MessageBox.Show("Must select a recruiter from table");
                return;
            }else
            {
                Recruiter r = mainAtt.Mydb.Recruiters.Where(a => a.RecruiterID == SelectedRow.RecruiterID).FirstOrDefault();
                r.isActive = 0;
                mainAtt.Mydb.SaveChanges();
                initDB(); 
            }
        }

        public void DoUpdateRecruiter()
        {
            if (SelectedRow == null)
            {
                System.Windows.MessageBox.Show("Must select a recruiter from table");
                return;
            }else
            {
                Recruiter r = mainAtt.Mydb.Recruiters.Where(a => a.RecruiterID == SelectedRow.RecruiterID).FirstOrDefault();
                r.recruiterAddress = RecruiterAddress;
                try
                {
                    r.recruiterDOB = Convert.ToDateTime(RecruiterDOB);

                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Wrong DOB Syntax");
                    return;
                    throw;
                }
                r.RecruiterEmail = RecruiterEmail;
                if(RecruiterGender == "")
                {
                    System.Windows.MessageBox.Show("Must select a gender");
                    return;
                }
                r.recruiterDOB = Convert.ToDateTime(RecruiterDOB);
                r.recruiterGender = RecruiterGender;
                //System.Windows.MessageBox.Show(RecruiterGender.ToString());
                r.RecruiterName = RecruiterName;
                r.recruiterPhoneNumber = RecruiterPhoneNumber;
                mainAtt.Mydb.SaveChanges();
                initDB();
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

        
    }

}

