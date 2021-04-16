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

        public ObservableCollection<Card> Hand { get; set;}

        public BlackjackPlayer()
        {

        }

        public void SetHand(List<Card> cards)
        {
            Hand = new ObservableCollection<Card>();

            foreach (Card card in cards)
            {
                Hand.Add(card);
            }
            OnPropertyChanged("Hand");
        }
    }
}
