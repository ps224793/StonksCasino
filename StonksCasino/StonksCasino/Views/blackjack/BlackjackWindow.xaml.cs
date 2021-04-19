using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
using StonksCasino.classes.blackjack;
using StonksCasino.classes.Main;

namespace StonksCasino.Views.blackjack
{
    /// <summary>
    /// Interaction logic for BlackjackWindow.xaml
    /// </summary>
    public partial class BlackjackWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private BlackjackDeck deck = new BlackjackDeck();
        public User User { get; set; }

        private BlackJack _game;

        public BlackJack Game
        {
            get { return _game; }
            set { _game = value; OnPropertyChanged(); }
        }

        private Computers computergame;

        int hitting = 0;

        public BlackjackWindow()
        {
            Game = new BlackJack();
            DataContext = this;
            Account();
            InitializeComponent();

            Game.Blackjackwindow();
        }

        private void Account()
        {
            DataTable dataTable = Database.Accounts();
            string Name = dataTable.Rows[0]["Gebruikersnaam"].ToString();
            int Tokens = (int)dataTable.Rows[0]["Token"];
            User = new User(Name, Tokens);
        }

        
        private Computers _computers = new Computers();

        public Computers MyComputers
        {
            get { return _computers; }
            set { _computers = value; OnPropertyChanged(); }
        }

        public void Deal_click(object sender, RoutedEventArgs e)
        {
            Game.Deal();
            //DataContext = this;
        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            if (hitting < 4)
            {
                Game.Hits();
                hitting += 1;
            }
            else
            {
                MessageBox.Show("Je kunt niet meer op hit klikken");
            }
        }

        private void Dubbelen_Click(object sender, RoutedEventArgs e)
        {
            Game.Dubbelen();
        }

        private void Splitten_Click(object sender, RoutedEventArgs e)
        {
            Game.Splitte();
        }

        private void Stand_Click(object sender, RoutedEventArgs e)
        {
            Game.Stands();

            computergame = new Computers();
            DataContext = computergame;

            try
            {
                int Player = int.Parse(tbPlayer.Text);
                int Bot = int.Parse(tbBot.Text);

                if (Player == 21 || Bot > 21 && Player <= 21)
                {
                    MessageBox.Show("Je hebt gewonnen!");
                }
                else if (Player > Bot && Player <= 21)
                {
                    MessageBox.Show("Je hebt gewonnen!");
                }
                else if (Bot == 21 || Player > 21 && Bot <= 21)
                {
                    MessageBox.Show("Je hebt verloren!");
                }
                else if (Bot > Player && Bot <= 21)
                {
                    MessageBox.Show("Je hebt verloren!");
                }
                else if (Bot == Player)
                {
                    MessageBox.Show("Het is gelijkspel!");
                }
                else if (Bot > 21 && Player > 21)
                {
                    MessageBox.Show("Allebij verloren!");
                }
                else
                {
                    MessageBox.Show("Fout!!!!");
                }
            }
            catch 
            {
               
            }            
        }
    }
}
