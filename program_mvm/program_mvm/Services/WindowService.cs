using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace program_mvm
{
    public class WindowService
    {
        public void ShowView(object viewModel)
        {
            var win = new Window();
            win.Content = viewModel;
            win.Show();
        }


    }
}
