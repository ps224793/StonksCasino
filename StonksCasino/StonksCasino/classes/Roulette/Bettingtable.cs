using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.Roulette
{
    public class Bettingtable
    {
        private ObservableCollection<Bet> _bets = new ObservableCollection<Bet>();

        public ObservableCollection<Bet> Bets
        {
            get { return _bets; }
            set { _bets = value; }
        }

        public Bettingtable()
        {
            _bets.Add(new Bet(new int[] { 1 }));
            _bets.Add(new Bet(new int[] { 1, 2 }, false, 18));
            _bets.Add(new Bet(new int[] { 2 }));
            _bets.Add(new Bet(new int[] { 2, 3 }, false, 18));
            _bets.Add(new Bet(new int[] { 3 }));

            _bets.Add(new Bet(new int[] { 1, 4 }, false, 18));
            _bets.Add(new Bet(new int[] { 1, 2, 4, 5 }, false, 9));
            _bets.Add(new Bet(new int[] { 2, 5 }, false, 18));
            _bets.Add(new Bet(new int[] { 2, 3, 5, 6 }, false, 9));
            _bets.Add(new Bet(new int[] { 3, 6 }, false, 18));

            _bets.Add(new Bet(new int[] { 4 }));
            _bets.Add(new Bet(new int[] { 4, 5 }, false, 18));
            _bets.Add(new Bet(new int[] { 5 }));
            _bets.Add(new Bet(new int[] { 5, 6 }, false, 18));
            _bets.Add(new Bet(new int[] { 6 }));
        }

    }
}
