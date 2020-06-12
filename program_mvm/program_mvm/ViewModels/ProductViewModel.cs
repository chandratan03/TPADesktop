using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace program_mvm.ViewModels
{
    public class ProductViewModel : Conductor<Object>
    {
    
        private StockViewModel stockViewModel = new StockViewModel();
        private string _productName;
        private Product _selectedRow;
        private MainAtt mainAtt;
        public Product SelectedRow
        {
            get { return _selectedRow; }
            set {
                _selectedRow = value;
                if (_selectedRow != null) {
                    ProductName = SelectedRow.ProductName.ToString();
                    MgfDate = SelectedRow.MgfDate.ToString();
                    ExpDate = SelectedRow.ExpDate.ToString();
                    MeasuringUnit = SelectedRow.MeasuringUnit.ToString();
                    ProductPrice = (double)SelectedRow.ProductPrice;
                    SellPrice = (double)SelectedRow.Price;
                    ProductID = _selectedRow.ProductID.ToString();

                }
                
            }
        }

        private int _restockPoint;

        public int RestockPoint
        {
            get { return _restockPoint; }
            set { _restockPoint = value; }
        }

        private List<Product> _fullProductList;
        private List<Product> _productList;
        public List<Product> ProductList
        {
            get { return _productList; }
            set { _productList = value; }
        }



        private BindableCollection<Product> _bindProductList;
        public BindableCollection<Product> BindProductList
        {
            get {
                return _bindProductList;
            }
            set {
                _bindProductList = value;
                NotifyOfPropertyChange(()=>BindProductList);
            }
        }


        private string _productID;

        public string ProductID
        {
            get { return _productID; }
            set { _productID = value; }
        }



        public string ProductName
        {
            get { return _productName; }
            set {
                _productName = value;
                NotifyOfPropertyChange(() => ProductName);
            }
        }

        private double _productPrice;

        public double ProductPrice
        {
            get { return _productPrice; }
            set { _productPrice = value;
                NotifyOfPropertyChange(() => ProductPrice);
            }
        }
        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value;

               ;
            }
        }


        private string _mgfDate;

        public string MgfDate
        {
            get { return _mgfDate; }
            set { _mgfDate = value;
                NotifyOfPropertyChange(() => MgfDate);
            }
        }

        private string _expDate;

        public string ExpDate
        {
            get { return _expDate; }
            set { _expDate = value;
                NotifyOfPropertyChange(() => ExpDate);
            }
        }

        private string _measuringUnit;

        public string MeasuringUnit
        {
            get { return _measuringUnit; }
            set { _measuringUnit = value;
                NotifyOfPropertyChange(() => MeasuringUnit);
            }
        }

        private double _sellPrice;

        public  double SellPrice
        {
            get { return _sellPrice; }
            set { _sellPrice = value;
                NotifyOfPropertyChange(() => SellPrice);
            }
        }

        private bool _Active;

        public bool Active
        {
            get { return _Active; }
            set { _Active = value;
                NotifyOfPropertyChange(() => Active);
            }
        }

        private Promo _promo;

        public Promo Promo
        {
            get { return _promo; }
            set { _promo = value;
                NotifyOfPropertyChange(() => Promo);
            }
        }


        public bool ValidateField()
        {
            if (ProductName == null || MgfDate == null || ExpDate==null || MeasuringUnit == null || ProductPrice <=0 ||  Quantity<=0)
            {
                return true;
            }

            return false;
        }
        private void initDB()
        {
            //ProductList = mainAtt.Mydb.Products.Where(a => a.IsActive == 1).ToList();
            ProductList = (from p in mainAtt.Mydb.Products join s in mainAtt.Mydb.Stocks on p.ProductID equals s.ProductID where p.IsActive == 1 orderby s.StockID descending select p).Distinct().ToList();

            BindProductList = new BindableCollection<Product>(ProductList);
            _fullProductList = mainAtt.Mydb.Products.ToList();
        }

        public void DoAddProduct()
        {
            if (Quantity <= 0)
            {
                System.Windows.MessageBox.Show("Please input valid quantity");
            }
            if (ValidateField())
            {
                System.Windows.MessageBox.Show("all Field must be filled");
                return;
            }
            string lastProductID;
            if (_fullProductList.Count == 0)
            {
                lastProductID = "PR001";
            }
            else
            {
                lastProductID = (_fullProductList[_fullProductList.Count - 1]).ProductID.ToString();
                int lastID = Convert.ToInt32(lastProductID[2].ToString() + lastProductID[3].ToString() + lastProductID[4].ToString()) + 1;
                int temp = lastID;
                int count = 0;
                while (temp != 0)
                {
                    temp /= 10;
                    count++;
                }

                if (count == 1)
                {
                    lastProductID = "PR00" + lastID.ToString();

                }
                else if (count == 2)
                {
                    lastProductID = "PR0" + lastID.ToString();
                }
                else
                {
                    lastProductID = "PR" + lastID.ToString();
                }
            }
            
            Product p = new Product();
            
            p.ProductID = lastProductID;
            p.ProductName = ProductName;

            try
            {
                p.ExpDate = Convert.ToDateTime(ExpDate);
                p.MgfDate = Convert.ToDateTime(MgfDate);

            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Please input valid date");
                return;
            }
            p.MeasuringUnit = MeasuringUnit;
            p.Price = 0; // sell price
            try
            {
                p.ProductPrice =  (decimal)ProductPrice;

            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("please input valid product price");
                return;
            }
            p.IsActive = 1;
            p.RestockPoint = RestockPoint;
            mainAtt.Mydb.Products.Add(p);
            mainAtt.Mydb.SaveChanges();
            stockViewModel.DoRestockProduct(p.ProductID, Quantity, (double)p.ProductPrice, false);
            DoCreateBarcodeForProduct(lastProductID);
            initDB();

        }
        public void DoDeleteProduct()
        {
            if (ValidateField())
            {
                System.Windows.MessageBox.Show("all Field must be filled");
                return;
            }

            if (SelectedRow == null)
            {
                System.Windows.MessageBox.Show("ga dapet :)");
            }else
            {
                Product p = mainAtt.Mydb.Products.FirstOrDefault(a => a.ProductID == SelectedRow.ProductID);
                p.IsActive = 0 ;
                mainAtt.Mydb.SaveChanges();
                initDB();
            }
        }

        public void DoUpdateProduct()
        {
            if (ValidateField())
            {
                System.Windows.MessageBox.Show("all Field must be filled");
                return;
            }
            if (_selectedRow == null)
            {
                System.Windows.MessageBox.Show("Need to select data");
            }
            else
            {   
                


                Product p = mainAtt.Mydb.Products.Single(a=> a.ProductID == ProductID);
                p.RestockPoint = RestockPoint;
                p.MgfDate = Convert.ToDateTime(MgfDate);
                p.ExpDate = Convert.ToDateTime(ExpDate);
                p.ProductPrice = (decimal)ProductPrice;
                mainAtt.Mydb.SaveChanges();
                stockViewModel.DoRestockProduct(ProductID, Quantity, ProductPrice, false);
                initDB();
                SelectedRow = null;
            }
        }
        IWindowManager stockview = new WindowManager();
        Screen stockScreen = null;
        public void DoShowStock()
        {
            stockScreen = new StockViewModel();
            stockview.ShowWindow(stockScreen, null, null);

        }
        public ProductViewModel()
        {
            mainAtt = MainAtt.getInstance();
            initDB();
        }

 

        public void DoCreateBarcodeForProduct(string productID)
        {
            var qrCodeWriter = new BarcodeWriter();
            qrCodeWriter.Format = BarcodeFormat.CODE_128;
            qrCodeWriter.Write(productID).Save(@"C:\Users\user\Desktop\TPA_Desktop\program_mvm\barcodePAth\"+productID+".png");
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
