using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm.ViewModels
{
    public class BillViewModel: Conductor<Object>
    {
        private MainAtt mainAtt;
        private List<DetailServiceTransaction> _detailServiceTransactions;

        public  List<DetailServiceTransaction> DetailServiceTransactions
        {
            get { return _detailServiceTransactions; }
            set { _detailServiceTransactions = value;
                NotifyOfPropertyChange(() => DetailServiceTransactions);
            }
        }
        private string htID;
        public BillViewModel(string htID)
        {
            this.htID = htID;
            mainAtt = MainAtt.getInstance();
            initDB();
        }
        public void initDB()
        {
            DetailServiceTransactions = mainAtt.Mydb.DetailServiceTransactions.Where(a => a.HeaderID.Equals(htID)).ToList();
        }


        public void DoDeleteServiceTranscation()
        {
            if(SelectedRow == null)
            {
                System.Windows.MessageBox.Show("No Selected Row");
                return;
            }

            DetailServiceTransaction dst = mainAtt.Mydb.DetailServiceTransactions.Where(a => a.DetailServiceID == SelectedRow.DetailServiceID).FirstOrDefault();
            mainAtt.Mydb.DetailServiceTransactions.Remove(dst);
            mainAtt.Mydb.SaveChanges();
            initDB();
        }

        private DetailServiceTransaction _selectedRow;

        public DetailServiceTransaction SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; }
        }

    }
}
