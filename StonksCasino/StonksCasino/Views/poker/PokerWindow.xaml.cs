using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StonksCasino.classes.poker;
using StonksCasino.Views.main;
using StonksCasino.classes.Main;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Animation;

namespace StonksCasino.Views.poker
{
    /// <summary>
    /// Interaction logic for PokerWindow.xaml
    /// </summary>
    public partial class PokerWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private PokerGame _game;

        public PokerGame Game
        {
            get { return _game; }
            set { _game = value; OnPropertyChanged(); }
        }

        private int _cardWidth;

        public int CardWidth
        {
            get { return _cardWidth; }
            set { _cardWidth = value; OnPropertyChanged(); }
        }


        public PokerWindow()
        {
            Game = new PokerGame();
            DataContext = this;
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CardWidth = (int)one.ActualWidth;
        }

        private void Raise_Bet(object sender, RoutedEventArgs e)
        {
            if (Game.Players[0].Balance >= Game.Players[0].RaiseBet && Game.Players[0].RaiseBet > 0)
            {
                Game.Players[0].Bet += Game.Players[0].RaiseBet;
                Game.Players[0].Balance -= Game.Players[0].RaiseBet;
                Game.Players[0].RaiseBet = 0;
                if (Game.Players[0].Balance == 0)
                {
                    Game.Players[0].IsAllIn = true;
                }
            }
        }

        private void Higher_Raise(object sender, RoutedEventArgs e)
        {
            if (Game.Players[0].Balance > Game.Players[0].RaiseBet)
            {
                Game.Players[0].RaiseBet++;
            }
        }

        private void Lower_Raise(object sender, RoutedEventArgs e)
        {
            if (Game.Players[0].RaiseBet > 0)
            {
                Game.Players[0].RaiseBet--;
            }
        }

        private void btnFold_Click(object sender, RoutedEventArgs e)
        {
            Game.Players[0].Fold();
        }

        private void Call_Click(object sender, RoutedEventArgs e)
        {
            int MaxBet = Game.Players[0].Bet + Game.Players[0].Balance;
            if (MaxBet <= Game.TopBet)
            {
                Game.Players[0].AllIn();
            }
            else if (Game.Players[0].Bet < Game.TopBet)
            {
                Game.Players[0].Call(Game.TopBet);
            }
            else
            {
                Game.Players[0].Check();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.IsActive == true)
            {
                MessageBoxResult leaving = MessageBox.Show("Weet u zeker dat u de applicatie wil afsluiten", "Afsluiten", MessageBoxButton.YesNo);
                if (leaving == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
                else if (leaving == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }

            }
        }

        private void btnBibliotheek_Click(object sender, RoutedEventArgs e)
        {
            LibraryWindow library = new LibraryWindow();
            this.Hide();
            library.Show();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Storyboard board = (Storyboard)this.FindResource("sbTurn");
            board.Begin();

            await Task.Delay(300);
            Game.MyTable[3].Turned = false;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Storyboard board = (Storyboard)this.FindResource("sbRiver");
            board.Begin();

            await Task.Delay(300);
            Game.MyTable[4].Turned = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Storyboard player0 = (Storyboard)this.FindResource("sbPlayer0_In");
            player0.Begin();
            Storyboard player1 = (Storyboard)this.FindResource("sbPlayer1_In");
            player1.Begin();
            Storyboard player2 = (Storyboard)this.FindResource("sbPlayer2_In");
            player2.Begin();
            Storyboard player3 = (Storyboard)this.FindResource("sbPlayer3_In");
            player3.Begin();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Storyboard player0_out = (Storyboard)this.FindResource("sbPlayer0_Out");
            player0_out.Begin();
            Storyboard player1_out = (Storyboard)this.FindResource("sbPlayer1_Out");
            player1_out.Begin();
            Storyboard player2_out = (Storyboard)this.FindResource("sbPlayer2_Out");
            player2_out.Begin();
            Storyboard player3_out = (Storyboard)this.FindResource("sbPlayer3_Out");
            player3_out.Begin();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Storyboard board = (Storyboard)this.FindResource("sbTableIn");
            board.Begin();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Storyboard board = (Storyboard)this.FindResource("sbTableOut");
            board.Begin();
        }

        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Storyboard playerleft = (Storyboard)this.FindResource("sbPlayer1");
            playerleft.Begin();
            Storyboard playertop = (Storyboard)this.FindResource("sbPlayer2");
            playertop.Begin();
            Storyboard playerright = (Storyboard)this.FindResource("sbPlayer3");
            playerright.Begin();

            await Task.Delay(300);
            Game.Players[1].Hand[0].Turned = false;
            Game.Players[1].Hand[1].Turned = false;
            Game.Players[2].Hand[0].Turned = false;
            Game.Players[2].Hand[1].Turned = false;
            Game.Players[3].Hand[0].Turned = false;
            Game.Players[3].Hand[1].Turned = false;
        }

        private async void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Storyboard board = (Storyboard)this.FindResource("sbFlop");
            board.Begin();

            await Task.Delay(300);
            Game.MyTable[0].Turned = false;
            await Task.Delay(500);
            Game.MyTable[1].Turned = false;
            await Task.Delay(500);
            Game.MyTable[2].Turned = false;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Game.CalcHand();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Game.firstBettingRound();
        }
    }
}
