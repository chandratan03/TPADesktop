using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm.Models
{
    public class EmployeeModel
    {
        private string employeeID { get; set; }
        private string employeeName { get; set; }
        private string employeePassword { get; set; }
        private string employeeEmail { get; set; }
        private string employeePhoneNumber { get; set; }
        private string employeeDOB { get; set; }
        private string employeAddress { get; set; }
        private string roleID { get; set; }
        private double employeeSalary { get; set; }
        private string workShift { get; set; }

        public EmployeeModel(
                string employeeID, 
            string employeeName,string employeePassword,
                    string employeeEmail,
                    string employeePhoneNumber,
                    string employeeDOB,
                    string employeAddress,
                    string roleID,
                    double employeeSalary,
                    string workShift
                )
        {
            this.employeeID = employeeID;
            this.employeeName = employeeName;
            this.employeePassword = employeePassword;
            this.employeeEmail = employeeEmail;
            this.employeePhoneNumber = employeePhoneNumber;
            this.employeeDOB = employeeDOB;
            this.employeAddress = employeAddress;
            this.roleID = roleID;
            this.employeeSalary = employeeSalary;
            this.workShift = workShift;

        }

    }
}
