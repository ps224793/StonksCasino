﻿using System;
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

        private int _currentPot = 0;

        public int CurrentPot
        {
            get
            {
                if (true)
                {
                    return MainPot;
                }
                else
                {
                    return SidePot;
                }
            }
            set
            {
                if (true)
                {
                    MainPot = value;
                }
                else
                {
                    SidePot = value;
                }
            }
        }

        private int _mainPot = 0;

        public int MainPot
        {
            get { return _mainPot; }
            set { _mainPot = value; }
        }

        private int _sidePot = 0;

        public int SidePot
        {
            get { return _mainPot; }
            set { _mainPot = value; }
        }

        private int _topBet = 0;

        public int TopBet
        {
            get { return _topBet; }
            set { _topBet = value; OnPropertyChanged("CallOrCheck"); OnPropertyChanged("RaiseOrAllIn"); }
        }

        public string RaiseOrAllIn
        {
            get
            {
                if (Players[0].Balance <= LastRaise + (TopBet - Players[0].Bet))
                {
                    return "All-in";
                }
                else
                {
                    return "Raise";
                }
            }
        }

        public string CallOrCheck
        {
            get
            {
                if (Players[0].Balance <= (TopBet - Players[0].Bet))
                {
                    return "All-in";
                }
                else if (Players[0].Bet == TopBet)
                {
                    return "Check";
                }
                else
                {
                    return "Call";
                }
            }
        }

        private int _lastRaise = 0;

        public int LastRaise
        {
            get { return _lastRaise; }
            set { _lastRaise = value; }
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
                Players[i].PlayerID = i;
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
            Players[0].Button = PokerButton.Dealer;
            Players[1].Button = PokerButton.SmallBlind;
            Players[2].Button = PokerButton.BigBlind;
            Players[3].Button = PokerButton.None;
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

        public void Raise(PokerPlayer player)
        {
            if (player.RaiseBet <= player.Balance && player.RaiseBet >= (LastRaise + (TopBet - player.Bet)))
            {
                int raiseBet;
                LastRaise = player.Raise(TopBet, out raiseBet);
                TopBet += raiseBet;
                CurrentPot += raiseBet;
            }
        }

        public void Fold(PokerPlayer player)
        {
            player.Fold();
        }

        public void Call(PokerPlayer player)
        {
            if (player.Balance >= (TopBet - player.Bet))
            {
                CurrentPot = player.Call(CurrentPot, TopBet);
            }
        }

        public void Check(PokerPlayer player)
        {
            player.Check();
        }

        public void AllIn(PokerPlayer player)
        {
            if (player.Balance >= (TopBet - player.Bet))
            {
                CurrentPot = player.AllIn(CurrentPot);
                // Switch from MainPot to SidePot
            }
        }

        public void firstBettingRound()
        {
            foreach (PokerPlayer player in Players)
            {
                switch (player.Button)
                {
                    case PokerButton.SmallBlind:
                        player.Bet += BlindsBet;
                        player.Balance -= BlindsBet;
                        MainPot += BlindsBet;
                        break;
                    case PokerButton.BigBlind:
                        player.Bet += BlindsBet * 2;
                        player.Balance -= (BlindsBet * 2);
                        MainPot += (BlindsBet * 2);
                        TopBet = (BlindsBet * 2);
                        LastRaise = TopBet;
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
                for (int handToCompare = 1; handToCompare < highestHands.Count; handToCompare++)
                {
                    for (int cardToCompare = 0; cardToCompare < 5; cardToCompare++)
                    {
                        if (highestHands[0].MyPokerHand == PokerHand.Straight ||
                            highestHands[0].MyPokerHand == PokerHand.StraightFlush)
                        {
                            if (highestHands[0].Hand[2].Value > highestHands[handToCompare].Hand[2].Value)
                            {
                                highestHands.RemoveAt(handToCompare);
                                handToCompare = 0;
                                break;
                            }
                            else if (highestHands[0].Hand[2].Value < highestHands[handToCompare].Hand[2].Value)
                            {
                                highestHands.RemoveAt(0);
                                handToCompare = 0;
                                break;
                            }
                        }
                        else
                        {
                            if (highestHands[0].Hand[cardToCompare].Value > highestHands[handToCompare].Hand[cardToCompare].Value)
                            {
                                highestHands.RemoveAt(handToCompare);
                                handToCompare = 0;
                                break;
                            }
                            else if (highestHands[0].Hand[cardToCompare].Value < highestHands[handToCompare].Hand[cardToCompare].Value)
                            {
                                highestHands.RemoveAt(0);
                                handToCompare = 0;
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
                    EndGame(highestHands[0]);
                }
            }
            else
            {
                EndGame(highestHands[0]);
            }
        }

        public void EndGame(PokerHandValue winningHand)
        {
            foreach (PokerPlayer player in Players)
            {
                player.Bet = 0;
            }
            Players[winningHand.PlayerID].Balance += CurrentPot;
            MessageBox.Show($"{Players[winningHand.PlayerID].PokerName} heeft gewonnen: \n" +
                            $"{winningHand.MyPokerHand}, " +
                            $"{winningHand.Hand[0].Type} {winningHand.Hand[0].Value}, " +
                            $"{winningHand.Hand[1].Type} {winningHand.Hand[1].Value}, " +
                            $"{winningHand.Hand[2].Type} {winningHand.Hand[2].Value}, " +
                            $"{winningHand.Hand[3].Type} {winningHand.Hand[3].Value}, " +
                            $"{winningHand.Hand[4].Type} {winningHand.Hand[4].Value}");
        }
    }
}
