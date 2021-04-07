﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.Roulette
{
    public class Bet : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; OnPropertyChanged(); }
        }

        private double _opacity;

        public double Opacity
        {
            get { return _opacity; }
            set { _opacity = value; OnPropertyChanged(); }
        }


        private int[] _values;

        public int[] Values
        {
            get { return _values; }
            set { _values = value; }
        }

        private bool _special;

        public bool Special
        {
            get { return _special; }
            set { _special = value; }
        }

        private int _amount = 0;

        public int Amount
        {
            get { return _amount; }
            set { _amount = value; OnPropertyChanged(); }
        }

        private string _amountLabel = "";

        public string AmountLabel
        {
            get { return _amountLabel; }
            set { _amountLabel = value; OnPropertyChanged(); }
        }

        private bool _set;

        public bool Set
        {
            get { return _set; }
            set { _set = value; OnPropertyChanged(); }
        }

        private int _multiplier = 36;

        public Bet(int[] values = null, bool special = false, int multiplier = 36)
        {
            Values = values;
            _multiplier = multiplier;
        }

        public int Checkwin(int value)
        {
            return 0;
        }

        public void SetBet(int amount)
        {
            AmountLabel = amount.ToString();
            Amount = amount;
            Opacity = 1;
            ImageUrl = "/Img/Roulette/Token.png";
            Set = true;
        }
        public void PreviewBet()
        {
            if (Set != true)
            {
                ImageUrl = "/Img/Roulette/Token.png";
                Opacity = 0.3;
            }
        }
        public void dePreviewBet()
        {
            if (Set != true)
            {
                ImageUrl = "";
              
            }
          

        }
        public void DeleteBet()
        {
            if (Set == true)
            {
                ImageUrl = "";
                AmountLabel = "";
                Set = false;

            }


        }


    }
}