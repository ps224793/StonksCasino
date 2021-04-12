using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.blackjack
{
    public class Computers : PropertyChange
    {
        private int _Computervalue;

        public int MyComputervalue
        {
            get { return _Computervalue; }
            set { _Computervalue = value; OnPropertyChanged(); }
        }
    }
}
