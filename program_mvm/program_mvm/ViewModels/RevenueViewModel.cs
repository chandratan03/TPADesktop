using Caliburn.Micro;
using program_mvm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm.ViewModels
{
    public class RevenueViewModel: Screen
    {
        private RevenueModel _data;

        public RevenueModel Data
        {
            get { return _data; }
            set { _data= value; }
        }
        public RevenueViewModel()
        {
            _data = new RevenueModel();
        }


    }
}
