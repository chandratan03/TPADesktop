using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm.ViewModels
{
    public class ViolationViewModel: Conductor<Object>
    {
        private MainAtt mainAtt;
        private List<Violation> _violations;
        private List<Violation> _fullViolations;
        private string employeeID;
        public List<Violation> Violations
        {
            get { return _violations; }
            set { _violations = value;
                NotifyOfPropertyChange(() => Violations);
            }
        }
        private Violation _selectedRow;

        public Violation SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; }
        }




        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


        public void initDB()
        {
            Violations = mainAtt.Mydb.Violations.Where(v => v.EmployeeID == employeeID).ToList();
            _fullViolations = mainAtt.Mydb.Violations.ToList();
        }

        public ViolationViewModel(string employeeID)
        {
            this.employeeID = employeeID;
            mainAtt = MainAtt.getInstance();
            initDB();
        }


        public void DoAddViolation()
        {
            string tempo = "";
            //System.Windows.MessageBox.Show(_username);
            if(Description == "")
            {
                System.Windows.MessageBox.Show("Please input violation Description");
                return;
            }

            if (_fullViolations.Count == 0)
            {
                tempo = "VI001";
            }
            else
            {

                tempo = (_fullViolations[_fullViolations.Count - 1]).ViolationID.ToString();
                
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
                    tempo = "VI00" + numberID.ToString();

                }
                else if (count == 2)
                {
                    tempo = "VI0" + numberID.ToString();
                }
                else
                {
                    tempo = "VI" + numberID.ToString();
                }
            }
            Violation v = new Violation();
            v.EmployeeID = employeeID;
            v.ViolationID = tempo;
            v.Description = Description;
            v.DateViolated = DateTime.Now;
            mainAtt.Mydb.Violations.Add(v);
            mainAtt.Mydb.SaveChanges();
            initDB();
        }
        public void DoDeleteViolation()
        {
            if(SelectedRow == null)
            {
                System.Windows.MessageBox.Show("Must select a row");
                return;

            }else
            {
                Violation v = mainAtt.Mydb.Violations.Where(a => a.ViolationID == _selectedRow.ViolationID).FirstOrDefault();
                mainAtt.Mydb.Violations.Remove(v);
                mainAtt.Mydb.SaveChanges();
                initDB();
            }
        }

    }
}
