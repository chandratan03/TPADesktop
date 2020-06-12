using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm.Models
{
    public class ExpensesModel: ObservableCollection<KeyValuePair<string, double>>
    {
        public void init()
        {
            MainAtt mainAtt = MainAtt.getInstance();
            var Journals = mainAtt.Mydb.Journals.ToList();
            for(int i=0; i<Journals.Count; i++)
            {
                Add(new KeyValuePair<string, double>(Journals[i].JournalDate.ToString() ,(double)Journals[i].credits));

            }
        }
        public ExpensesModel()
        {
            init();
        }
    }
}
