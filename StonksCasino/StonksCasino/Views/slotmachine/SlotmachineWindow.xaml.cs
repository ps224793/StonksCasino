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
using StonksCasino.classes.Roulette;
using StonksCasino.classes.Slotmachine;
using StonksCasino.Views.main;

namespace StonksCasino.Views.slotmachine
{
    /// <summary>
    /// Interaction logic for SlotmachineWindow.xaml
    /// </summary>
    public partial class SlotmachineWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private Database _database = new Database();

        public Database MyDatabase
        {
            get { return _database; }
            set { _database = value; }
        }
        private void accountrefresh()
        {
            DataTable dataTable = Database.Accounts();
            _Tokens = (int)dataTable.Rows[0]["Token"];
            user.MyTokens = _Tokens;
            long Time = (long)dataTable.Rows[0]["Timestamp"];
            if (Time != Properties.Settings.Default.Timestamp)
            {
                _database.MyLogout = false;
                Application.Current.Shutdown();
            }
        }

        public User user { get; set; }
        public User User { get; set; }
        int _Tokens;

        DispatcherTimer computertimer = new DispatcherTimer();

        public int Beurt = 100;
        public int _betAmount;

        public SlotmachineWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            DataContext = this;
            Account();

            computertimer.Interval = TimeSpan.FromMilliseconds(1);
            computertimer.Tick += computertimer_Tick;
        }

        private void computertimer_Tick(object sender, EventArgs e)
        {
            computertimer.Stop();
        }

        private void Account()
        {
            DataTable dataTable = Database.Accounts();
            _Tokens = (int)dataTable.Rows[0]["Token"];
            user.MyTokens = _Tokens;
        }

        private void Bibliotheek_Click(object sender, EventArgs e)
        {

            LibraryWindow library = new LibraryWindow();
            this.Hide();
            library.Show();

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

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpTab();
        }
        private void HelpTab()
        {
            HelpWindow roulette = new HelpWindow();
            roulette.Show();
        }

        private Bet _myamount = new Bet();

        public Bet MyAmount
        {
            get { return _myamount; }
            set { _myamount = value; }
        }

        public void Verhogen_Click(object sender, RoutedEventArgs e)
        {
            DataTable data = Database.Tokensadd(100);
            accountrefresh();
        }

        private void Verlagen_Click(object sender, RoutedEventArgs e)
        {
            DataTable data = Database.Tokensremove(100);
            accountrefresh();
        }

        private Slotmachine _slotmachine;

        public Slotmachine Slotmachine
        {
            get { return _slotmachine; }
            set { _slotmachine = value; OnPropertyChanged(); }
        }
    }
}
