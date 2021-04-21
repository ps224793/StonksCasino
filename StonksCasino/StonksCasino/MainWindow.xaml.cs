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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StonksCasino.Views.main;
using StonksCasino.classes.Main;

namespace StonksCasino
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string _pasword;
        private Database _database = new Database();

        public Database MyDatabase
        {
            get { return _database; }
            set { _database = value; }
        }



        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void Library_Click(object sender, RoutedEventArgs e)
        {
          bool window =  MyDatabase.Login(_pasword);
            if (window)
            {
                this.Close();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { _pasword = ((PasswordBox)sender).Password; }
        }
    }
}
