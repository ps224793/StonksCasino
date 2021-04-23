using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using StonksCasino.classes.Main;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StonksCasino.classes.poker
{
    public class PokerPlayer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    
        public ObservableCollection<Card> Hand { get; set; }
       

        private bool _folded;

        public bool Folded
        {
            get { return _folded; }
            set { _folded = value; }
        }

        private int _balance;

        public int Balance
        {
            get { return _balance; }
            set { _balance = value; OnPropertyChanged(); }
        }

        private int _bet;

        public int Bet
        {
            get { return _bet; }
            set { _bet = value; OnPropertyChanged(); }
        }

        public PokerPlayer()
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
