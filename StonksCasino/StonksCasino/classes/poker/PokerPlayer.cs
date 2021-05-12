﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using StonksCasino.classes.Main;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StonksCasino.enums.poker;

namespace StonksCasino.classes.poker
{
    public class PokerPlayer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        
        /// <summary>
        /// Represents the cards this player has in their hand
        /// </summary>
        public ObservableCollection<Card> Hand { get; set; }

        private PokerButton _button;

        public PokerButton Button
        {
            get { return _button; }
            set { _button = value; OnPropertyChanged("BackURL"); OnPropertyChanged("ActiveURL"); }
        }

        public string ImageURL
        {
            get
            {
                switch (Button)
                {
                    case PokerButton.Dealer:
                        return "/Img/PokerButtons/Dealer.png";
                    case PokerButton.SmallBlind:
                        return "/Img/PokerButtons/SmallBlind.png";
                    case PokerButton.BigBlind:
                        return "/Img/PokerButtons/BigBlind.png";
                    default:
                        return null;
                }
            }
        }

        private bool _checked;

        /// <summary>
        /// Represents if this player is currently all-in
        /// </summary>
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        private bool _isAllIn;

        /// <summary>
        /// Represents if this player is currently all-in
        /// </summary>
        public bool IsAllIn
        {
            get { return _isAllIn; }
            set { _isAllIn = value; }
        }

        private bool _folded;

        /// <summary>
        /// Represents if this player has folded
        /// </summary>
        public bool Folded
        {
            get { return _folded; }
            set { _folded = value; }
        }

        private bool _busted;

        /// <summary>
        /// Represents if this player has no chips left
        /// </summary>
        public bool Busted
        {
            get { return _busted; }
            set { _busted = value; }
        }

        private int _balance;

        /// <summary>
        /// Represents the amount of chips this player has left
        /// </summary>
        public int Balance
        {
            get { return _balance; }
            set { _balance = value; OnPropertyChanged(); }
        }

        private int _bet;

        /// <summary>
        /// Represents the amount of chips this player has currently bet
        /// </summary>
        public int Bet
        {
            get { return _bet; }
            set { _bet = value; OnPropertyChanged(); }
        }

        public PokerPlayer()
        {

        }

        /// <summary>
        /// Sets this player's hand using the cards given
        /// </summary>
        /// <param name="cards">A list of cards to add to this player's hand</param>
        public void SetHand(List<Card> cards)
        {
            Hand = new ObservableCollection<Card>();

            foreach (Card card in cards)
            {
                Hand.Add(card);
            }
            OnPropertyChanged("Hand");
        }

        /// <summary>
        /// Raises this player's bet with the amount given
        /// </summary>
        /// <param name="amount">How much this player's bet must be raised</param>
        public void Raise(int amount)
        {
            Balance -= amount;
            Bet += amount;
            // End of this player's turn
        }

        /// <summary>
        /// Raises this player's bet to match the highest bet on the table
        /// </summary>
        /// <param name="topBet">The current highest bet on the table</param>
        public void Call(int topBet)
        {
            Balance -= Math.Abs(topBet - Bet);
            Bet = topBet;
            // End of this player's turn
        }

        /// <summary>
        /// Simply ends this player's turn without raising their bet
        /// </summary>
        public void Check()
        {
            // End of this player's turn
        }

        /// <summary>
        /// Raises this player's bet with all their remaining balance
        /// </summary>
        public void AllIn()
        {
            Bet += Balance;
            Balance = 0;
            IsAllIn = true;
            // End of this player's turn
        }

        /// <summary>
        /// <para>
        /// Folds this player removing them from betting and forfeits their chance to win the pot
        /// <br>Any chips they have already bet remain in the pot</br>
        /// </para> 
        /// </summary>
        public void Fold()
        {
            Folded = true;
            // End of this player's turn
        }

    }
}
