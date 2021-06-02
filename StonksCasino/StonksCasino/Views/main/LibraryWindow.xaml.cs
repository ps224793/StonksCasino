using System;
using System.Collections.Generic;
using System.Data;
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
using StonksCasino.classes.Main;
using StonksCasino.Views.Roulette;
using StonksCasino.Views.blackjack;
using StonksCasino.Views.poker;
using StonksCasino.Views.slotmachine;
using StonksCasino.Views.horserace;
using StonksCasino.classes.Api;

namespace StonksCasino.Views.main
{
    /// <summary>
    /// Interaction logic for LibraryWindow.xaml
    /// </summary>
    public partial class LibraryWindow : Window
    {
        int _Tokens;

        public LibraryWindow()
        {
            Account();
            DataContext = this;
            InitializeComponent();
        }
        private async void Account()
        {
            bool Apicall = await ApiWrapper.GetUserInfo();

            if(!Apicall)
            {
                Application.Current.Shutdown();
            }
        }

        private void Roullete_click(object sender, RoutedEventArgs e)
        {
            Roulettegame();
        }

        private void ImgRoulette_Click(object sender, RoutedEventArgs e)
        {
            Roulettegame();
        }

        private void Roulettegame()
        {
            Account();
            RouletteWindow roulette = new RouletteWindow();
            this.Hide();
            roulette.Show();
      
        }

        private void Blackjack_click(object sender, RoutedEventArgs e)
        {
            Blackjackgame();
        }

        private void ImgBlackjack_Click(object sender, RoutedEventArgs e)
        {
            Blackjackgame();
        }

        private void Blackjackgame()
        {
            Account();
            BlackjackWindow blackjack = new BlackjackWindow();
            this.Hide();
            blackjack.Show();
           
        }

        private void Poker_click(object sender, RoutedEventArgs e)
        {
            Pokergame();
        }

        private void ImgPoker_Click(object sender, RoutedEventArgs e)
        {
            Pokergame();
        }

        private void Pokergame()
        {
            Account();
            PokerWindow roulette = new PokerWindow();
            this.Hide();
            roulette.Show();
           
        }

        private void SlotMachine_click(object sender, RoutedEventArgs e)
        {
            SlotMachinegame();
        }

        private void ImgSlotMachine_Click(object sender, RoutedEventArgs e)
        {
            SlotMachinegame();
        }

        private void SlotMachinegame()
        {
            Account();
            SlotmachineWindow roulette = new SlotmachineWindow();
            this.Hide();
            roulette.Show();
        }


        private void HorseRace_click(object sender, RoutedEventArgs e)
        {
            HorseRacegame();
        }

        private void ImgHorseRace_Click(object sender, RoutedEventArgs e)
        {
            HorseRacegame();
        }

        private void HorseRacegame()
        {
            Account();
            horseracewindow horserace = new horseracewindow();
            this.Hide();
            horserace.Show();
        }



        private void Window_Closed(object sender, EventArgs e)
        {
            ApiWrapper.Logout();
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

        private void Uitloggen_Click(object sender, RoutedEventArgs e)
        {
            ApiWrapper.Logout();
            User.Username = "";
            User.Tokens = 0;
            MainWindow window = new MainWindow();
        
            this.Close();
            window.Show();
        }
    }
}
