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

        public Bettingtable MyBettingTable
        {
            get { return _bettingtable; }
            set { _bettingtable = value; OnPropertyChanged(); }
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
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            ((Bet)bt.Tag).SetBet(5);
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
    }
}
