using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace program_mvm.ViewModels
{
    public class VoucherViewModel: Conductor<Object>
    {
        private string _voucherID;
        private MainAtt mainAtt;


        public VoucherViewModel()
        {
            mainAtt = MainAtt.getInstance();
            initDB();
        }
        private Voucher _selectedRow;

        public Voucher SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value;
                if (_selectedRow!= null)
                {
                    VoucherID = _selectedRow.VoucherID;
                    VoucherDiscount = Convert.ToDouble(_selectedRow.VoucherDiscount);
                    IsApplied = "NotApplied";
                }
            }
        }


        private List<Voucher> _voucherList;

        public List<Voucher> VoucherList
        {
            get { return _voucherList; }
            set { _voucherList = value;
  
                NotifyOfPropertyChange(() => VoucherList);
            }
        }


        private List<Voucher> _allVoucherList;
        public void initDB()
        {

            VoucherList = mainAtt.Mydb.Vouchers.Where(v=>v.IsActive == 1).ToList();
            _allVoucherList = mainAtt.Mydb.Vouchers.ToList();
        }


        public void DoAddVoucher()
        {
            string lastID = "";
            if(_allVoucherList.Count == 0)
            {
                lastID = "VO001";
            }else
            {
                lastID = (_allVoucherList[_allVoucherList.Count-1].VoucherID).ToString();
                int temp = Convert.ToInt32(lastID[2].ToString() + lastID[3].ToString() + lastID[4].ToString()) + 1;
                int temp2 = temp;
                int count = 0;
                while (temp2 != 0)
                {
                    temp2 /= 10;
                    count++;
                }

                if (count == 1)
                {
                    lastID = "VO00" + temp.ToString();
                }
                else if (count == 2)
                {
                    lastID = "VO0" + temp.ToString();
                }
                else
                {
                    lastID = "VO" + temp.ToString();
                }
                
            }
            Voucher v = new Voucher();
            v.VoucherID = lastID;
            try
            {
                v.VoucherDiscount = Convert.ToDecimal(VoucherDiscount);
                if(v.VoucherDiscount> 100 || v.VoucherDiscount <= 0)
                {
                    System.Windows.MessageBox.Show("Voucher Discount must be valid");
                    
                    return;
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Voucher Discount must be valid");
                return;
            }
            System.Windows.MessageBox.Show(IsApplied);
            v.IsApplied = 0;
            v.IsActive = 1;
            mainAtt.Mydb.Vouchers.Add(v);
            mainAtt.Mydb.SaveChanges();
            DoCreateVoucherBarcode(v.VoucherID);
            initDB();
        }
        private void DoCreateVoucherBarcode(string voucherID)
        {
            var qrCodeWriter = new BarcodeWriter();
            qrCodeWriter.Format = BarcodeFormat.QR_CODE;
            qrCodeWriter.Write(voucherID).Save(@"C:\Users\user\Desktop\TPA_Desktop\program_mvm\barcodePAth\" + voucherID + ".png");
        }



        public void DoDeleteVoucher()
        {
            if(_selectedRow != null)
            {
                Voucher v = mainAtt.Mydb.Vouchers.Where(a => a.VoucherID.Equals(_selectedRow.VoucherID)).FirstOrDefault();
                v.IsActive = 0;
                mainAtt.Mydb.SaveChanges();
                initDB();
            }else
            {
                System.Windows.MessageBox.Show("Must be select a row");
            }
        }
        public void DoUpdateVoucher()
        {
            if (_selectedRow != null)
            {
                Voucher v = mainAtt.Mydb.Vouchers.Where(a => a.VoucherID.Equals(_selectedRow.VoucherID)).FirstOrDefault();
                try
                {
                    v.VoucherDiscount = Convert.ToDecimal(VoucherDiscount);
                    if (v.VoucherDiscount > 100 || v.VoucherDiscount <= 0)
                    {
                        System.Windows.MessageBox.Show("Voucher Discount must be valid");

                        return;
                    }
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Voucher Discount must be valid");
                    return;
                }
                if (IsApplied.Equals(""))
                {
                    System.Windows.MessageBox.Show("The Voucher Is Applied Must be filled");
                    return;
                }
                if (IsApplied.Equals("NotApplied"))
                {
                    v.IsApplied = 0;
                }
                else
                {
                    v.IsApplied = 1;
                }
                mainAtt.Mydb.SaveChanges();
                initDB();
            }
            else
            {
                System.Windows.MessageBox.Show("Must be select a row");
            }
        }

        public string VoucherID
        {
            get { return _voucherID; }
            set { _voucherID = value;

                NotifyOfPropertyChange(() => VoucherID);
            }
        }

        private double _voucherDiscount;

        public double VoucherDiscount
        {
            get { return _voucherDiscount; }
            set { _voucherDiscount = value;

                NotifyOfPropertyChange(() => VoucherDiscount);
            }
        }

        private string _isApplied;

        public string IsApplied
        {
            get { return _isApplied; }
            set {
                _isApplied = value;
                NotifyOfPropertyChange(() => IsApplied);
            }
        }

        public Voucher FindVoucher(string id)
        {
            return mainAtt.Mydb.Vouchers.Where(v => v.VoucherID == id).FirstOrDefault();
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
