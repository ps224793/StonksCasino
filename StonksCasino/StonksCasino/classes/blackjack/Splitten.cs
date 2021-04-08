using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StonksCasino.classes.blackjack
{
    public class Splitten : PropertyChange
    {

        private int _PlayerSplit;

        public int MyPlayerSplit
        {
            get { return _PlayerSplit; }
            set { _PlayerSplit = value; }
        }

        private int _aantal;

        public int MyAantal
        {
            get { return _aantal; }
            set { _aantal = value; OnPropertyChanged(); }
        }

        public void Splitte()
        {
            MyAantal = MyAantal * 2;
            MessageBox.Show($"Het aantal Tokens is verdubbeld naar: { MyAantal } en de kaarten zijn gesplit!");
        }
    }
}
