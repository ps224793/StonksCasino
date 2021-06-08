using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using StonksCasino.classes.Main;
using StonksCasino.enums.poker;
using StonksCasino.Views.poker;

namespace StonksCasino.classes.poker
{
    public class PokerGame : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private PokerDeck deck = new PokerDeck();

        private List<PokerPlayer> _players;

        public List<PokerPlayer> Players
        {
            get { return _players; }
            set { _players = value; }
        }

        private int _blindsBet = 5;

        public int BlindsBet
        {
            get { return _blindsBet; }
            set { _blindsBet = value; }
        }

        private int _currentPot = 0;

        public int CurrentPot
        {
            get
            {
                if (true)
                {
                    return MainPot;
                }
                else
                {
                    return SidePot1;
                }
            }
            set
            {
                if (true)
                {
                    MainPot = value;
                }
                else
                {
                    SidePot1 = value;
                }
            }
        }

        private int _mainPot = 0;

        public int MainPot
        {
            get { return _mainPot; }
            set { _mainPot = value; }
        }

        private int _sidePot1 = 0;

        public int SidePot1
        {
            get { return _sidePot1; }
            set { _sidePot1 = value; }
        }

        private int _sidePot2 = 0;

        public int SidePot2
        {
            get { return _sidePot2; }
            set { _sidePot2 = value; }
        }

        private int _topBet = 0;

        public int TopBet
        {
            get { return _topBet; }
            set { _topBet = value; OnPropertyChanged("CallOrCheck"); OnPropertyChanged("RaiseOrAllIn"); }
        }

        public string RaiseOrAllIn
        {
            get
            {
                if (Players[0].Balance <= LastRaise + (TopBet - Players[0].Bet))
                {
                    return "All-in";
                }
                else
                {
                    return "Raise";
                }
            }
        }

        public string CallOrCheck
        {
            get
            {
                if (Players[0].Balance <= (TopBet - Players[0].Bet))
                {
                    return "All-in";
                }
                else if (Players[0].Bet == TopBet)
                {
                    return "Check";
                }
                else
                {
                    return "Call";
                }
            }
        }

        private int _lastRaise = 0;

        public int LastRaise
        {
            get { return _lastRaise; }
            set { _lastRaise = value; }
        }

        private string _gameState;

        public string GameState
        {
            get { return _gameState; }
            set { _gameState = value; }
        }



        private ObservableCollection<Card> _table;

        public ObservableCollection<Card> MyTable
        {
            get { return _table; }
            set { _table = value; OnPropertyChanged(); }
        }

        private Storyboard _boardOut;

        public Storyboard BoardOut
        {
            get { return _boardOut; }
            set { _boardOut = value; }
        }


        public PokerGame()
        {
            Players = new List<PokerPlayer>();
            for (int i = 0; i < 4; i++)
            {
                Players.Add(new PokerPlayer());
                Players[i].Balance = 500;
                Players[i].RaiseBet = 0;
                Players[i].Bet = 0;
                Players[i].PlayerID = i;
                switch (i)
                {
                    case 0:
                        Players[i].PokerName = $"{User.Username}";
                        break;
                    case 1:
                        Players[i].PokerName = "Gambletron 2000";
                        break;
                    case 2:
                        Players[i].PokerName = "The Pokernator";
                        break;
                    case 3:
                        Players[i].PokerName = "MyloBot";
                        break;
                }
            }
            Players[0].Button = PokerButton.Dealer;
            Players[1].Button = PokerButton.SmallBlind;
            Players[2].Button = PokerButton.BigBlind;
            Players[3].Button = PokerButton.None;
            //PokerHandCalculator.GetHandValue(_players[0].Hand.ToList(), _table.ToList());
        }

        public void CalcHand()
        {
            //List<Card> handje = new List<Card>();
            //handje.Add(new Card(enums.card.CardType.Clubs, enums.card.CardValue.Six, enums.card.CardBackColor.Blue));
            //handje.Add(new Card(enums.card.CardType.Clubs, enums.card.CardValue.Jack, enums.card.CardBackColor.Blue));

            //List<Card> tafeltje = new List<Card>();
            //tafeltje.Add(new Card(enums.card.CardType.Diamonds, enums.card.CardValue.Seven, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Clubs, enums.card.CardValue.Three, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Spades, enums.card.CardValue.Three, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Hearts, enums.card.CardValue.Three, enums.card.CardBackColor.Blue));
            //tafeltje.Add(new Card(enums.card.CardType.Hearts, enums.card.CardValue.Seven, enums.card.CardBackColor.Blue));
            //PokerHandCalculator.GetHandValue(tafeltje, handje);
            PokerHandCalculator.GetHandValue(_players[0], _table.ToList());
        }

