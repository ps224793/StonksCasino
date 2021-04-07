using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StonksCasino.classes.blackjack
{
    public class Token : PropertyChange
    {
        private int _aantal;

        public int MyAantal
        {
            get { return _aantal; }
            set { _aantal = value; }
        }

        public void GeefMelding()
        {
            MessageBox.Show($"Het aantal ingezette Tokens: { MyAantal }");
        }
    }
}
