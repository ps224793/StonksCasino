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
using StonksCasino.classes.Main;
using StonksCasino.classes.Roulette;

namespace StonksCasino.Views.Roulette
{
    /// <summary>
    /// Interaction logic for RouletteWindow.xaml
    /// </summary>
    public partial class RouletteWindow : Window, INotifyPropertyChanged
    {
        private Bettingtable _bettingtable = new Bettingtable();


        int _betAmount;

        public Bettingtable MyBettingTable
        {
            get { return _bettingtable; }
            set { _bettingtable = value; OnPropertyChanged(); }
        }

        private Bet _myamount = new Bet();

        public Bet MyAmount
        {
            get { return _myamount; }
            set { _myamount = value; }
        }

        Random _random = new Random();

        bool _Spinning = false;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
        int _value = 0;
        int _Finalnumber;
        int _Tokens;
        int _valuedisplay;
        bool _canbet = true;
        bool _display = true;
        DispatcherTimer _timerbet = new DispatcherTimer();
        DispatcherTimer _timerdisplay = new DispatcherTimer();
        public User user { get; set; }

        public RouletteWindow(User user)
        {
            
            this.user = user;
            Account();
            DataContext = this;
            configTimer();

            InitializeComponent();
            

        }


        private void Account()
        {
            DataTable dataTable = Database.Accounts();
            _Tokens = (int)dataTable.Rows[0]["Token"];
            user.MyTokens = _Tokens;

        }

        private void Bibliotheek_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void configTimer()
        {
            _timerbet.Interval = TimeSpan.FromSeconds(1);
            _timerbet.Tick += _timerbet_Tick;
            _timerdisplay.Interval = TimeSpan.FromSeconds(1);
            _timerdisplay.Tick += _timerdisplay_Tick;
        }

        private void _timerdisplay_Tick(object sender, EventArgs e)
        {
            if (_valuedisplay == 3)
            {
                MyBettingTable.Resetbet();
                _valuedisplay = 0;
                _display = true;
                _timerdisplay.Stop();
            }
            _valuedisplay++;
        }

        private void _timerbet_Tick(object sender, EventArgs e)
        {
            if (_value == 3)
            {
                _canbet = false;
                _value = 0;
                _timerbet.Stop();
                
            }
            _value++;
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            _timerbet.Start();
            _Spinning = true;
            int[] _score = new int[] { 0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 20 };
            Angle = 0;
            Storyboard storyboard = new Storyboard();
            storyboard.Duration = new Duration(TimeSpan.FromSeconds(8.0));
            double angle = _random.Next(1800, 3600);
            DoubleAnimation rotateAnimation = new DoubleAnimation()
            {
                From = Angle,
                To = angle,
                Duration = storyboard.Duration,
                AccelerationRatio = 0.1,
                DecelerationRatio = 0.5
            };
            Angle += angle;
            Angle = Angle % 360;
            Storyboard.SetTarget(rotateAnimation, imWheel);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));


            storyboard.Children.Add(rotateAnimation);
            storyboard.Begin();



