﻿using System;
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
            set { _aantal = value; OnPropertyChanged(); }
        }

        public void GeefMelding()
        {
            MessageBox.Show($"Het aantal ingezette Tokens: { MyAantal }");
        }

        public void Dubbelen()
        {
            MyAantal = MyAantal * 2;
            MessageBox.Show($"Het aantal Tokens is verdubbeld naar: { MyAantal }");
        }
    }
}
