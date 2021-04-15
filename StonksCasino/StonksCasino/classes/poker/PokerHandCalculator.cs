using StonksCasino.classes.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.poker
{
    class PokerHandCalculator
    {
        public static PokerHandValue GetHandValue(List<Card> hand, List<Card> table)
        {
            List<Card> cards = new List<Card>();
            cards.AddRange(hand);
            cards.AddRange(table);



            return null;
        }

        private bool RoyalFlush(List<Card> cards)
        {
            

            return false;
        }
    }
}
