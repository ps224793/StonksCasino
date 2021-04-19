using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using StonksCasino.classes.Main;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StonksCasino.classes.blackjack
{
    public class BlackjackPlayer : PropertyChange
    {

        public int Score
        {
            get { return GetScore(); }
        }


        private ObservableCollection<Card> _hand;

        public ObservableCollection<Card> Hand
        {
            get { return _hand; }
            set { _hand = value; OnPropertyChanged(); }
        }


        public BlackjackPlayer()
        {
            Hand = new ObservableCollection<Card>();
            OnPropertyChanged("Score");
        }

        public void SetHand(List<Card> cards)
        {
            Hand = new ObservableCollection<Card>();

            foreach (Card card in cards)
            {
                Hand.Add(card);
            }
            OnPropertyChanged("Score");
        }

        public void AddCard(Card card)
        {
            Hand.Add(card);
            OnPropertyChanged("Score");
        }

        private int GetScore()
        {
            int score = 0;

            foreach (Card card in Hand)
            {
                score += (int)card.Value + 1;
                
            }
            return score;
        }
    }
}
