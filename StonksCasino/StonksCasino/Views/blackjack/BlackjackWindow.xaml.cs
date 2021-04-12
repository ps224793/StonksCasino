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
    public partial class BlackjackWindow : Window
    {

        private BlackjackDeck deck = new BlackjackDeck();
        public User User { get; set; }
        public BlackjackWindow()
        {
            Account();
            DataContext = this;
            InitializeComponent();

            btSplitten.IsEnabled = false;
            btDubbel.IsEnabled = false;
            btStand.IsEnabled = false;
            btHit.IsEnabled = false;
        }
        private void Account()
        {
            DataTable dataTable = Database.Accounts();
            string Name = dataTable.Rows[0]["Gebruikersnaam"].ToString();
            int Tokens = (int)dataTable.Rows[0]["Token"];
            User = new User(Name, Tokens);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private BlackJack _token = new BlackJack();

        public BlackJack MyToken
        {
            get { return _token; }
            set { _token = value; OnPropertyChanged(); }
        }

        private Computers _computers = new Computers();

        public Computers MyComputers
        {
            get { return _computers; }
            set { _computers = value; OnPropertyChanged(); }
        }

        public void Melding_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tokeninzetten = int.Parse(tokeninzet.Text);

                if (tokeninzetten > 0)
                {
                    MyToken.GeefMelding();

                    tokeninzet.IsEnabled = false;
                    btDubbel.IsEnabled = true;
                    btSplitten.IsEnabled = true;
                    btStand.IsEnabled = true;
                    btHit.IsEnabled = true;
                    btDeal.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("U moet minimaal 1 token inzetten om te kunnen spelen!");
                }
            }

            catch
            {
                MessageBox.Show("U moet minimaal 1 token inzetten om te kunnen spelen!");
            }
        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            btHit.IsEnabled = false;
        }

        private void Dubbelen_Click(object sender, RoutedEventArgs e)
        {
            MyToken.Dubbelen();
            btDubbel.IsEnabled = false;
        }

        private void Splitten_Click(object sender, RoutedEventArgs e)
        {
            MyToken.Splitte();
            btSplitten.IsEnabled = false;
        }

        private void Stand_Click(object sender, RoutedEventArgs e)
        {
            int Player = int.Parse(tbPlayer.Text);
            int Bot = int.Parse(tbBot.Text);

            btStand.IsEnabled = false;
            btDeal.IsEnabled = true;
            tokeninzet.IsEnabled = true;

            if (Player == 21 || Bot > 21 && Player <= 21)
            {
                MessageBox.Show("Je hebt gewonnen!");
            }
            else if(Player > Bot && Player <= 21)
            {
                MessageBox.Show("Je hebt gewonnen!");
            }
            else if(Bot == 21 || Player > 21 && Bot <= 21)
            {
                MessageBox.Show("Je hebt verloren!");
            }
            else if(Bot > Player && Bot <= 21)
            {
                MessageBox.Show("Je hebt verloren!");
            }
            else if(Bot == Player)
            {
                MessageBox.Show("Het is gelijkspel!");
            }
            else if(Bot > 21 && Player > 21)
            {
                MessageBox.Show("Allebij verloren!");
            }
            else
            {
                MessageBox.Show("Fout!!!!");
            }
        }
    }
}
