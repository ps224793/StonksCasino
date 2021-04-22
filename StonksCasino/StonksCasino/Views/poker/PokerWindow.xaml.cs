using System;
using System.Collections.Generic;
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
using StonksCasino.classes.poker;
using StonksCasino.Views.main;
using StonksCasino.classes.Main;

namespace StonksCasino.Views.poker
{
    /// <summary>
    /// Interaction logic for PokerWindow.xaml
    /// </summary>
    public partial class PokerWindow : Window
    {


        private PokerGame game;
        public PokerWindow()
        {
            game = new PokerGame();
            DataContext = game;
            InitializeComponent();
        }

        private void Call_Click(object sender, RoutedEventArgs e)
        {
            game.CalcHand();
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

        private void btnBibliotheek_Click(object sender, RoutedEventArgs e)
        {
            LibraryWindow library = new LibraryWindow();
            this.Hide();
            library.Show();
        }
    }
}
