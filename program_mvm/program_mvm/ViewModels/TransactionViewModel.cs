using Caliburn.Micro;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using System.Drawing;
using ZXing.QrCode;
using Microsoft.Win32;
using System.Windows;

namespace program_mvm.ViewModels
{
    public class TransactionViewModel : Screen
    {
        //MyDatabaseEntities2 mydb = new MyDatabaseEntities2();
        private MainAtt mainAtt;
        private StockViewModel stockVM;

        List<DetailTransaction> dtsList;
        List<DetailTransaction> fullDtsList;
        private BindableCollection<DetailTransaction> _detailTransactions;
        private BindableCollection<DetailTransaction> _fullDetailTransactions;
        private VoucherViewModel voucherVm;
        private DetailServiceViewModel dtServiceVM;


        private List<DetailServiceTransaction> dtServiceList;


        public BindableCollection<DetailTransaction> FullDetailTransactions
        {
            get { return _fullDetailTransactions; }
            set { _fullDetailTransactions = value; }
        }

        public TransactionViewModel()
        {
            mainAtt = MainAtt.getInstance();
            voucherVm = new VoucherViewModel();
            dtServiceVM = new DetailServiceViewModel();
            stockVM = new StockViewModel();
            initDB();
        }
        public TransactionViewModel(string htID)
        {

            mainAtt = MainAtt.getInstance();
            Ht =mainAtt.Mydb.HeaderTransactions.Where(a => a.HeaderID.Equals(htID)).FirstOrDefault();
            voucherVm = new VoucherViewModel();
            dtServiceVM = new DetailServiceViewModel();
            stockVM = new StockViewModel();
            initDB();
            UpdateTotalPrice();
        }

        public BindableCollection<DetailTransaction> DetailTransactions
        {
            get { return _detailTransactions; }
            set { _detailTransactions = value;
                NotifyOfPropertyChange(() => DetailTransactions);
            }
        }
        private HeaderTransaction _ht;

        public HeaderTransaction Ht
        {
            get { return _ht; }
            set {
                _ht = value;
            }
        }

      
        private double _totalPrice;

        public double TotalPrice
        {
            get { return  _totalPrice; }
            set {  _totalPrice = value;
                NotifyOfPropertyChange(() => TotalPrice);
            }
        }



        private string _productID;

        public string ProductID
        {
            get { return _productID; }
            set { _productID = value;
                NotifyOfPropertyChange(()=>ProductID);
            }
        }

        private string _productName;

        public string ProductName
        {
            get { return _productName; }
            set { _productName = value;
                NotifyOfPropertyChange(() => ProductName);
                   }
        }

        private int _qty;

