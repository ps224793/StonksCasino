﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StonksCasino.enums.card;


namespace StonksCasino.classes.Main
{
    class Card : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private CardType _type;

		public CardType Type
		{
			get { return _type; }
			set { _type = value; OnPropertyChanged("ImageURL"); }
		}

		private CardValue _value;

		public CardValue Value
		{
			get { return _value; }
			set { _value = value; OnPropertyChanged("ImageURL"); }
		}

		private CardBackColor _backColor;

		public CardBackColor BackColor
		{
			get { return _backColor; }
			set { _backColor = value; OnPropertyChanged("BackURL"); }
		}


		public string ImageURL 
		{
			get { return $"/Img/Cards/{Value}{Type}.png"; }
		}

		public string BackURL 
		{
			get 
			{ 
				if(BackColor == CardBackColor.Blue)
				{
					return "/Img/Cards/BackBlue.png";
				}
				else
				{
					return "/Img/Cards/BackRed.png";
				}
			}
		}

	}
}
