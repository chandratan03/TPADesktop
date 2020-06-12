using Caliburn.Micro;
using program_mvm.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;

namespace program_mvm.ViewModels
{
    public class LoginViewModel : Conductor<object>
    {
        public void ShowView(object viewModel)
        {
            var win = new Window();
            win.Content = viewModel;
            win.Show();
        }


        private string _email="";
        private string _password="";


        private PasswordBox _passwordBoxTest;

        public PasswordBox PasswordBoxTest
        {
            get { return _passwordBoxTest; }
            set { _passwordBoxTest = value; }
        }


        public string Email
        {
            get {
                return _email;
            }
            set {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }

        
        public string Password
        {
            get {
                return _password;
            }
            set {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }



        //public bool CanLogin(string email, string password)
        //{
        //    if (String.Equals(email, "admin") && String.Equals(password, "admin"))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        //public void Login(string email, string password)
        //{
        //    Email = "";
        //    Password = "";
        //}
                IWindowManager employeeView = new WindowManager();


        
        public void Login()
        {
            //System.Windows.MessageBox.Show(t.ToString());

            //System.Windows.MessageBox.Show(PasswordBoxTest.Password.ToString());
            //return;
            if (String.Equals(Email, "admin") && String.Equals(Password, "admin"))/**/
            {
                Email = "";
                Password = "";
                //ShowView(new EmployeeViewModel());
                //ActivateItem(new EmployeeViewModel());
                //var newView = new EmployeeViewModel();
                employeeView.ShowWindow(new EmployeeViewModel(), null, null);
                this.TryClose();
            }
            else
            {
                System.Windows.MessageBox.Show(Password);
                Email = "";
                Password = "";

            }
        }

    }
}