            //---------------------
            Angle2 = 0;
            int random1 = _random.Next(0, 36);
            Storyboard storyboard2 = new Storyboard();
            storyboard2.Completed += Storyboard2_Completed;
            storyboard2.Duration = new Duration(TimeSpan.FromSeconds(8.0));
            double angle2 = 9.72972973 * random1 + -3600 + Angle;
            DoubleAnimation rotateAnimation2 = new DoubleAnimation()
            {
                From = Angle2,
                To = angle2,
                Duration = storyboard2.Duration,
                AccelerationRatio = 0.4,
                DecelerationRatio = 0.2
            };
            Angle2 += angle2;
            Angle2 = Angle2 % 360;
            Storyboard.SetTarget(rotateAnimation2, imBal);
            Storyboard.SetTargetProperty(rotateAnimation2, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));


            storyboard2.Children.Add(rotateAnimation2);
            storyboard2.Begin(this);

            _Finalnumber = _score[random1];


        }

        private void Storyboard2_Completed(object sender, EventArgs e)
        {
            int totelwin = MyBettingTable.Checkwin(_Finalnumber);
            MyAmount.MyTotalinzet = 0;
            if (totelwin > 0)
            {
                MessageBox.Show("Gefeliciteerd u hebt € " + totelwin.ToString() + " Gewonnen");
                DataTable data = Database.Tokensadd(totelwin);

            }
            _display = false;
            _timerdisplay.Start();
            _canbet = true;
            Account();
            _Spinning = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_display == false)
            {
                MyBettingTable.Resetbet();
                _valuedisplay = 0;
                _display = true;
                _timerdisplay.Stop();
            }
            if (_canbet)
            {


                if (_betAmount > 0)
                {

                    if (_Tokens >= _betAmount)
                    {
                        MyAmount.Addtotal(_betAmount);
                        Button bt = sender as Button;
                        ((Bet)bt.Tag).SetBet(_betAmount);
                        DataTable data = Database.Tokensremove(_betAmount);
                        Account();
                    }
                    else
                    {
                        MessageBox.Show("U heeft niet genoeg tokens om in te zetten");
                    }
                }
                else
                {
                    MessageBox.Show("U kunt geen fiche van 0 inzetten");
                }
            }
            else
            {
                MessageBox.Show("U kunt nu niet meer inzetten. Wacht tot er een nummer is gevallen");
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_canbet)
            {
                Button bt = sender as Button;
                ((Bet)bt.Tag).PreviewBet();
                bool Chip = ((Bet)bt.Tag).Set;
                if (Chip == true)
                {
                    bt.ToolTip = ((Bet)bt.Tag).AmountLabel;
                }
                else
                {
                    bt.ToolTip = null;
                }
            }
           

        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
           
            
                Button bt = sender as Button;
                ((Bet)bt.Tag).dePreviewBet();
            
        }



        private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_canbet)
            {
                Button bt = sender as Button;
                int amount = ((Bet)bt.Tag).Amount;
                ((Bet)bt.Tag).DeleteBet();

                MyAmount.RemoveTotal(amount);
                Account();
            }
            else
            {
                MessageBox.Show("u kunt uw ingezetten fiches niet meer weg halen. Wacht tot er een nummer is gevallen");
            }
        }

        private void Fiche_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox text = sender as TextBox;
            int inttext = 0;


            try
            {
                inttext = int.Parse(text.Text);
            }
            catch (Exception)
            {


            }
            _betAmount = inttext;

            if (text.Text.Length < 4)
            {
                text.FontSize = 20;
            }
            if (text.Text.Length >= 4)
            {
                text.FontSize = 15;
            }
            if (text.Text.Length >= 5)
            {
                text.FontSize = 12;
            }
            if (text.Text.Length >= 6)
            {
                text.FontSize = 10;
            }




        }



        private void MaskNumericInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextIsNumeric(e.Text);
        }

        private void MaskNumericPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string input = (string)e.DataObject.GetData(typeof(string));
                if (!TextIsNumeric(input)) e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }

        private bool TextIsNumeric(string input)
        {
            return input.All(c => Char.IsDigit(c) || Char.IsControl(c));
        }

        private void plus_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MyAmount.Plusinzet();
        }

        private void min_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_betAmount > 0)
            {
                MyAmount.Mininzet();
            }

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (_Spinning)
            {
                if (MyAmount.MyTotalinzet > 0)
                {
                    MessageBoxResult spinning = MessageBox.Show("De roulette tafel is aan het draaien. Als u nu weggaat dan bent u uw ingezetten fiches kwijt", "Weet u zeker dat u wil weggaan?", MessageBoxButton.OKCancel);
                    if (spinning == MessageBoxResult.Cancel)
                    {
                        e.Cancel = true;
                    }


                }
                else
                {
                    if (MyAmount.MyTotalinzet > 0)
                    {
                        MessageBoxResult Leave = MessageBox.Show("U heeft geld ingezet. Als u nu weggaat worden uw ingezetten fiches teruggegeven", "Weet u zeker dat u wil weggaan?", MessageBoxButton.OKCancel);
                        if (Leave == MessageBoxResult.OK)
                        {
                            DataTable data = Database.Tokensadd(MyAmount.MyTotalinzet);
                            Account();
                        }
                        else
                        {
                            e.Cancel = true;
                        }



                    }
                }

            }
        }
    }
}
