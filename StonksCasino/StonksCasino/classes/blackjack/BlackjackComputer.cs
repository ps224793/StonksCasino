using StonksCasino.classes.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.blackjack
{
    public class BlackjackComputer : PropertyChange
    {
        public ObservableCollection<Card> HandC { get; set; }

        public BlackjackComputer()
        {

        }

        public void SetHandC(List<Card> cards)
        {
            HandC = new ObservableCollection<Card>();

            foreach (Card card in cards)
            {
                HandC.Add(card);
            }
        }
    }
}
