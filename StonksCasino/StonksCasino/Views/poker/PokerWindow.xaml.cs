﻿using System;
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
using System.Data;
using StonksCasino.classes.Api;

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

        public string Username
        {
            get { return User.Username; }
        }

        public int Tokens
        {
            get { return User.Tokens; }
        }


        public PokerWindow()
        {
            Game = new PokerGame();
            DataContext = this;
            InitializeComponent();
            Game.sbSetup();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CardWidth = (int)one.ActualWidth;
        }

        private void Raise_Bet(object sender, RoutedEventArgs e)
        {
            Game.Raise(Game.Players[0]);
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
            if (Game.Players[0].RaiseBet > Game.LastRaise + (Game.TopBet - Game.Players[0].Bet))
            {
                Game.Players[0].RaiseBet--;
            }
        }

        private void btnFold_Click(object sender, RoutedEventArgs e)
        {
            Game.Fold(Game.Players[0]);
        }

        private void Call_Click(object sender, RoutedEventArgs e)
        {
            int MaxBet = Game.Players[0].Bet + Game.Players[0].Balance;
            if (MaxBet <= Game.TopBet)
            {
                Game.AllIn(Game.Players[0]);
            }
            else if (Game.Players[0].Bet < Game.TopBet)
            {
                Game.Call(Game.Players[0]);
            }
            else
            {
                Game.Check(Game.Players[0]);
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
            Game.TableTurn();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Game.TableRiver();
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

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Game.TableFlop();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            // Game.CalcHand();
            Game.showdown(Game.Players);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Game.firstBettingRound();
        }

        private void Uitloggen_Click(object sender, RoutedEventArgs e)
        {
            StonksCasino.Properties.Settings.Default.Username = "";
            StonksCasino.Properties.Settings.Default.Password = "";
            StonksCasino.Properties.Settings.Default.Save();
            ApiWrapper.Logout();
            User.Username = "";
            User.Tokens = 0;


            MainWindow window = new MainWindow();

            this.Close();
            window.Show();
        }
    }
}
