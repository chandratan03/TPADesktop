using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace program_mvm.ViewModels
{
    public class CustomerViewModel: Conductor<Object>
    {
        private string _customerID;
        private string _customerName;
        private string _customerEmail;
        private string _customerPhoneNumber;
        private string _customerDOB;
        private string _customerAddress;
        private double _balance;
        private List<Customer> _customers;
        private BindableCollection<Customer> _bindCustomers;
        private MainAtt mainAtt;
        private Customer _selectedRow;

        public Customer SelectedRow
        {
            get { return _selectedRow; }
            set {
                _selectedRow = value;
                if (_selectedRow != null)
                {
                    CustomerEmail = _selectedRow.CustomerEmail;
                    CustomerID = _selectedRow.CustomerID;
                    CustomerPassword = _selectedRow.CustomerPassword;
                    CustomerName = _selectedRow.CustomerName;
                    CustomerPhoneNumber = _selectedRow.CustomerPhoneNumber;
                    CustomerAddress = _selectedRow.CustomerAddress;
                    Balance = (double)_selectedRow.Balance;
                    CustomerDOB = _selectedRow.CustomerDOB.ToString();
                }
            }
        }


        public BindableCollection<Customer> BindCustomers
        {
            get {
                return _bindCustomers;
            }
            set {
                _bindCustomers = value;
                NotifyOfPropertyChange(()=>BindCustomers);
            }
        }
        public List<Customer> Customers
        {
            get { return _customers; }
            set { _customers = value;
                NotifyOfPropertyChange(()=>Customers);
            }
        }
        private List<Customer> _fullCustomers;


        public string  CustomerID
        {
            get { return _customerID; }
            set {
                _customerID = value;
            }
        }
        private string _customerPassword;

        public string CustomerPassword
        {
            get { return _customerPassword; }
            set { _customerPassword = value; NotifyOfPropertyChange(() => CustomerPassword);
            }
        }


        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; NotifyOfPropertyChange(() => CustomerName); }
        }
        public string CustomerEmail
        {
            get { return _customerEmail; }
            set { _customerEmail = value; NotifyOfPropertyChange(() => CustomerEmail);
            }
        }
        public string CustomerPhoneNumber
        {
            get { return  _customerPhoneNumber; }
            set {  _customerPhoneNumber = value; NotifyOfPropertyChange(() => CustomerPhoneNumber);
            }
        }
        public string CustomerDOB
        {
            get { return _customerDOB; }
            set { _customerDOB = value; NotifyOfPropertyChange(() => CustomerDOB);
            }
        }
        public string CustomerAddress
        {
            get { return _customerAddress; }
            set { _customerAddress = value; NotifyOfPropertyChange(() => CustomerAddress);
            }
        }
        public double Balance
        {
            get { return _balance; }
            set { _balance = value; NotifyOfPropertyChange(() => Balance);
            }
        }

        public void initDB()
        {
            _fullCustomers = mainAtt.Mydb.Customers.ToList();
            Customers = mainAtt.Mydb.Customers.Where(a=>a.IsActive==1).ToList();
            _bindCustomers = new BindableCollection<Customer>(Customers);
        }
        
        public void DoRegisterCustomer(Customer c)
        {
            CustomerName = c.CustomerName;
            CustomerAddress = c.CustomerAddress;
            CustomerDOB = c.CustomerDOB.ToString();
            CustomerEmail = c.CustomerEmail;
            CustomerPassword = c.CustomerPassword;
            CustomerPhoneNumber = c.CustomerPhoneNumber;

            DoAddCustomer();
        }

        public void DoAddCustomer()
        {
            string temp = "";
            if (_fullCustomers.Count == 0)
            {
                temp = "CU001";
            }
            else
            {

                temp = _fullCustomers[_fullCustomers.Count - 1].CustomerID;
                int lastIDCount = Convert.ToInt32(temp[2].ToString() + temp[3].ToString() + temp[4].ToString());
                lastIDCount++;
                int count = 0;
                int temp2 = lastIDCount;

                while (temp2 > 0)
                {
                    temp2 /= 10;
                    count++;
                }

                if (count == 1)
                {
                    temp = "CU00" + lastIDCount.ToString();

                }
                else if (count == 2)
                {
                    temp = "CU0" + lastIDCount.ToString();
                }
                else
                {
                    temp = "CU" + lastIDCount.ToString();
                }
        
            }

            Customer c = new Customer();
            c.CustomerID = temp;
            c.CustomerName = CustomerName;
            c.CustomerAddress = CustomerAddress;
            c.CustomerEmail = CustomerEmail;
            c.CustomerPhoneNumber = CustomerPhoneNumber;
            try
            {
            c.CustomerDOB = Convert.ToDateTime(CustomerDOB);

            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Please input valid date");
                return;
            }
            c.CustomerAddress = CustomerAddress;
            try
            {
                c.Balance = 0;
                
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Please input valid balance");
                throw;
            }
            c.CustomerPassword = CustomerPassword;
            c.IsActive = 1;
            DoCreateCustomerBarcode(temp);
            mainAtt.Mydb.Customers.Add(c);
            mainAtt.Mydb.SaveChanges();
            
            initDB();
        }

        public void doUpdateCustomer()
        {
            if(_selectedRow == null)
            {
                System.Windows.MessageBox.Show("please select a row");
            }
            else
            {
                Customer c = mainAtt.Mydb.Customers.Where(a => a.CustomerID == _selectedRow.CustomerID).FirstOrDefault();
                c.CustomerName = CustomerName;
                c.CustomerEmail = CustomerEmail;
                c.CustomerPassword = CustomerPassword;
                c.CustomerAddress = CustomerAddress;
                try
                {
                    c.CustomerDOB = Convert.ToDateTime(CustomerDOB);

                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Please input valid date");
                    return;
                }
                c.CustomerPhoneNumber = CustomerPhoneNumber;
                c.Balance = (decimal)Balance;
                mainAtt.Mydb.SaveChanges();
                initDB();
            }

        }
        
        public void DoDeleteCustomer()
        {
            if(_selectedRow == null)
            {
                System.Windows.MessageBox.Show("please select a row");
            }
            else
            {
                Customer c = mainAtt.Mydb.Customers.Where(a => a.CustomerID == _selectedRow.CustomerID).FirstOrDefault();
                c.IsActive = 0;
                mainAtt.Mydb.SaveChanges();
                initDB();
            }
        }


        private void DoCreateCustomerBarcode(string customerID)
        {

            var qrCodeWriter = new BarcodeWriter();
            qrCodeWriter.Format = BarcodeFormat.QR_CODE;
            qrCodeWriter.Write(customerID).Save(@"C:\Users\user\Desktop\TPA_Desktop\program_mvm\barcodePAth\" + customerID + ".png");
        }



        public CustomerViewModel()
        {
            mainAtt = MainAtt.getInstance();
            initDB();
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
