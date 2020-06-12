using Caliburn.Micro;
using program_mvm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm.ViewModels
{
    public class IncomeViewModel: Screen
    {

        private IncomeModel _data;

        public IncomeModel Data
        {
            get { return _data; }
            set { _data = value; }
        }


        public IncomeViewModel()
        {
            Data = new IncomeModel();
        }
    }
}
