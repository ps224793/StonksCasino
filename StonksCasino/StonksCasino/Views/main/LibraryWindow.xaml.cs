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

namespace StonksCasino.Views.main
{
    /// <summary>
    /// Interaction logic for LibraryWindow.xaml
    /// </summary>
    public partial class LibraryWindow : Window
    {
        public User user { get; set; }
        public LibraryWindow()
        {
            Account();
            DataContext = this;
            InitializeComponent();
        }
        private void Account()
        {
            DataTable dataTable = Database.Accounts();
            string Name = dataTable.Rows[0]["Gebruikersnaam"].ToString();
            int Tokens = (int)dataTable.Rows[0]["Token"];
            user = new User(Name, Tokens);
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

            RouletteWindow roulette = new RouletteWindow(user);
            this.Hide();
            roulette.ShowDialog();
            this.Show();
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
            BlackjackWindow roulette = new BlackjackWindow(user);
            this.Hide();
            roulette.ShowDialog();
            this.Show();
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
            PokerWindow roulette = new PokerWindow();
            this.Hide();
            roulette.ShowDialog();
            this.Show();
        }
    }
}
