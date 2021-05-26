using StonksCasino.classes.Main;
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

        private PokerPlayer _player;

        public PokerPlayer Player
        {
            get { return _player; }
            set { _player = value; }
        }

        private int _handValue;

		public int HandValue
		{
			get { return _handValue; }
			set { _handValue = value; }
		}

		private List<Card> _hand;

		public List<Card> Hand
		{
			get { return _hand; }
			set { _hand = value; }
		}

		private List<Card> _highCards = new List<Card>();

		public List<Card> HighCards
		{
			get { return _highCards; }
			set { _highCards = value; }
		}

		public PokerHandValue(PokerHand pokerHand)
		{
			MyPokerHand = pokerHand;
		}

		public PokerHandValue(PokerHand pokerHand, int handValue, List<Card> hand, PokerPlayer player)
		{
			MyPokerHand = pokerHand;
			HandValue = handValue;
			Hand = hand;
			Player = player;
		}

		public PokerHandValue(PokerHand pokerHand, int handValue, List<Card> hand, List<Card> highCards, PokerPlayer player)
		{
			MyPokerHand = pokerHand;
			HandValue = handValue;
			Hand = hand;
			HighCards.AddRange(highCards);
			Player = player;
		}
	}
}
