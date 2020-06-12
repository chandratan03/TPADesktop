using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm.ViewModels
{
    public class RegisterMemberViewModel: Conductor<Object>
    {
        CustomerViewModel customerVM;

   
        private string _customerID;
        private string _customerName;
        private string _customerEmail;
        private string _customerPhoneNumber;
        private string _customerDOB;
        private string _customerAddress;


        public string CustomerID
        {
            get { return _customerID; }
            set
            {
                _customerID = value;
            }
        }
        private string _customerPassword;

        public string CustomerPassword
        {
            get { return _customerPassword; }
            set
            {
                _customerPassword = value; NotifyOfPropertyChange(() => CustomerPassword);
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
            set
            {
                _customerEmail = value; NotifyOfPropertyChange(() => CustomerEmail);
            }
        }
        public string CustomerPhoneNumber
        {
            get { return _customerPhoneNumber; }
            set
            {
                _customerPhoneNumber = value; NotifyOfPropertyChange(() => CustomerPhoneNumber);
            }
        }
        public string CustomerDOB
        {
            get { return _customerDOB; }
            set
            {
                _customerDOB = value; NotifyOfPropertyChange(() => CustomerDOB);
            }
        }
        public string CustomerAddress
        {
            get { return _customerAddress; }
            set
            {
                _customerAddress = value; NotifyOfPropertyChange(() => CustomerAddress);
            }
        }

        public void DoRegisterMember()
        {
            Customer c = new Customer();
            c.CustomerID = "";
            c.CustomerName = CustomerName;
            c.CustomerAddress = CustomerAddress;
            try
            {
                c.CustomerDOB = Convert.ToDateTime(CustomerDOB);

            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Must be Valid Date");
                return;
                throw;
            }
            c.CustomerEmail = CustomerEmail;
            c.CustomerPassword = CustomerDOB;
            c.CustomerPhoneNumber = CustomerPhoneNumber;
            customerVM.DoRegisterCustomer(c);
            System.Windows.MessageBox.Show("RegisterSuccess");
            this.TryClose();
        }

        public RegisterMemberViewModel()
        {
            customerVM = new CustomerViewModel();
        }

        

    }
}
