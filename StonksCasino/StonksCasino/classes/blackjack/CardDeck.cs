using StonksCasino.classes.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;
using System.Security.Policy;

namespace StonksCasino.classes.blackjack
{
    class CardDeck
    {
        private List<Card> _shuffledCards;

        public CardDeck()
        {

        }

        public CardDeck(List<Card> cards)
        {
            AddCards(cards);
        }

        public void AddCards(List<Card> addCards)
        {
            _shuffledCards.AddRange(addCards.Shuffle());
        }

        public bool DrawCard(out Card card)
        {
            card = null;
            if(_shuffledCards.Count > 0)
            {
                card = _shuffledCards[0];
                _shuffledCards.RemoveAt(0);
                return true;
            }
            return false;
        }

    }
}
