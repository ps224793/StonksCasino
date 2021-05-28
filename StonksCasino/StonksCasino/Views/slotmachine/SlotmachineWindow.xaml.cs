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
using System.Windows.Media.Animation;
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

        private int _beurt = 0;

        public int Beurt
        {
            get { return _beurt; }
            set { _beurt = value; OnPropertyChanged(); }
        }


        public SlotmachineWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            DataContext = this;
            Account();

            computertimer.Interval = TimeSpan.FromMilliseconds(1);
            computertimer.Tick += computertimer_Tick;

            Check();
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

        public void Check()
        {
            if (_Tokens < 100 || Beurt >= 10)
            {
                btVerhogen.IsEnabled = false;
            }
            else
            {
                btVerhogen.IsEnabled = true;
            }
            if (Beurt <= 0)
            {
                btVerlagen.IsEnabled = false;
            }
            else
            {
                btVerlagen.IsEnabled = true;
            }
        }

        public void Verhogen_Click(object sender, RoutedEventArgs e)
        {
            DataTable data = Database.Tokensremove(100);
            accountrefresh();
            Beurt++;
            Check();
        }
        private void Verlagen_Click(object sender, RoutedEventArgs e)
        {
            DataTable data = Database.Tokensadd(100);
            accountrefresh();
            Beurt--;
            Check();
        }

        private Slotmachine _slotmachine = new Slotmachine();

        public SlotmachineHendel Slotmachine
        {
            get { return _slotmachine; }
            set { _slotmachine = value; OnPropertyChanged(); }
        }







        private double _angle = 0;

        public double Angle
        {
            get { return _angle; }
            set { _angle = value; }
        }

        private double _angle2 = 0;

        public double Angle2
        {
            get { return _angle2; }
            set { _angle2 = value; }
        }

        DispatcherTimer _timerbet = new DispatcherTimer();

        bool _pressed = false;


        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (_pressed == false)
            {
                _timerbet.Start();
                _pressed = true;
                Angle = 0;
                Storyboard storyboard = new Storyboard();
                storyboard.Duration = new Duration(TimeSpan.FromSeconds(0.3));
                storyboard.Completed += Storyboard_Completed;
                double angle = 70;
                DoubleAnimation rotateAnimation = new DoubleAnimation()
                {
                    From = Angle,
                    To = angle,
                    Duration = storyboard.Duration,
                    AccelerationRatio = 0.5,
                    DecelerationRatio = 0.5
                };
                Angle += angle;
                Angle = Angle % 360;
                Storyboard.SetTarget(rotateAnimation, imgLever);
                Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));

                storyboard.Children.Add(rotateAnimation);
                storyboard.Begin();
            }
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {

            _timerbet.Start();
            Angle2 = 70;
            Storyboard storyboard2 = new Storyboard();
            storyboard2.Completed += Storyboard2_Completed;
            storyboard2.Duration = new Duration(TimeSpan.FromSeconds(1.0));

            double angle2 = 0;
            DoubleAnimation rotateAnimation = new DoubleAnimation()
            {
                From = Angle2,
                To = angle2,
                Duration = storyboard2.Duration,
                AccelerationRatio = 0.5,
                DecelerationRatio = 0.5
            };
            Angle += angle2;
            Angle = Angle % 360;
            Storyboard.SetTarget(rotateAnimation, imgLever);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));


            storyboard2.Children.Add(rotateAnimation);
            storyboard2.Begin();

            //--------------------------------------
        }
        private void Storyboard2_Completed(object sender, EventArgs e)
        {
            _pressed = false;
        }
    }
}
