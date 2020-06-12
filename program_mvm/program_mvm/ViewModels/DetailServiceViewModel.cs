using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm.ViewModels
{
    public class DetailServiceViewModel: Conductor<Object>
    {

        private MainAtt mainAtt;
        private string _htID;
        private TransactionViewModel tvm;
        public string HtID
        {
            get { return _htID; }
            set { _htID = value; }
        }

        private string _selectedServiceType;
        public string SelectedServiceType
        {
            get { return _selectedServiceType; }
            set {
                _selectedServiceType = value;
                if (_selectedServiceType != null)
                {
                    if (_selectedServiceType.Equals("pulse"))
                    {
                        Services = pulses;
                    }else if(_selectedServiceType.Equals("electricity"))
                    {
                        Services = electricities;
                    }
                }

            }
        }

        private string _selectedService;
        public string SelectedService
        {
            get { return _selectedService; }
            set { _selectedService = value; }
        }


        private double _selectedPrice;
        public double SelectedPrice
        {
            get { return _selectedPrice; }
            set { _selectedPrice = value; }
        }

        private List<string> _serviceTypes;

        public List<string> ServiceTypes
        {
            get { return _serviceTypes; }
            set { _serviceTypes = value; }
        }

        private List<string> _services;

        public List<string> Services
        {
            get { return _services; }
            set {  _services = value;
                NotifyOfPropertyChange(() => Services);
            }
        }

        private List<double> _prices;

        public List<double> Prices
        {
            get { return _prices; }
            set { _prices = value; }
        }

        private List<string> pulses;
        private List<string> electricities;

        public DetailServiceViewModel(string htID)
        {
            tvm = new TransactionViewModel();
            mainAtt = MainAtt.getInstance();
            HtID = htID;
            ServiceTypes = new List<string>();
            pulses = new List<string>();
            Prices = new List<double>();
            electricities = new List<string>();
            ServiceTypes.Add("pulse");
            ServiceTypes.Add("electricity");


            pulses.Add("Telkomsel");
            pulses.Add("Indosat");
            pulses.Add("XL");

            electricities.Add("MyPLN");
            electricities.Add("YourElec");

            Prices.Add(11000);
            Prices.Add(21000);
            Prices.Add(52000);
            Prices.Add(101000);
            Prices.Add(201000);



        }
        public DetailServiceViewModel()
        {
            mainAtt = MainAtt.getInstance();
        }

        public List<DetailServiceTransaction> GetDetailServiceTransactions(string htID)
        {
            List<DetailServiceTransaction> temp = mainAtt.Mydb.DetailServiceTransactions.Where(a => a.HeaderID.Equals(htID)).ToList();
            return temp;
        }


        public bool ValidateField()
        {
            if (SelectedPrice == null || SelectedService == null || SelectedServiceType == null)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public void DoAddServiceTransaction()
        {
            if (ValidateField())
            {
                System.Windows.MessageBox.Show("All field must be filled!");
                return;
            }

            DetailServiceTransaction d = new DetailServiceTransaction();
            var fulldb = mainAtt.Mydb.DetailServiceTransactions.ToList();
            string lastID;
            if (fulldb.Count==0)
            {
                lastID = "DS001";
            }else
            {
                lastID = fulldb[fulldb.Count - 1].DetailServiceID;
                int temp = Convert.ToInt32(lastID[2].ToString() + lastID[3].ToString() + lastID[4].ToString())+1;
                int count=0;
                int temp2 = temp ;

                while (temp2 != 0)
                {
                    temp2 /= 10;
                    count++;
                }
                if (count == 1)
                {
                    lastID = "DS00" + temp.ToString();

                }
                else if (count == 2)
                {
                    lastID = "DS0" + temp.ToString();
                }
                else
                {
                    lastID = "DS" + temp.ToString();
                }
                
            }
            DetailServiceTransaction dst = new DetailServiceTransaction();
            dst.HeaderID = HtID;
            dst.DetailServiceID = lastID;
            if (_selectedService.Equals("Telkomsel"))
            {
                dst.ServiceID = "SE001";
            }
            else if (_selectedService.Equals("Indosat"))
            {
                dst.ServiceID = "SE002";
            }
            else if (_selectedService.Equals("XL"))
            {
                dst.ServiceID = "SE003";
            }
            else if (_selectedService.Equals("MyPLN"))
            {
                dst.ServiceID = "SE004";
            }
            else if (_selectedService.Equals("YourElec"))
            {
                dst.ServiceID = "SE005";
            }
            dst.ServicePrice = (decimal)SelectedPrice;
            mainAtt.Mydb.DetailServiceTransactions.Add(dst);
            mainAtt.Mydb.SaveChanges();
            System.Windows.MessageBox.Show("Success add a bill");
            this.TryClose();
        }
    }
}
