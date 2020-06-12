using Caliburn.Micro;
using program_mvm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm.ViewModels
{
    public class ExpensesViewModel: Screen
    {
        private ExpensesModel _data;

        public ExpensesModel Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public ExpensesViewModel()
        {
            _data = new ExpensesModel();
        
        }
    }
}
