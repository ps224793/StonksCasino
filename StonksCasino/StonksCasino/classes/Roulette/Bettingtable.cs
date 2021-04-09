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

            _bets.Add(new Bet(new int[] { 4, 7 }, false, 18));
            _bets.Add(new Bet(new int[] { 4, 5, 7, 8 }, false, 9));
            _bets.Add(new Bet(new int[] { 5, 8 }, false, 18));
            _bets.Add(new Bet(new int[] { 5, 6, 8, 9 }, false, 9));
            _bets.Add(new Bet(new int[] { 3, 6 }, false, 18));

            _bets.Add(new Bet(new int[] { 7 }));
            _bets.Add(new Bet(new int[] { 7, 8 }, false, 18));
            _bets.Add(new Bet(new int[] { 8 }));
            _bets.Add(new Bet(new int[] { 8, 9 }, false, 18));
            _bets.Add(new Bet(new int[] { 9 }));

            _bets.Add(new Bet(new int[] { 7, 10 }, false, 18));
            _bets.Add(new Bet(new int[] { 7, 8, 10, 11 }, false, 9));
            _bets.Add(new Bet(new int[] { 8, 11 }, false, 18));
            _bets.Add(new Bet(new int[] { 8, 9, 11, 12 }, false, 9));
            _bets.Add(new Bet(new int[] { 9, 12 }, false, 18));

            _bets.Add(new Bet(new int[] { 10 }));
            _bets.Add(new Bet(new int[] { 10, 11 }, false, 18));
            _bets.Add(new Bet(new int[] { 11 }));
            _bets.Add(new Bet(new int[] { 11, 12 }, false, 18));
            _bets.Add(new Bet(new int[] { 12 }));

            _bets.Add(new Bet(new int[] { 10, 13 }, false, 18));
            _bets.Add(new Bet(new int[] { 10, 11, 13, 14 }, false, 9));
            _bets.Add(new Bet(new int[] { 11, 14 }, false, 18));
            _bets.Add(new Bet(new int[] { 11, 12, 14, 15 }, false, 9));
            _bets.Add(new Bet(new int[] { 12, 15 }, false, 18));

            _bets.Add(new Bet(new int[] { 13 }));
            _bets.Add(new Bet(new int[] { 13, 14 }, false, 18));
            _bets.Add(new Bet(new int[] { 14 }));
            _bets.Add(new Bet(new int[] { 14, 15 }, false, 18));
            _bets.Add(new Bet(new int[] { 15 }));

            _bets.Add(new Bet(new int[] { 13, 16 }, false, 18));
            _bets.Add(new Bet(new int[] { 13, 14, 16, 17 }, false, 9));
            _bets.Add(new Bet(new int[] { 14, 17 }, false, 18));
            _bets.Add(new Bet(new int[] { 14, 15, 17, 18 }, false, 9));
            _bets.Add(new Bet(new int[] { 15, 18 }, false, 18));

            _bets.Add(new Bet(new int[] { 16 }));
            _bets.Add(new Bet(new int[] { 16, 18 }, false, 18));
            _bets.Add(new Bet(new int[] { 17 }));
            _bets.Add(new Bet(new int[] { 17, 18 }, false, 18));
            _bets.Add(new Bet(new int[] { 18 }));

            _bets.Add(new Bet(new int[] { 16, 19 }, false, 18));
            _bets.Add(new Bet(new int[] { 16, 17, 19, 20 }, false, 9));
            _bets.Add(new Bet(new int[] { 17, 20 }, false, 18));
            _bets.Add(new Bet(new int[] { 17, 18, 20, 21 }, false, 9));
            _bets.Add(new Bet(new int[] { 18, 21 }, false, 18));

            _bets.Add(new Bet(new int[] { 19 }));
            _bets.Add(new Bet(new int[] { 19, 20 }, false, 18));
            _bets.Add(new Bet(new int[] { 20 }));
            _bets.Add(new Bet(new int[] { 20, 21  }, false, 18));
            _bets.Add(new Bet(new int[] { 21 }));

            _bets.Add(new Bet(new int[] { 19, 22 }, false, 18));
            _bets.Add(new Bet(new int[] { 19, 20, 22, 23 }, false, 9));
            _bets.Add(new Bet(new int[] { 20, 23 }, false, 18));
            _bets.Add(new Bet(new int[] { 20, 21, 23, 24 }, false, 9));
            _bets.Add(new Bet(new int[] { 21, 24 }, false, 18));

            _bets.Add(new Bet(new int[] { 22 }));
            _bets.Add(new Bet(new int[] { 22, 23 }, false, 18));
            _bets.Add(new Bet(new int[] { 23 }));
            _bets.Add(new Bet(new int[] { 23, 24 }, false, 18));
            _bets.Add(new Bet(new int[] { 24 }));

            _bets.Add(new Bet(new int[] { 22, 25 }, false, 18));
            _bets.Add(new Bet(new int[] { 22, 23, 25, 26 }, false, 9));
            _bets.Add(new Bet(new int[] { 23, 26 }, false, 18));
            _bets.Add(new Bet(new int[] { 23, 24, 26, 27 }, false, 9));
            _bets.Add(new Bet(new int[] { 24, 27 }, false, 18));

            _bets.Add(new Bet(new int[] { 25 }));
            _bets.Add(new Bet(new int[] { 25, 26 }, false, 18));
            _bets.Add(new Bet(new int[] { 26 }));
            _bets.Add(new Bet(new int[] { 26, 27 }, false, 18));
            _bets.Add(new Bet(new int[] { 27 }));

            _bets.Add(new Bet(new int[] { 25, 28 }, false, 18));
            _bets.Add(new Bet(new int[] { 25, 26, 28, 29 }, false, 9));
            _bets.Add(new Bet(new int[] { 26, 29 }, false, 18));
            _bets.Add(new Bet(new int[] { 26, 27, 29, 30 }, false, 9));
            _bets.Add(new Bet(new int[] { 27, 30 }, false, 18));

            _bets.Add(new Bet(new int[] { 28 }));
            _bets.Add(new Bet(new int[] { 28, 29 }, false, 18));
            _bets.Add(new Bet(new int[] { 29 }));
            _bets.Add(new Bet(new int[] { 29, 30 }, false, 18));
            _bets.Add(new Bet(new int[] { 30 }));

            _bets.Add(new Bet(new int[] { 28, 31 }, false, 18));
            _bets.Add(new Bet(new int[] { 28, 29, 31, 32 }, false, 9));
            _bets.Add(new Bet(new int[] { 29, 32 }, false, 18));
            _bets.Add(new Bet(new int[] { 29, 30, 32, 33 }, false, 9));
            _bets.Add(new Bet(new int[] { 30, 32 }, false, 18));

            _bets.Add(new Bet(new int[] { 31 }));
            _bets.Add(new Bet(new int[] { 31, 32 }, false, 18));
            _bets.Add(new Bet(new int[] { 32 }));
            _bets.Add(new Bet(new int[] { 32, 33 }, false, 18));
            _bets.Add(new Bet(new int[] { 33 }));

            _bets.Add(new Bet(new int[] { 31, 34 }, false, 18));
            _bets.Add(new Bet(new int[] { 31, 32, 34, 35 }, false, 9));
            _bets.Add(new Bet(new int[] { 32, 35 }, false, 18));
            _bets.Add(new Bet(new int[] { 32, 33, 35, 36 }, false, 9));
            _bets.Add(new Bet(new int[] { 33, 36 }, false, 18));

            _bets.Add(new Bet(new int[] { 34 }));
            _bets.Add(new Bet(new int[] { 34, 35 }, false, 18));
            _bets.Add(new Bet(new int[] { 35 }));
            _bets.Add(new Bet(new int[] { 35, 36 }, false, 18));
            _bets.Add(new Bet(new int[] { 36 }));
        }

    }
}
