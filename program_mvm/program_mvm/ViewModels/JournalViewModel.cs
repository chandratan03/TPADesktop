using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm.ViewModels
{
    public class JournalViewModel :Conductor<Object>
    {
        private MainAtt mainAtt;
        private List<Journal> Journals;

        public List<Journal> MyProperty
        {
            get { return Journals; }
            set { Journals = value; }
        }

        public void initDB()
        {
            Journals = mainAtt.Mydb.Journals.ToList() ;
        }
        public JournalViewModel()
        {
            mainAtt = MainAtt.getInstance();
            initDB();
        }

        public void UpdateData()
        {
            var currDate = DateTime.Now.Date;
            var lastHtDate = mainAtt.Mydb.HeaderTransactions.OrderByDescending(a => a.HeaderID).FirstOrDefault();
            DateTime htDate = Convert.ToDateTime(lastHtDate.TransactionDate).Date;
            List<HeaderTransaction> htsTemp = mainAtt.Mydb.HeaderTransactions.ToList();
            List<HeaderTransaction> hts = new List<HeaderTransaction>();

            for(int i=0; i < htsTemp.Count; i++)
            {
                DateTime temp2 = Convert.ToDateTime(htsTemp[i].TransactionDate).Date;
                if (temp2.ToString().Equals(currDate.ToString())){
                    hts.Add(htsTemp[i]);
                } 
            }

            Journal j = new Journal();

            j.JournalDate = DateTime.Now;
            double totalRevenue = 0;
            for(int i=0; i<hts.Count; i++)
            {
                totalRevenue += (double)hts[i].TotalPrice;
            }
            j.revenue = (decimal)totalRevenue;


            var lastStockCard =  mainAtt.Mydb.Stocks.OrderByDescending(a=>a.StockID).FirstOrDefault();
            var lastStockCardDate = Convert.ToDateTime(lastStockCard.Date).Date;

            var stockCardsTemp = mainAtt.Mydb.Stocks.ToList();
            List<Stock> stockCards = new List<Stock>();

            for (int i = 0; i < stockCardsTemp.Count; i++)
            {
                DateTime tempDate = Convert.ToDateTime(stockCardsTemp[i].Date).Date;

                if (tempDate.ToString().Equals(lastStockCardDate.ToString()))
                {
                    stockCards.Add(stockCardsTemp[i]);
                }
            }
            double credit = 0;
            for(int i=0; i<stockCards.Count; i++)
            {
                var productID = stockCards[i].ProductID;
                int qty = Convert.ToInt32(stockCards[i].ManyIn);
                var product = mainAtt.Mydb.Products.Where(a => a.ProductID == productID).FirstOrDefault();
                //System.Windows.MessageBox.Show(product.ProductPrice.ToString());
                //double productPrice;
                credit += ((double)product.Price*qty);
            }
            j.credits =(decimal) credit;

            j.debits = j.revenue - j.credits;

            j.Tax = 0;


            int day = Convert.ToDateTime(j.JournalDate).Date.Day;
            
            if(day == 1)//UPDATE TAX SETIAP TANGGAL 1
            {
                j.Tax = (decimal)((double)j.debits * 0.5);
                j.debits = j.debits - j.Tax;
            }
            



            string tempo;
            if (Journals.Count == 0)
            {
                tempo = "JO001";
            }
            else
            {

                //int last = Journals.Count - 1;
                tempo = (Journals[Journals.Count - 1]).JournalID.ToString();
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
                    tempo = "JO00" + numberID.ToString();

                }
                else if (count == 2)
                {
                    tempo = "JO0" + numberID.ToString();
                }
                else
                {
                    tempo = "JO"+numberID.ToString();
                }
            }
            j.JournalID = tempo;
            
            mainAtt.Mydb.Journals.Add(j);
            mainAtt.Mydb.SaveChanges();
            initDB();
            System.Windows.MessageBox.Show("Update Successfully");
        }

        public void ShowExpensesChart()
        {
            ActivateItem(new ExpensesViewModel());
            //System.Windows.MessageBox.Show("test");
        }

        public void ShowIncomeChart()
        {
            ActivateItem(new IncomeViewModel());
            //System.Windows.MessageBox.Show("test");
        }
        public void ShowRevenueChart()
        {
            ActivateItem(new RevenueViewModel());
            //System.Windows.MessageBox.Show("test");
        }
        public void ShowTaxChart()
        {
            ActivateItem(new TaxViewModel());
            //System.Windows.MessageBox.Show("test");
        }
        IWindowManager otherView = new WindowManager();
        Screen viewModel = null;

        public void DoRegisterMember()
        {
            //if (viewModel == null)
            //{
            //viewModel = new RegisterMemberViewModel();
            otherView.ShowWindow(new RegisterMemberViewModel(), null, null);
            //}
        }
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
