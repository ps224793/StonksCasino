using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StonksCasino.classes.Main;

namespace StonksCasino.classes.blackjack
{
    class BlackjackDeck : CardDeck
    {
        public BlackjackDeck()
        {
            AssembleDeck();
            AssembleDeck();
        }
    }
}
