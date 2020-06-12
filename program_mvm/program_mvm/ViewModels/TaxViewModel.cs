using Caliburn.Micro;
using program_mvm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm.ViewModels
{
    public class TaxViewModel: Screen
    {

        private TaxModel _data;

        public TaxModel Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public TaxViewModel()
        {
            Data = new TaxModel();
        }


    }
}
