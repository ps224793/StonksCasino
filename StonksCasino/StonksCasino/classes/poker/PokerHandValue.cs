using StonksCasino.enums.poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StonksCasino.classes.poker
{
    class PokerHandValue
    {
		private PokerHand _pokerHand;

		public PokerHand MyPokerHand
		{
			get { return _pokerHand; }
			set { _pokerHand = value; }
		}

		private int _handValue;

		public int HandValue
		{
			get { return _handValue; }
			set { _handValue = value; }
		}

		public PokerHandValue(PokerHand pokerHand)
		{
			MyPokerHand = pokerHand;
		}

		public PokerHandValue(PokerHand pokerHand, int handValue)
		{
			MyPokerHand = pokerHand;
			HandValue = handValue;
		}
	}
}
