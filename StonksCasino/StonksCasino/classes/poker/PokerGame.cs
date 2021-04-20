using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StonksCasino.classes.Main;

namespace StonksCasino.classes.poker
{
    class PokerGame : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private PokerDeck deck = new PokerDeck();

        private List<PokerPlayer> _players;

        public List<PokerPlayer> Players
        {
            get { return _players; }
            set { _players = value; }
        }

        private ObservableCollection<Card> _table;

        public ObservableCollection<Card> MyTable
        {
            get { return _table; }
            set { _table = value; OnPropertyChanged(); }
        }

        public PokerGame()
        {
            Players = new List<PokerPlayer>();
            Players.Add(new PokerPlayer());
            SetPlayerHand(Players[0]);
            SetTable();
            //PokerHandCalculator.GetHandValue(_players[0].Hand.ToList(), _table.ToList());
        }

        public void CalcHand()
        {
            //List<Card> handje = new List<Card>();
            //handje.Add(new Card(enums.card.CardType.Hearts, enums.card.CardValue.Ace, enums.card.CardBackColor.Blue));
            //handje.Add(new Card(enums.card.CardType.Clubs, enums.card.CardValue.Two, enums.card.CardBackColor.Blue));

            //List<Card> tafeltje = new List<Card>();
            //tafeltje.Add(new Card(enums.card.CardType.Clubs, enums.card.CardValue.Three, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Clubs, enums.card.CardValue.Ten, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Spades, enums.card.CardValue.Jack, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Clubs, enums.card.CardValue.King, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Diamonds, enums.card.CardValue.Queen, enums.card.CardBackColor.Blue));

            PokerHandCalculator.GetHandValue(_players[0].Hand.ToList(), _table.ToList());
        }

        private void SetPlayerHand(PokerPlayer player)
        {
            List<Card> cards = new List<Card>();
            cards.Add(deck.DrawCard());
            cards.Add(deck.DrawCard());
            player.SetHand(cards);
        }

        private void SetTable()
        {
            ObservableCollection<Card> cards = new ObservableCollection<Card>();
            for (int i = 0; i < 5; i++)
            {
                Card card = deck.DrawCard();
                //card.Turned = true;
                cards.Add(card);
            }
            MyTable = cards;
        }

    }
}
