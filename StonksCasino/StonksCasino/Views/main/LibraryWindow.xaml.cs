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

namespace StonksCasino.Views.main
{
    /// <summary>
    /// Interaction logic for LibraryWindow.xaml
    /// </summary>
    public partial class LibraryWindow : Window
    {
        int _Tokens;
        public User user { get; set; }
  

        private Database _database = new Database();

        public Database MyDatabase
        {
            get { return _database; }
            set { _database = value; }
        }
        public LibraryWindow()
        {
            Account();
            DataContext = this;
            InitializeComponent();
        }
        private void Account()
        {
            DataTable dataTable = Database.Accounts();
            string Name = dataTable.Rows[0]["username"].ToString();
            int Tokens = (int)dataTable.Rows[0]["token"];
            user = new User(Name, Tokens);
            long Time = (long)dataTable.Rows[0]["timestamp"];
            if (Time != Properties.Settings.Default.Timestamp)
            {
                _database.MyLogout = false;
                Application.Current.Shutdown();
            }
        }
        private void accountrefresh()
        {
            DataTable dataTable = Database.Accounts();
            _Tokens = (int)dataTable.Rows[0]["token"];
            user.MyTokens = _Tokens;
            long Time = (long)dataTable.Rows[0]["timestamp"];
            if (Time != Properties.Settings.Default.Timestamp)
            {
               _database.MyLogout = false;
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
            accountrefresh();
            RouletteWindow roulette = new RouletteWindow(user);
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
            accountrefresh();
            BlackjackWindow roulette = new BlackjackWindow(user);
            this.Hide();
            roulette.Show();
           
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
            accountrefresh();
            PokerWindow roulette = new PokerWindow(user);
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
            accountrefresh();
            SlotmachineWindow roulette = new SlotmachineWindow(user);
            this.Hide();
            roulette.Show();
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            MyDatabase.Logout();
          
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
            MyDatabase.Logout();
            StonksCasino.Properties.Settings.Default.Username = "";
            StonksCasino.Properties.Settings.Default.Password = "";
            StonksCasino.Properties.Settings.Default.Save();
            _database.MyUsername = "";
            _database.MyPassword = "";
            MainWindow window = new MainWindow();
        
            this.Hide();
            window.Show();
        }
    }
}