        public int Qty
        {
            get { return _qty; }
            set { _qty = value;
                NotifyOfPropertyChange(()=>Qty); 
            }
        }
        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value;
                NotifyOfPropertyChange(()=>Status);
            }
        }

        private double _priceNoDiscount;

        public double PriceNoDiscount
        {
            get { return _priceNoDiscount; }
            set { _priceNoDiscount = value;
                NotifyOfPropertyChange(()=>PriceNoDiscount);
            }
        }

        private double _discount;

        public double Discount         {
            get { return _discount; }
            set {
                _discount = value;
                NotifyOfPropertyChange(() => Discount);

            }
        }
        

        public void UpdateTotalPrice()
        {
            
                //    .Where(o => o.HeaderID == Ht.HeaderID)
                var temp = (from d in mainAtt.Mydb.DetailTransactions
                            join p in mainAtt.Mydb.Products on d.ProductID equals p.ProductID
                            where d.HeaderID == Ht.HeaderID
                            select new { a = d.quantity * p.ProductPrice }).ToList();
                double totalTemp = 0;
                //System.Windows.MessageBox.Show(temp[0].ToString());
                for (int i = 0; i < temp.Count; i++)
                {
                    totalTemp += Convert.ToDouble(temp[i].a);
                }
                if(_voucher != null)
                { 
                    TotalPrice = totalTemp - totalTemp * (Double)(_voucher.VoucherDiscount / 100);
                }else
                    TotalPrice = totalTemp;
                PriceNoDiscount = totalTemp;

                var temp2 = dtServiceVM.GetDetailServiceTransactions(Ht.HeaderID);
                
                for(int i=0; i < temp2.Count; i++)
                {
                    TotalPrice += (double)temp2[i].ServicePrice;
                PriceNoDiscount += (double)temp2[i].ServicePrice;
                }
        }

        public void initDB()
        {
            if (Ht!=null) {
                fullDtsList = mainAtt.Mydb.DetailTransactions.ToList();
                FullDetailTransactions = new BindableCollection<DetailTransaction>(fullDtsList);
                //dtsList = mainAtt.Mydb.DetailTransactions.Where(o=>o.HeaderID == Ht.HeaderID).ToList();
                dtsList = (from dt in mainAtt.Mydb.DetailTransactions join p in mainAtt.Mydb.Products on dt.ProductID equals p.ProductID where dt.HeaderID == Ht.HeaderID select dt).ToList();
                DetailTransactions = new BindableCollection<DetailTransaction>(dtsList);
                //dtsList[1].Product.

            }
        }
        private DetailTransaction _selectedRow;

        public DetailTransaction SelectedRow
        {
            get { return _selectedRow; }
            set {
                _selectedRow = value;
                if(_selectedRow != null)
                {
                    ProductID = _selectedRow.ProductID;
                    Qty = Convert.ToInt32(_selectedRow.quantity);
                    Status = _selectedRow.status;
                    Product product = mainAtt.Mydb.Products.Where(o => o.ProductID == ProductID).SingleOrDefault();
                    ProductName = product.ProductName;
                }
            }
        }

        public void DoCreateHeaderTransaction()
        {
            if(Ht == null)
            {
                List<HeaderTransaction> tempHts = mainAtt.Mydb.HeaderTransactions.ToList<HeaderTransaction>();
                int last = tempHts.Count - 1;
                Ht = new HeaderTransaction();
                if (!tempHts.Any())
                {
                    Ht.HeaderID = "HE001";
                }
                else
                {

                    HeaderTransaction temp = tempHts[last];
                    int lastID = Convert.ToInt32(temp.HeaderID[2].ToString() + temp.HeaderID[3].ToString() + temp.HeaderID[4].ToString())+1;
                    int lastID2 = lastID;
                  //  System.Windows.MessageBox.Show(lastID2.ToString());
                    int count = 0;
                    string headerIDTemp;
                    while (lastID2 > 0)
                    {
                        count++;
                        lastID2 /= 10;
                    }

                    if (count == 1)
                    {
                        headerIDTemp = "HE00" + lastID;
                    }
                    else if (count == 2)
                    {
                        headerIDTemp = "HE0" + lastID;
                    }
                    else 
                    {
                        headerIDTemp = "HE" + lastID;
                    }
                    Ht.HeaderID = headerIDTemp;
                }
               // System.Windows.MessageBox.Show(Ht.HeaderID);
                Ht.CustomerID = null;
                Ht.Employee = null;
                Ht.TotalPrice = 0;
                Ht.CustomerCash = 0;
                //Ht.CustomerID = "CU001";
                Ht.TransactionDate = DateTime.Now;
                try
                {
                    mainAtt.Mydb.HeaderTransactions.Add(Ht);
                    mainAtt.Mydb.SaveChanges();

                }
                catch (Exception)
                {

                }

            }
        }

        private Voucher _voucher;

        public Voucher Voucher
        {
            get { return _voucher; }
            set
            {
                _voucher = value;
                if(Voucher  != null)
                {
                    TotalPrice = TotalPrice - TotalPrice * (Double)(_voucher.VoucherDiscount / 100);
                    Discount = (double)Voucher.VoucherDiscount;

                }
            }

        }





        public void DoaddDetailTransaction()
        {
            //System.Windows.MessageBox.Show(_username);
           
            DoCreateHeaderTransaction();
            
            initDB();   
            
            string tempo;
            if (fullDtsList.Count ==0)
            {
                tempo = "DE001";
            }
            else
            {

                int last = DetailTransactions.Count - 1;
                tempo = (fullDtsList[fullDtsList.Count - 1]).DetailID.ToString();
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
                    tempo = "DE00" + numberID.ToString();

                }
                else if (count == 2)
                {
                    tempo = "DE0" + numberID.ToString();
                }
                else
                {
                    tempo = "DE"+ numberID.ToString();
                }
            }
            DetailTransaction temp = new DetailTransaction();
            temp.DetailID = tempo;
            temp.HeaderID = Ht.HeaderID;
            temp.ProductID = _productID;
            temp.quantity = _qty;
            temp.status = _status;


            mainAtt.Mydb.DetailTransactions.Add(temp);
            try
            {
                mainAtt.Mydb.SaveChanges();

            }
            catch (Exception)
            {

            }
            initDB();
            UpdateTotalPrice();
        }


        public void DoUpdateDetailTransaction()
        {
            if (_selectedRow == null)
            {
                System.Windows.MessageBox.Show("Need to select data");
            }
            else
            {
                DetailTransaction dt = mainAtt.Mydb.DetailTransactions.Single(o=>o.DetailID == _selectedRow.DetailID);
                dt.quantity = _qty;
                dt.ProductID = _productID;
                mainAtt.Mydb.SaveChanges();
                initDB();
            }
            UpdateTotalPrice();
        }
        //public void DoTest()
        //{
        //    System.Windows.MessageBox.Show(SelectedRow.EmployeeId);
        //}

        public void DoDeleteDetailTransaction()
        {
            if (_selectedRow == null)
            {
                System.Windows.MessageBox.Show("Need to select data");
            }
            else
            {
                DetailTransaction tempDetailTransaction = mainAtt.Mydb.DetailTransactions.FirstOrDefault(p => p.DetailID == _selectedRow.DetailID);
                mainAtt.Mydb.DetailTransactions.Remove(tempDetailTransaction);
                mainAtt.Mydb.SaveChanges();

                initDB();
                System.Windows.MessageBox.Show("Scucess " + tempDetailTransaction.DetailID);
            }
        }

      

        private string _pathFileName;

        public string PathFileName
        {
            get { return _pathFileName; }
            set { _pathFileName = value;
                NotifyOfPropertyChange(() => PathFileName);
            }
        }

        public void DoApplyVoucher()
        {
            var dialogService = new OpenFileDialog();
            dialogService.ShowDialog();
            string path = dialogService.FileName;
            //System.Windows.MessageBox.Show(path);

            PathFileName = path;
            Bitmap barcodeBitMap=null;
            try
            {
                barcodeBitMap = (Bitmap)Bitmap.FromFile(path);

            }
            catch (Exception)
            {
                return;
                throw;
            }
            var barcodeReader = new BarcodeReader();
            string resultBarcode = barcodeReader.Decode(barcodeBitMap).ToString();

            string tempID = resultBarcode;
            Voucher v = voucherVm.FindVoucher(tempID);
            if (v != null && v.IsApplied ==0 )
            {
                v.IsApplied = 1;
                Voucher = v;
            }
            else
            {
                System.Windows.MessageBox.Show("Not found Or Applied");
            }


        }
        public void DoScanProduct()
        {
            var dialogService = new OpenFileDialog();
            dialogService.ShowDialog();
            string path = dialogService.FileName;
            //System.Windows.MessageBox.Show(path);
   
            PathFileName = path;


            Bitmap barcodeBitMap = null;
            try
            {
                barcodeBitMap = (Bitmap)Bitmap.FromFile(path);

            }
            catch (Exception)
            {
                return;
                throw;
            }
            var barcodeReader = new BarcodeReader();
            string resultBarcode = barcodeReader.Decode(barcodeBitMap).ToString();

            ProductID = resultBarcode;
            Product p = mainAtt.Mydb.Products.Where(a=> a.ProductID == ProductID).FirstOrDefault();
            if (p != null)
            {
                ProductID = p.ProductID;
                ProductName = p.ProductName;
                Status = null;
                Qty = 1;
                DoaddDetailTransaction();

            }else
            {
                System.Windows.MessageBox.Show("Not found");
            }
            

        }

        public void DoPayment()
        {
            if (Ht == null)
            {
                System.Windows.MessageBox.Show("You dont have any transaction");
                return;
            }

            var forCheck = mainAtt.Mydb.DetailTransactions.Where(a => a.HeaderID == Ht.HeaderID);
            if(forCheck == null)
            {
                System.Windows.MessageBox.Show("You dont have any transaction");
                return;
            }


            MessageBoxResult confirmResult = MessageBox.Show("Do you have membership?", "Select",MessageBoxButton.YesNo);
            
            if(confirmResult == MessageBoxResult.Yes)
            {
                var dialogService = new OpenFileDialog();
                dialogService.ShowDialog();
                string path = dialogService.FileName;
                //System.Windows.MessageBox.Show(path);
                if(path == null)
                {
                    return;
                }
                PathFileName = path;

                Bitmap barcodeBitMap = null;
                try
                {
                    barcodeBitMap = (Bitmap)Bitmap.FromFile(path);

                }
                catch (Exception)
                {
                    return;
                    throw;
                }
                var barcodeReader = new BarcodeReader();
                string resultBarcode = barcodeReader.Decode(barcodeBitMap).ToString();

                string customerID = resultBarcode;

                Customer c = mainAtt.Mydb.Customers.Where(a => a.CustomerID == customerID).FirstOrDefault();

                if (c != null)
                {
                    double TotalPrice = Convert.ToDouble((from dt in mainAtt.Mydb.DetailTransactions join p in mainAtt.Mydb.Products on dt.ProductID equals p.ProductID where dt.HeaderID.Equals(Ht.HeaderID) select (p.ProductPrice * dt.quantity)).Sum());
                    double bonus = TotalPrice % 9 ;
                    c.Balance += (decimal)bonus;
                    MessageBox.Show("add point to customer ID successfully " + TotalPrice.ToString());
                }
                
               

            }
            else
            {

            }
            var htInDb = mainAtt.Mydb.HeaderTransactions.Where(a => a.HeaderID == Ht.HeaderID).FirstOrDefault();
            htInDb.TotalPrice = (decimal)TotalPrice;

            //MessageBoxResult tet = MessageBox.Show("Do you have membership?", "Select", MessageBoxButton.YesNo);

            confirmResult = MessageBox.Show("Do you want to use debit?", "Select", MessageBoxButton.YesNo);
            string inputValue;
            if (confirmResult == MessageBoxResult.Yes) {
                try
                {
                    inputValue = Microsoft.VisualBasic.Interaction.InputBox("Input: ","Input DebitTransactionID");
                    htInDb.DebitTransactionID = inputValue;

                }
                catch (Exception)
                {
                    return;
                }
            }else
            {
                double balance = 0;

                while (balance < TotalPrice)
                {
                    try
                    {
                        inputValue = Microsoft.VisualBasic.Interaction.InputBox("Input: ", "Input DebitTransactionID");

                    }
                    catch (Exception)
                    {
                        return;
                        throw;
                    }
                    try
                    {
                        balance = Convert.ToDouble(inputValue);                    
                    }
                    catch (Exception)
                    {
                        System.Windows.MessageBox.Show("Please input Correct Balance");
                        continue;
                        throw;
                    }
                    if (balance < TotalPrice)
                    {
                        System.Windows.MessageBox.Show("Your balance doesnt enough");
                    }

                }
                htInDb.CustomerCash = (decimal)balance;


            }





            MessageBox.Show("payment has been paid");
            stockVM.DoDecreaseStock(dtsList);
            mainAtt.Mydb.SaveChanges();
            Ht = null;
            DetailTransactions = null;
            TotalPrice = 0;
            Discount = 0;
            Voucher = null;
            PriceNoDiscount = 0;
            
            initDB();

        }

        public void RefreshPage()
        {
            if (viewModel == null && Ht != null)
            {
                viewModel = new TransactionViewModel(Ht.HeaderID);
                otherView.ShowWindow(viewModel, null, null);
                this.TryClose();
            }
        }

        public void ShowBillMenu()
        {
            
            if (Ht == null) DoCreateHeaderTransaction();
                otherView.ShowWindow(new DetailServiceViewModel(Ht.HeaderID), null, null);
            
        }
        public void ShowDetailServiceTransaction()
        {
            if (Ht == null)
            {
                System.Windows.MessageBox.Show("Please make a Service Transaction");
                return;
            }
            otherView.ShowWindow(new BillViewModel(Ht.HeaderID), null, null);
        }




        /// <summary>
        /// 
        /// </summary>

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
