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
using System.Windows.Threading;
using StonksCasino.classes.blackjack;
using StonksCasino.classes.Main;
using StonksCasino.Views.main;

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

   

        private CardBlackjack _cardturned;

        public CardBlackjack Mycardturned
        {
            get { return _cardturned; }
            set { _cardturned = value; OnPropertyChanged(); }
        }

        private BlackjackDeck deck = new BlackjackDeck();
        public User User { get; set; }

        private BlackJack _game;

        public BlackJack Game
        {
            get { return _game; }
            set { _game = value; OnPropertyChanged(); }
        }
        private Database _database = new Database();

        public Database MyDatabase
        {
            get { return _database; }
            set { _database = value; }
        }

        private Computers _player;

        public Computers Playergame
        {
            get { return _player; }
            set { _player = value; OnPropertyChanged(); }
        }

        private Computers _computer;

        public Computers ComputerGame
        {
            get { return _computer; }
            set { _computer = value; OnPropertyChanged(); }
        }

        private Computers _computer2;

        public Computers ComputerGame2
        {
            get { return _computer2; }
            set { _computer2 = value; OnPropertyChanged(); }
        }


        public User user { get; set; }
        int _Tokens;

        DispatcherTimer computertimer = new DispatcherTimer();

        public BlackjackWindow(User user)
        {
            BlackjackWindowRestart();          
            this.user = user;
            DataContext = this;
            Account();
            InitializeComponent();

            computertimer.Interval = TimeSpan.FromMilliseconds(1);
            computertimer.Tick += computertimer_Tick;
        }

        private void Account()
        {
            DataTable dataTable = Database.Accounts();
            _Tokens = (int)dataTable.Rows[0]["token"];
            user.MyTokens = _Tokens;

        }
        private bool Checkingelogd()
        {
            DataTable dataTable = Database.Accounts();
            long Time = (long)dataTable.Rows[0]["timestamp"];
            if (Time != Properties.Settings.Default.Timestamp)
            {
                MessageBox.Show("Er is door iemand anders ingelogd op het account waar u momenteel op speelt. Hierdoor wordt u uitgelogd");
                StonksCasino.Properties.Settings.Default.Username = "";
                StonksCasino.Properties.Settings.Default.Password = "";
                StonksCasino.Properties.Settings.Default.Save();
                _database.MyUsername = "";
                _database.MyPassword = "";


                MainWindow window = new MainWindow();

                this.Hide();
                window.Show();
                return false;

            }
            return true;
        }

        private void Bibliotheek_Click(object sender, EventArgs e)
        {
            
            LibraryWindow library = new LibraryWindow();
            this.Hide();
            library.Show();
           
        }

        private void BlackjackWindowRestart()
        {
            Game = new BlackJack();
            Game.Blackjackwindow();
            computertimer.Start();
        }

        private void computertimer_Tick(object sender, EventArgs e)
        {
            ComputerGame = new Computers();
            computertimer.Stop();
        }

        private Computers _computers = new Computers();

        public Computers MyComputers
        {
            get { return _computers; }
            set { _computers = value; OnPropertyChanged(); }
        }

        public void Deal_click(object sender, RoutedEventArgs e)
        {
           bool ingelogd = Checkingelogd();
            if (ingelogd)
            {
                int MyAantal = Game.MyAantal;
                if (MyAantal <= _Tokens)
                {
                    Game.Deal();
                    Account();
                }
                else
                {
                    MessageBox.Show("U heeft niet genoeg tokens om te kunnen spelen!");
                }
            }
          
        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            bool ingelogd = Checkingelogd();
            if (ingelogd)
            {
                Game.Hits();
                int Player = Game.Players[0].Score;

                if (Player > 21)
                {
                    Game.Stands();
                    ComputerGame.ComputerDeal(Player);
                    Endresult();
                }
            }
        }

        private void Dubbelen_Click(object sender, RoutedEventArgs e)
        {
            bool ingelogd = Checkingelogd();
            if (ingelogd)
            {
                int MyAantal = Game.MyAantal;
                if (MyAantal <= _Tokens)
                {
                    Game.Dubbelen();
                    Game.Hits();
                    Account();
                    int Player = Game.Players[0].Score;
                    if (Player > 21)
                    {
                        Game.Stands();
                        ComputerGame.ComputerDeal(Player);
                        Endresult();
                    }

                }
                else
                {
                    MessageBox.Show("U heeft niet genoeg tokens om te kunnen spelen!");
                }
            }
        }

        private void Splitten_Click(object sender, RoutedEventArgs e)
        {
            bool ingelogd = Checkingelogd();
            if (ingelogd)
            {
                Game.Splitte();
                Account();
            }
        }

        private void Stand_Click(object sender, RoutedEventArgs e)
        {
            bool ingelogd = Checkingelogd();
            if (ingelogd)
            {
                int Player = Game.Players[0].Score;
                Game.Stands();
                ComputerGame.ComputerDeal(Player);
                Endresult();
            }
        }

        private void Endresult()
        {            
            try
            {
                int bot = ComputerGame.Computer[0].ScoreC;
                int Player = Game.Players[0].Score;

                if (Player == 21 && bot < 21 )
                {
                    MessageBox.Show("Je hebt gewonnen!");
                    Game.Gamewin();
                }
                else if (bot > 21 && Player > 21)
                {
                    MessageBox.Show("Allebij verloren!");
                }
                else if (bot > 21 && Player <= 21)
                {
                    MessageBox.Show("Je hebt gewonnen!");
                    Game.Gamewin();
                }
                else if (Player > bot && Player <= 21)
                {
                    MessageBox.Show("Je hebt gewonnen!");
                    Game.Gamewin();
                }
                else if (Player > 21 && bot <= 21)
                {
                    MessageBox.Show("Je hebt verloren!");
                }
                else if (bot > Player && bot <= 21)
                {
                    MessageBox.Show("Je hebt verloren!");
                }
                else if (bot == Player)
                {
                    MessageBox.Show("Het is gelijkspel!");
                    Game.Gamedraw();
                }

                else
                {
                    MessageBox.Show("Fout!!!!");
                }
                Game.Gameclear();
                ComputerGame.GameclearComputer();
                Account();
                BlackjackWindowRestart();

            }
            catch
            {

            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
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
    }
}
