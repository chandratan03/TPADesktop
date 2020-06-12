using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm.Models
{
    public class TaxModel: ObservableCollection<KeyValuePair<string, double>>
    {

        public void init()
        {
            MainAtt mainAtt = MainAtt.getInstance();
            var Journals = mainAtt.Mydb.Journals.ToList();
            int month = 0;
            for (int i = 0; i < Journals.Count; i++)
            {
                int temp = 0;
                if (month == 0)
                {
                    month = Convert.ToDateTime(Journals[i].JournalDate).Date.Month;
                    Add(new KeyValuePair<string, double>(Journals[i].JournalDate.ToString(), (double)Journals[i].credits));

                }
                temp = Convert.ToDateTime(Journals[i].JournalDate).Date.Month;
                if(temp!= month)
                {
                    month = temp;
                    Add(new KeyValuePair<string, double>(Journals[i].JournalDate.ToString(), (double)Journals[i].credits));
                }


            }
        }
        public TaxModel()
        {
            init();
        }
    }
}
