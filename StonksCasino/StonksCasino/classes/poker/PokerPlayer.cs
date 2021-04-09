using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using StonksCasino.classes.Main;

namespace StonksCasino.classes.poker
{
    class PokerPlayer
    {
        public ObservableCollection<Card> Hand { get; set; }

        public PokerPlayer(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                Hand.Add(card);
            }
        }

        private bool _folded;

        public bool Folded
        {
            get { return _folded; }
            set { _folded = value; }
        }

        private int _balance;

        public int Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        private int _bet;

        public int Bet
        {
            get { return _bet; }
            set { _bet = value; }
        }



    }
}
