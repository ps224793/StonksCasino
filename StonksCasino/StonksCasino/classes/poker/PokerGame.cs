using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StonksCasino.classes.Main;

namespace StonksCasino.classes.poker
{
    class PokerGame
    {

        private PokerDeck deck = new PokerDeck();

        private List<PokerPlayer> _players;

        public List<PokerPlayer> Players
        {
            get { return _players; }
            set { _players = value; }
        }


        public PokerGame()
        {
            Players = new List<PokerPlayer>();
            Players.Add(new PokerPlayer());
            SetPlayerHand(Players[0]);
        }

        private void SetPlayerHand(PokerPlayer player)
        {
            List<Card> cards = new List<Card>();
            cards.Add(deck.DrawCard());
            cards.Add(deck.DrawCard());
            player.SetHand(cards);
        }


    }
}
