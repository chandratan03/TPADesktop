using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_mvm
{
    public class MainAtt
    {
        private static MainAtt instance = null;
        private MyDatabaseEntities2 _mydb;

        public MyDatabaseEntities2 Mydb
        {
            get { return _mydb; }
            set { _mydb = value; }
        }


        private MainAtt()
        {
            Mydb = new MyDatabaseEntities2();
        }

        public static MainAtt getInstance()
        {
            if(instance == null)
            {
                instance = new MainAtt();
            }
            
            return instance;
            
        }

    }
}
