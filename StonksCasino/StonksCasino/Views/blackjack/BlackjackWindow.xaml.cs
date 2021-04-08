﻿using System;
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
using StonksCasino.classes.blackjack;
using StonksCasino.classes.Main;

namespace StonksCasino.Views.blackjack
{
    /// <summary>
    /// Interaction logic for BlackjackWindow.xaml
    /// </summary>
    public partial class BlackjackWindow : Window
    {
        public User User { get; set; }
        public BlackjackWindow()
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

        private Token _token = new Token();

        public Token MyToken
        {
            get { return _token; }
            set { _token = value; }
        }


        private void Melding_Click(object sender, RoutedEventArgs e)
        {
            MyToken.GeefMelding();
        }

        private void Stand_Click(object sender, RoutedEventArgs e)
        {
            int Player = int.Parse(tbPlayer.Text);
            int Bot = int.Parse(tbBot.Text);

            if(Player == 21 || Bot > 21 && Player <= 21)
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
