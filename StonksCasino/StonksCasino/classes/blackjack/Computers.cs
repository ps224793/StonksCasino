using StonksCasino.classes.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StonksCasino.classes.blackjack
{
    public class Computers : PropertyChange
    {

        private int _Computervalue;

        public int MyComputervalue
        {
            get { return _Computervalue; }
            set { _Computervalue = value; OnPropertyChanged(); }
        }

        private BlackjackDeck deck = new BlackjackDeck();

        private List<BlackjackComputer> _computer;

        List<Card> cards = new List<Card>();

        public List<BlackjackComputer> Computer
        {
            get { return _computer; }
            set { _computer = value; OnPropertyChanged(); }
        }

        public Computers()
        {
            Computer = new List<BlackjackComputer>();
            Computer.Add(new BlackjackComputer());
            SetPlayerHand(Computer[0]);
        }

        public void SetPlayerHand(BlackjackComputer computer)
        {
            for (int i = 0; i < 4; i++)
            {
                if (MyComputervalue == 0)
                {
                    cards.Add(deck.DrawCard());
                    cards.Add(deck.DrawCard());
                    computer.SetHandC(cards);
                    MyComputervalue += 1;
                }
                else if (MyComputervalue < 18)
                {
                    cards.Add(deck.DrawCard());
                    computer.SetHandC(cards);
                    MyComputervalue += 18;
                }
                else
                {
                    //MessageBox.Show("De dealer is klaar met zijn kaarten uitleggen");
                }
            }
            
        }
    }
}
