using Caliburn.Micro;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm.ViewModels
{
    public class ReportingViewModel : Conductor<Object>
    {

        private MainAtt mainAtt;
        public ReportingViewModel()
        {
            mainAtt = MainAtt.getInstance();
        }

        private HeaderTransaction _ht;

        public HeaderTransaction Ht
        {
            get { return _ht; }
            set { _ht = value; }
        }


        public void initDBTransaction()
        {
            //Ht
        }


        public void GenerateTransactionReport() {
            Application excel = new Microsoft.Office.Interop.Excel.Application();

            if (excel == null)
            {
                System.Windows.MessageBox.Show("Excel is not generated");
            }
            else
            {
                object misValue = System.Reflection.Missing.Value;
                Workbook xlWorkBook = excel.Workbooks.Add(misValue);
                Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);

                var result = (from ht in mainAtt.Mydb.HeaderTransactions
                              join dt in mainAtt.Mydb.DetailTransactions on ht.HeaderID equals dt.HeaderID
                              join pro in mainAtt.Mydb.Products on dt.ProductID equals pro.ProductID
                              select new { Header = ht, Detail = dt, Product = pro }).ToList();

                xlWorkSheet.Cells[1, 1] = "headerID";
                xlWorkSheet.Cells[1, 2] = "DetailID";
                xlWorkSheet.Cells[1, 3] = "TransactionDate";
                xlWorkSheet.Cells[1, 4] = "VoucherID";
                xlWorkSheet.Cells[1, 5] = "ProductID";
                xlWorkSheet.Cells[1, 6] = "ProductPrice";
                xlWorkSheet.Cells[1, 7] = "Quantity";
                xlWorkSheet.Cells[1, 8] = "EmployeeID";
                xlWorkSheet.Cells[1, 9] = "CustomerID";
                xlWorkSheet.Cells[1, 10] = "TotalPrice";

                for (int i = 0; i < result.Count; i++)
                {
                    xlWorkSheet.Cells[i + 2, 1] = result[i].Header.HeaderID;
                    xlWorkSheet.Cells[i + 2, 2] = result[i].Detail.DetailID;
                    xlWorkSheet.Cells[i + 2, 3] = result[i].Header.TransactionDate;
                    xlWorkSheet.Cells[i + 2, 4] = result[i].Header.VoucherID;
                    xlWorkSheet.Cells[i + 2, 5] = result[i].Product.ProductID;
                    xlWorkSheet.Cells[i + 2, 6] = result[i].Product.ProductPrice;
                    xlWorkSheet.Cells[i + 2, 7] = result[i].Detail.quantity;
                    xlWorkSheet.Cells[i + 2, 8] = result[i].Header.CashierID;
                    xlWorkSheet.Cells[i + 2, 9] = result[i].Header.CustomerID;
                    xlWorkSheet.Cells[i + 2, 10] = result[i].Header.TotalPrice;

                }

                xlWorkBook.SaveAs(@"C:\Users\user\Desktop\TPA_Desktop\TransactionReport", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                excel.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(excel);
                System.Windows.MessageBox.Show("created transaction");
            }


        }
        public void GenerateMembershipReport()
        {
            Application excel = new Microsoft.Office.Interop.Excel.Application();

            if (excel == null)
            {
                System.Windows.MessageBox.Show("Excel is not generated");
            }
            else
            {
                object misValue = System.Reflection.Missing.Value;
                Workbook xlWorkBook = excel.Workbooks.Add(misValue);
                Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);

                var result = mainAtt.Mydb.Customers.ToList();

                xlWorkSheet.Cells[1, 1] = "CustomerID";
                xlWorkSheet.Cells[1, 2] = "CustomerName";
                xlWorkSheet.Cells[1, 3] = "CustomerEmail";
                xlWorkSheet.Cells[1, 4] = "CustomerAddress";
                xlWorkSheet.Cells[1, 5] = "CustomerPhoneNumber";
                xlWorkSheet.Cells[1, 6] = "CustomerDOB";
                
                for (int i = 0; i < result.Count; i++)
                {
                    xlWorkSheet.Cells[i + 2, 1] = result[i].CustomerID;
                    xlWorkSheet.Cells[i + 2, 2] = result[i].CustomerName;
                    xlWorkSheet.Cells[i + 2, 3] = result[i].CustomerEmail;
                    xlWorkSheet.Cells[i + 2, 4] = result[i].CustomerAddress;
                    xlWorkSheet.Cells[i + 2, 5] = result[i].CustomerPhoneNumber;
                    xlWorkSheet.Cells[i + 2, 6] = result[i].CustomerDOB;
                    
                }

                xlWorkBook.SaveAs(@"C:\Users\user\Desktop\TPA_Desktop\MembershipReport", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                excel.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(excel);
                System.Windows.MessageBox.Show("Membership report created");
            }
        } 

        public void GenerateProductReport()
        {
            Application excel = new Microsoft.Office.Interop.Excel.Application();

            if (excel == null)
            {
                System.Windows.MessageBox.Show("Excel is not generated");
            }
            else
            {
                object misValue = System.Reflection.Missing.Value;
                Workbook xlWorkBook = excel.Workbooks.Add(misValue);
                Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);

                var result = (from p in mainAtt.Mydb.Products
                              join s in mainAtt.Mydb.Stocks on p.ProductID equals s.ProductID
                              select new { Product = p, Stock = s }).OrderBy(a=>a.Product.ProductID).ToList();

                xlWorkSheet.Cells[1, 1] = "ProductID";
                xlWorkSheet.Cells[1, 2] = "StockID";
                xlWorkSheet.Cells[1, 3] = "ProductName";
                xlWorkSheet.Cells[1, 4] = "ProdductPrice";
                xlWorkSheet.Cells[1, 5] = "Sell";
                xlWorkSheet.Cells[1, 6] = "Available Stocks";
                xlWorkSheet.Cells[1, 7] = "Many IN";
                xlWorkSheet.Cells[1, 8] = "Many OUT";
                xlWorkSheet.Cells[1, 9] = "Is Reward Item";
                xlWorkSheet.Cells[1, 10] = "Reward Stocks";
                xlWorkSheet.Cells[1, 11] = "Broken Stocks";

                for (int i = 0; i < result.Count; i++)
                {
                    xlWorkSheet.Cells[i + 2, 1] = result[i].Product.ProductID;
                    xlWorkSheet.Cells[i + 2, 2] = result[i].Stock.StockID;
                    xlWorkSheet.Cells[i + 2, 3] = result[i].Product.ProductName;
                    xlWorkSheet.Cells[i + 2, 4] = result[i].Product.ProductPrice;
                    xlWorkSheet.Cells[i + 2, 5] = result[i].Product.Price; //sellPricec
                    xlWorkSheet.Cells[i + 2, 6] = result[i].Stock.AvailableStocks;
                    xlWorkSheet.Cells[i + 2, 7] = result[i].Stock.ManyIn;
                    xlWorkSheet.Cells[i + 2, 8] = result[i].Stock.ManyOut;
                    xlWorkSheet.Cells[i + 2, 9] = result[i].Stock.IsRewardItem;
                    xlWorkSheet.Cells[i + 2, 10] = result[i].Stock.RewardStocks;
                    xlWorkSheet.Cells[i + 2, 11] = result[i].Stock.BrokenStocks;

                }

                xlWorkBook.SaveAs(@"C:\Users\user\Desktop\TPA_Desktop\ProductReport", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(excel);
                System.Windows.MessageBox.Show("Product Report Created");
            }
        }
        public void GenerateHumanResourceReport()
        {
            Application excel = new Microsoft.Office.Interop.Excel.Application();

            if (excel == null)
            {
                System.Windows.MessageBox.Show("Excel is not generated");
            }
            else
            {
                object misValue = System.Reflection.Missing.Value;
                Workbook xlWorkBook = excel.Workbooks.Add(misValue);
                Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);

                var result = mainAtt.Mydb.Employees.ToList();

                xlWorkSheet.Cells[1, 1] = "Employee ID";
                xlWorkSheet.Cells[1, 2] = "Employee Email";
                xlWorkSheet.Cells[1, 3] = "Employee Name";
                xlWorkSheet.Cells[1, 4] = "EmployeePhoneNumber";
                xlWorkSheet.Cells[1, 5] = "Employee Salary";
                xlWorkSheet.Cells[1, 6] = "Employee Date Of Birth";
         

                for (int i = 0; i < result.Count; i++)
                {
                    xlWorkSheet.Cells[i + 2, 1] = result[i].EmployeeId;
                    xlWorkSheet.Cells[i + 2, 2] = result[i].EmployeeEmail;
                    xlWorkSheet.Cells[i + 2, 3] = result[i].EmployeeName;
                    xlWorkSheet.Cells[i + 2, 4] = result[i].EmployeePhoneNumber;
                    xlWorkSheet.Cells[i + 2, 5] = result[i].EmployeeSalary; //sellPricec
                    xlWorkSheet.Cells[i + 2, 6] = result[i].EmployeeDOB;
                    
                }

                xlWorkBook.SaveAs(@"C:\Users\user\Desktop\TPA_Desktop\EmployeeReport", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(excel);
                System.Windows.MessageBox.Show("Human Resource Report created");
            }
        }
        public void GenerateExpenseRevenueProfitReport()
        {
            Application excel = new Microsoft.Office.Interop.Excel.Application();

            if (excel == null)
            {
                System.Windows.MessageBox.Show("Excel is not generated");
            }
            else
            {
                object misValue = System.Reflection.Missing.Value;
                Workbook xlWorkBook = excel.Workbooks.Add(misValue);
                Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);

                var result = mainAtt.Mydb.Journals.ToList() ;

                xlWorkSheet.Cells[1, 1] = "JournalID";
                xlWorkSheet.Cells[1, 2] = "Date";
                xlWorkSheet.Cells[1, 3] = "Debits";
                xlWorkSheet.Cells[1, 4] = "Credits";
                xlWorkSheet.Cells[1, 5] = "Revenue";
               
                xlWorkSheet.Cells[1, 6] = "Descriptipn";
                for (int i = 0; i < result.Count; i++)
                {
                    xlWorkSheet.Cells[i + 2, 1] = result[i].JournalID;
                    xlWorkSheet.Cells[i + 2, 2] = result[i].JournalDate;
                    xlWorkSheet.Cells[i + 2, 3] = result[i].debits;
                    xlWorkSheet.Cells[i + 2, 4] = result[i].credits;
                    xlWorkSheet.Cells[i + 2, 5] = result[i].revenue;
                    xlWorkSheet.Cells[i + 2, 6] = result[i].Description;
                }

                xlWorkBook.SaveAs(@"C:\Users\user\Desktop\TPA_Desktop\JournalReport", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                excel.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(excel);
                System.Windows.MessageBox.Show("created Journal");
            }

        }
        public void GenerateTaxReport()
        {
            Application excel = new Microsoft.Office.Interop.Excel.Application();

            if (excel == null)
            {
                System.Windows.MessageBox.Show("Excel is not generated");
            }
            else
            {
                object misValue = System.Reflection.Missing.Value;
                Workbook xlWorkBook = excel.Workbooks.Add(misValue);
                Worksheet xlWorkSheet = xlWorkBook.Worksheets.get_Item(1);

                var result = mainAtt.Mydb.Journals.ToList();

                xlWorkSheet.Cells[1, 1] = "JournalID";
                xlWorkSheet.Cells[1, 2] = "Date";
                xlWorkSheet.Cells[1, 3] = "Tax";
                int index = 2;
                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].Tax == 0 || result[i].Tax==null) continue;
                    xlWorkSheet.Cells[index, 1] = result[i].JournalID;
                    xlWorkSheet.Cells[index, 2] = result[i].JournalDate;
                    xlWorkSheet.Cells[index, 3] = result[i].Tax;
                    index++;
                }

                xlWorkBook.SaveAs(@"C:\Users\user\Desktop\TPA_Desktop\TaxReport", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                excel.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(excel);
                System.Windows.MessageBox.Show("created Tax");
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


        private void GoToEmployeeMenu()
        {
            if (viewModel == null)
            {
                viewModel = new EmployeeViewModel();
                otherView.ShowWindow(viewModel, null, null);
                this.TryClose();
            }
        }
        private void GoToProductMenu()
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