        private void SetPlayerHand(PokerPlayer player)
        {
            List<Card> cards = new List<Card>();
            cards.Add(deck.DrawCard());
            cards.Add(deck.DrawCard());
            foreach (Card card in cards)
            {
                if (player.PlayerID != 0)
                {
                    card.Turned = true;
                }
            }
            player.SetHand(cards);
        }

        public void Raise(PokerPlayer currentPlayer)
        {
            if (currentPlayer.RaiseBet <= currentPlayer.Balance && currentPlayer.RaiseBet >= (LastRaise + (TopBet - currentPlayer.Bet)))
            {
                int raiseBet;
                LastRaise = currentPlayer.Raise(TopBet, out raiseBet);
                TopBet += raiseBet;
                CurrentPot += raiseBet;
            }
            resetCheckedPlayers(currentPlayer);
            WagerRound(currentPlayer);
        }

        public void Fold(PokerPlayer player)
        {
            player.Fold();
        }

        public void Call(PokerPlayer player)
        {
            if (player.Balance >= (TopBet - player.Bet))
            {
                CurrentPot = player.Call(CurrentPot, TopBet);
            }
        }

        public void Check(PokerPlayer player)
        {
            player.Check();
        }

        public void AllIn(PokerPlayer player)
        {
            if (player.Balance >= (TopBet - player.Bet))
            {
                CurrentPot = player.AllIn(CurrentPot);
                // Switch from MainPot to SidePot
            }
        }

        public void StartGame()
        {
            deck = new PokerDeck();
            GameState = "pre-Flop";
            PlaceBlinds();
            DealHoleCards();
            SetTable();
            foreach (PokerPlayer player in Players)
            {
                if (player.Button == PokerButton.None)
                {
                    WagerRound(player);
                }
            }
        }

        private void PlaceBlinds()
        {
            foreach (PokerPlayer player in Players)
            {
                switch (player.Button)
                {
                    case PokerButton.SmallBlind:
                        player.Bet += BlindsBet;
                        player.Balance -= BlindsBet;
                        MainPot += BlindsBet;
                        break;
                    case PokerButton.BigBlind:
                        player.Bet += BlindsBet * 2;
                        player.Balance -= (BlindsBet * 2);
                        MainPot += (BlindsBet * 2);
                        TopBet = (BlindsBet * 2);
                        LastRaise = TopBet;
                        break;
                    default:
                        break;
                }
            }
        }

