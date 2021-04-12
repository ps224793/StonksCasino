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
            get { return _myamount ; }
            set { _myamount = value; }
        }



        Random _random = new Random();
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
        public User User { get; set; }
        public RouletteWindow()
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
            User = new User(Name, Tokens);
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {

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
            storyboard2.Duration = new Duration(TimeSpan.FromSeconds(8.0));
            double angle2 = 9.72972973 * random1 + 3600 + Angle;
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
            storyboard2.Begin();

            MessageBox.Show("het nummer is" + _score[random1]);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            ((Bet)bt.Tag).SetBet(_betAmount);
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button bt = sender as Button;
            
            ((Bet)bt.Tag).PreviewBet();
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button bt = sender as Button;
            ((Bet)bt.Tag).dePreviewBet();
        }

     

        private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Button bt = sender as Button;
            ((Bet)bt.Tag).DeleteBet();
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
    }
}
