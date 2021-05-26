using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using StonksCasino.classes.Main;
using StonksCasino.enums.poker;

namespace StonksCasino.classes.poker
{
    public class PokerGame : INotifyPropertyChanged
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

        private int _blindsBet = 5;

        public int BlindsBet
        {
            get { return _blindsBet; }
            set { _blindsBet = value; }
        }


        private int _topBet = 0;

        public int TopBet
        {
            get { return _topBet; }
            set { _topBet = value; }
        }

        private ObservableCollection<Card> _table;

        public ObservableCollection<Card> MyTable
        {
            get { return _table; }
            set { _table = value; OnPropertyChanged(); }
        }

        public PokerGame(User user)
        {
            Players = new List<PokerPlayer>();
            for (int i = 0; i < 4; i++)
            {
                Players.Add(new PokerPlayer());
                SetPlayerHand(Players[i]);
                Players[i].Balance = 500;
                Players[i].RaiseBet = 0;
                Players[i].Bet = 0;
                switch (i)
                {
                    case 0:
                        Players[i].PokerName = $"{user.MyName}";
                        break;
                    case 1:
                        Players[i].PokerName = "Gambletron 2000";
                        break;
                    case 2:
                        Players[i].PokerName = "The Pokernator";
                        break;
                    case 3:
                        Players[i].PokerName = "MyloBot";
                        break;
                }
            }
            Players[0].Button = enums.poker.PokerButton.Dealer;
            Players[1].Button = enums.poker.PokerButton.SmallBlind;
            Players[2].Button = enums.poker.PokerButton.BigBlind;
            Players[3].Button = enums.poker.PokerButton.None;
            foreach (Card card in Players[0].Hand)
            {
                card.Turned = false;
            }
            SetTable();
            //PokerHandCalculator.GetHandValue(_players[0].Hand.ToList(), _table.ToList());
        }

        public void CalcHand()
        {
            //List<Card> handje = new List<Card>();
            //handje.Add(new Card(enums.card.CardType.Clubs, enums.card.CardValue.Six, enums.card.CardBackColor.Blue));
            //handje.Add(new Card(enums.card.CardType.Clubs, enums.card.CardValue.Jack, enums.card.CardBackColor.Blue));

            //List<Card> tafeltje = new List<Card>();
            //tafeltje.Add(new Card(enums.card.CardType.Diamonds, enums.card.CardValue.Seven, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Clubs, enums.card.CardValue.Three, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Spades, enums.card.CardValue.Three, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Hearts, enums.card.CardValue.Three, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Hearts, enums.card.CardValue.Seven, enums.card.CardBackColor.Blue));
            //PokerHandCalculator.GetHandValue(tafeltje, handje);
            PokerHandCalculator.GetHandValue(_players[0], _table.ToList());
        }

        private void SetPlayerHand(PokerPlayer player)
        {
            List<Card> cards = new List<Card>();
            cards.Add(deck.DrawCard());
            cards.Add(deck.DrawCard());
            foreach (Card card in cards)
            {
                card.Turned = true;
            }
            player.SetHand(cards);
        }

        private void SetTable()
        {
            ObservableCollection<Card> cards = new ObservableCollection<Card>();
            for (int i = 0; i < 5; i++)
            {
                Card card = deck.DrawCard();
                card.Turned = true;
                cards.Add(card);
            }
            MyTable = cards;
        }

        public void firstBettingRound()
        {
            foreach (PokerPlayer player in Players)
            {
                switch (player.Button)
                {
                    case PokerButton.SmallBlind:
                        player.Bet = BlindsBet;
                        break;
                    case PokerButton.BigBlind:
                        player.Bet = BlindsBet * 2;
                        break;
                    case PokerButton.None:
                        if (player != Players[0])
                        {
                            // Execute alogrithm
                        }
                        break;
                }
            }
        }

        public void showdown(List<PokerPlayer> Players)
        {
            List<PokerHandValue> playerHands = new List<PokerHandValue>();
            foreach (PokerPlayer player in Players)
            {
                PokerHandValue result = PokerHandCalculator.GetHandValue(player, _table.ToList());
                playerHands.Add(result);
            }
            playerHands = playerHands.OrderBy(x => x.MyPokerHand).ToList();
            List<PokerHandValue> highestHands = new List<PokerHandValue>();
            foreach (PokerHandValue playerHand in playerHands)
            {
                if (playerHand.MyPokerHand == playerHands[0].MyPokerHand)
                {
                    highestHands.Add(playerHand);
                }
            }
            if (highestHands.Count > 1)
            {
                for (int x = 1; x < highestHands.Count; x++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (highestHands[0].MyPokerHand == PokerHand.Straight ||
                            highestHands[0].MyPokerHand == PokerHand.StraightFlush)
                        {
                            if (highestHands[0].Hand[2].Value > highestHands[x].Hand[2].Value)
                            {
                                highestHands.RemoveAt(x);
                                x = 0;
                                break;
                            }
                            else if (highestHands[0].Hand[2].Value < highestHands[x].Hand[2].Value)
                            {
                                highestHands.RemoveAt(0);
                                x = 0;
                                break;
                            }
                        }
                        else
                        {
                            if (highestHands[0].Hand[i].Value > highestHands[x].Hand[i].Value)
                            {
                                highestHands.RemoveAt(x);
                                x = 0;
                                break;
                            }
                            else if (highestHands[0].Hand[i].Value < highestHands[x].Hand[i].Value)
                            {
                                highestHands.RemoveAt(0);
                                x = 0;
                                break;
                            }
                        }
                    }
                }
                if (highestHands.Count > 1)
                {
                    MessageBox.Show("Gelijkspel");
                }
                else
                {
                    MessageBox.Show($"{highestHands[0].Player.PokerName} heeft gewonnen: \n" +
                                $"{highestHands[0].MyPokerHand}, " +
                                $"{highestHands[0].Hand[0].Type} {highestHands[0].Hand[0].Value}, " +
                                $"{highestHands[0].Hand[1].Type} {highestHands[0].Hand[1].Value}, " +
                                $"{highestHands[0].Hand[2].Type} {highestHands[0].Hand[2].Value}, " +
                                $"{highestHands[0].Hand[3].Type} {highestHands[0].Hand[3].Value}, " +
                                $"{highestHands[0].Hand[4].Type} {highestHands[0].Hand[4].Value}");
                }
            }
            else
            {
                MessageBox.Show($"{highestHands[0].Player.PokerName} heeft gewonnen: \n" +
                                $"{highestHands[0].MyPokerHand}, " +
                                $"{highestHands[0].Hand[0].Type} {highestHands[0].Hand[0].Value}, " +
                                $"{highestHands[0].Hand[1].Type} {highestHands[0].Hand[1].Value}, " +
                                $"{highestHands[0].Hand[2].Type} {highestHands[0].Hand[2].Value}, " +
                                $"{highestHands[0].Hand[3].Type} {highestHands[0].Hand[3].Value}, " +
                                $"{highestHands[0].Hand[4].Type} {highestHands[0].Hand[4].Value}");
            }
        }
    }
}