        private void DealHoleCards()
        {
            foreach (PokerPlayer player in Players)
            {
                SetPlayerHand(player);
            }
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    Storyboard player0_in = (Storyboard)(window as PokerWindow).FindResource("sbPlayer0_In");
                    player0_in.Begin();
                    Storyboard player1_in = (Storyboard)(window as PokerWindow).FindResource("sbPlayer1_In");
                    player1_in.Begin();
                    Storyboard player2_in = (Storyboard)(window as PokerWindow).FindResource("sbPlayer2_In");
                    player2_in.Begin();
                    Storyboard player3_in = (Storyboard)(window as PokerWindow).FindResource("sbPlayer3_In");
                    player3_in.Begin();
                }
            }
        }

        public void WagerRound(PokerPlayer player)
        {
            int i;
            int startingPlayer = player.PlayerID;
            for (i = 0; i < Players.Count; i++)
            {
                int currentPlayer = (i + startingPlayer) % Players.Count;
                if (Players[currentPlayer].Busted != true && Players[currentPlayer].Checked != true && Players[currentPlayer].IsAllIn != true && Players[currentPlayer].Folded != true)
                {
                    if (Players[currentPlayer].PlayerID != 0)
                    {
                        MessageBox.Show($"{Players[currentPlayer].PokerName} is aan de beurt");
                        if (Players[currentPlayer].Bet == TopBet)
                        {
                            Check(Players[currentPlayer]);
                        }
                        else
                        {
                            Call(Players[currentPlayer]);
                        }
                        // Execute algorithm
                    }
                    else
                    {
                        MessageBox.Show($"{Players[currentPlayer].PokerName} is aan de beurt");
                        break;
                    }
                }
            }
            if (i == Players.Count)
            {
                switch (GameState)
                {
                    case "pre-Flop":
                        PlaceFlop();
                        break;
                    case "Flop":
                        PlaceTurn();
                        break;
                    case "Turn":
                        PlaceRiver();
                        break;
                    case "River":
                        showdown();
                        break;
                }
            }
        }

        private void SetTable()
        {
            ObservableCollection<Card> cards = new ObservableCollection<Card>();
            for (int i = 0; i < 5; i++)
            {
                Card card = deck.DrawCard();
                card.Turned = true;
                cards.Add(card);
            }
            MyTable = cards;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    Storyboard board = (Storyboard)(window as PokerWindow).FindResource("sbTableIn");
                    board.Begin();
                    (window as PokerWindow).SetCardWidth();
                }
            }
        }

        private void resetCheckedPlayers(PokerPlayer currentPlayer)
        {
            foreach (PokerPlayer player in Players)
            {
                if (currentPlayer != null)
                {
                    if (player.Busted != true && player.IsAllIn != true && player.Folded != true && player.PlayerID != currentPlayer.PlayerID)
                    {
                        player.Checked = false;
                    }
                }
                else
                {
                    if (player.Busted != true && player.IsAllIn != true && player.Folded != true)
                    {
                        player.Checked = false;
                    }
                }
            }
        }

        private void newWagerRound()
        {
            foreach (PokerPlayer player in Players)
            {
                if (player.Button == PokerButton.None && GameState == "pre-Flop")
                {
                    WagerRound(player);
                }
                else if (player.Button == PokerButton.SmallBlind)
                {
                    WagerRound(player);
                }
            }
        }

        private async void PlaceFlop()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    Storyboard board = (Storyboard)(window as PokerWindow).FindResource("sbFlop");
                    board.Begin();

                    await Task.Delay(300);
                    MyTable[0].Turned = false;
                    await Task.Delay(500);
                    MyTable[1].Turned = false;
                    await Task.Delay(500);
                    MyTable[2].Turned = false;

                }
            }
            GameState = "Flop";
            resetCheckedPlayers(null);
            newWagerRound();
        }

        public async void PlaceTurn()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    Storyboard board = (Storyboard)(window as PokerWindow).FindResource("sbTurn");
                    board.Begin();

                    await Task.Delay(300);
                    MyTable[3].Turned = false;
                }
            }
            GameState = "Turn";
            resetCheckedPlayers(null);
            newWagerRound();
        }

        public async void PlaceRiver()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    Storyboard board = (Storyboard)(window as PokerWindow).FindResource("sbRiver");
                    board.Begin();

                    await Task.Delay(300);
                    MyTable[4].Turned = false;
                }
            }
            GameState = "River";
            resetCheckedPlayers(null);
            newWagerRound();
        }

        public void showdown()
        {
            List<PokerHandValue> playerHands = new List<PokerHandValue>();
            foreach (PokerPlayer player in Players)
            {
                PokerHandValue result = PokerHandCalculator.GetHandValue(player, _table.ToList());
                playerHands.Add(result);
            }
            playerHands = playerHands.OrderBy(x => x.MyPokerHand).ToList();
            List<PokerPlayer> activePlayers = new List<PokerPlayer>();
            List<PokerHandValue> highestHands = new List<PokerHandValue>();
            foreach (PokerHandValue playerHand in playerHands)
            {
                if (playerHand.MyPokerHand == playerHands[0].MyPokerHand)
                {
                    highestHands.Add(playerHand);
                    activePlayers.Add(Players[playerHand.PlayerID]);
                }
            }
            showCards(activePlayers);
            if (highestHands.Count > 1)
            {
                for (int handToCompare = 1; handToCompare < highestHands.Count; handToCompare++)
                {
                    for (int cardToCompare = 0; cardToCompare < 5; cardToCompare++)
                    {
                        if (highestHands[0].MyPokerHand == PokerHand.Straight ||
                            highestHands[0].MyPokerHand == PokerHand.StraightFlush)
                        {
                            if (highestHands[0].Hand[2].Value > highestHands[handToCompare].Hand[2].Value)
                            {
                                highestHands.RemoveAt(handToCompare);
                                handToCompare = 0;
                                break;
                            }
                            else if (highestHands[0].Hand[2].Value < highestHands[handToCompare].Hand[2].Value)
                            {
                                highestHands.RemoveAt(0);
                                handToCompare = 0;
                                break;
                            }
                        }
                        else
                        {
                            if (highestHands[0].Hand[cardToCompare].Value > highestHands[handToCompare].Hand[cardToCompare].Value)
                            {
                                highestHands.RemoveAt(handToCompare);
                                handToCompare = 0;
                                break;
                            }
                            else if (highestHands[0].Hand[cardToCompare].Value < highestHands[handToCompare].Hand[cardToCompare].Value)
                            {
                                highestHands.RemoveAt(0);
                                handToCompare = 0;
                                break;
                            }
                        }
                    }
                }
                if (highestHands.Count > 1)
                {
                    for (int winningHand = 0; winningHand < highestHands.Count; winningHand++)
                    {
                        foreach (PokerPlayer player in Players)
                        {
                            if (player.PlayerID == highestHands[winningHand].PlayerID)
                            {
                                player.Balance += CurrentPot / highestHands.Count;
                            }
                        }
                    }
                    for (int player = 0; player < Players.Count; player++)
                    {
                        if (Players[player].Button == PokerButton.SmallBlind)
                        {
                            Players[player].Balance += CurrentPot % highestHands.Count;
                        }
                    }
                    foreach (PokerPlayer player in Players)
                    {
                        player.Bet = 0;
                    }
                    MessageBox.Show("Gelijkspel");
                }
                else
                {
                    Players[highestHands[0].PlayerID].Balance += CurrentPot;
                    MessageBox.Show($"{Players[highestHands[0].PlayerID].PokerName} heeft gewonnen!");
                }
            }
            else
            {
                Players[highestHands[0].PlayerID].Balance += CurrentPot;
                MessageBox.Show($"{Players[highestHands[0].PlayerID].PokerName} heeft gewonnen!");
            }
            GameState = "End";
            EndGame();
        }

        private async void showCards(List<PokerPlayer> Players)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    for (int i = 0; i < Players.Count; i++)
                    {
                        if (Players[i].PlayerID != 0)
                        {
                            Storyboard playerCards = (Storyboard)(window as PokerWindow).FindResource($"sbPlayer{Players[i].PlayerID}");
                            playerCards.Begin();
                        }
                        await Task.Delay(300);
                        Players[i].Hand[0].Turned = false;
                        Players[i].Hand[1].Turned = false;
                    }
                }
            }
        }

        public void ClearTable()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PokerWindow))
                {
                    Storyboard player0_out = (Storyboard)(window as PokerWindow).FindResource("sbPlayer0_Out");
                    player0_out.Begin();
                    Storyboard player1_out = (Storyboard)(window as PokerWindow).FindResource("sbPlayer1_Out");
                    player1_out.Begin();
                    Storyboard player2_out = (Storyboard)(window as PokerWindow).FindResource("sbPlayer2_Out");
                    player2_out.Begin();
                    Storyboard player3_out = (Storyboard)(window as PokerWindow).FindResource("sbPlayer3_Out");
                    player3_out.Begin();
                    Storyboard board = (Storyboard)(window as PokerWindow).FindResource("sbTableOut");
                    board.Begin();
                }
            }
        }

        public void EndGame()
        {
            CurrentPot = 0;
            ClearTable();
            foreach (PokerPlayer player in Players)
            {
                player.Bet = 0;
                player.Hand.Clear();
                player.Checked = false;
                player.Folded = false;
                player.IsAllIn = false;
            }
            MyTable.Clear();
            switch (Players[0].Button)
            {
                case PokerButton.Dealer:
                    Players[0].Button = PokerButton.None;
                    Players[1].Button = PokerButton.Dealer;
                    Players[2].Button = PokerButton.SmallBlind;
                    Players[3].Button = PokerButton.BigBlind;
                    break;
                case PokerButton.SmallBlind:
                    Players[0].Button = PokerButton.Dealer;
                    Players[1].Button = PokerButton.SmallBlind;
                    Players[2].Button = PokerButton.BigBlind;
                    Players[3].Button = PokerButton.None;
                    break;
                case PokerButton.BigBlind:
                    Players[0].Button = PokerButton.SmallBlind;
                    Players[1].Button = PokerButton.BigBlind;
                    Players[2].Button = PokerButton.None;
                    Players[3].Button = PokerButton.Dealer;
                    break;
                case PokerButton.None:
                    Players[0].Button = PokerButton.BigBlind;
                    Players[1].Button = PokerButton.None;
                    Players[2].Button = PokerButton.Dealer;
                    Players[3].Button = PokerButton.SmallBlind;
                    break;
            }
        }
    }
}
