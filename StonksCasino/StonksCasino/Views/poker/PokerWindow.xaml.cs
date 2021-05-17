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
        private  PokerGame _game;

        public  PokerGame Game
        {
            get { return _game; }
            set { _game = value; OnPropertyChanged(); }
        }

        public PokerWindow()
        {
            Game = new PokerGame();
            DataContext = this;
            InitializeComponent();
        }

        private void Call_Click(object sender, RoutedEventArgs e)
        {
            Game.CalcHand();
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

        private void Higher_Raise(object sender, RoutedEventArgs e)
        {
            if (Game.Players[0].Balance > Game.Players[0].RaiseBet)
            {
                Game.Players[0].RaiseBet++;
            }
        }

        private void Lower_Raise(object sender, RoutedEventArgs e)
        {
            if(Game.Players[0].RaiseBet > 0)
            {
                Game.Players[0].RaiseBet--;
            }
        }

        private void Raise_Bet(object sender, RoutedEventArgs e)
        {
            if(Game.Players[0].Balance >= Game.Players[0].RaiseBet && Game.Players[0].RaiseBet > 0)
            {
                Game.Players[0].Bet += Game.Players[0].RaiseBet;
                Game.Players[0].Balance -= Game.Players[0].RaiseBet;
                Game.Players[0].RaiseBet = 0;
            }
        }
    }
}
