using StonksCasino.classes.Main;
using StonksCasino.enums.card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.poker
{
    class PokerHandCalculator
    {
        public static PokerHandValue GetHandValue(List<Card> hand, List<Card> table)
        {
            List<Card> cards = new List<Card>();
            cards.AddRange(hand);
            cards.AddRange(table);

            bool royal = false;
            bool straight = false;
            bool flush = false;

            List<Card> result = CheckHandValue(cards, out royal, out straight, out flush);
            if (royal && flush)
            {
                //royalflush
            }else if (straight && flush)
            {
                //straighflush
            }
            else if(flush)
            {
                //flush
            }else if (straight)
            {
                //straight
            }

            return null;
        }

        private static List<Card> CheckHandValue(List<Card> cards, out bool royal, out bool straight, out bool flush)
        {
            royal = false;
            straight = false;
            flush = false;
            // maak lijst van alle met dezelfde type
            List<Card> result = new List<Card>();
            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                IEnumerable<Card> suit = cards.Select(x => x).Where(x => x.Type == type);
                if (suit.Count() >= 5)
                {
                    flush = true;
                    suit = suit.OrderBy(x => (int)x.Value);
                    List<Card> suitList = suit.ToList();
                    

                    straight = CheckStraight(suitList, out result, out royal);
                }
            }
            if (!flush)
            {
                straight = CheckStraight(cards, out result, out royal);
            }
            return result;
        }

        private static bool CheckStraight(List<Card> cardsToCheck, out List<Card> straight, out bool royal)
        {
            royal = false;
            straight = new List<Card>();
            foreach (Card card in cardsToCheck)
            {
                if (straight.Count > 0)
                {
                    if ((int)card.Value == ((int)straight[straight.Count - 1].Value))
                    {
                        // do nothing
                    }
                    else if ((int)card.Value == ((int)straight[straight.Count - 1].Value) + 1)
                    {
                        straight.Add(card);
                    }
                    else if (straight.Count < 5)
                    {
                        straight.Clear();
                        straight.Add(card);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    straight.Add(card);
                }
            }
            
            //check if royal
            if (cardsToCheck[0].Value == CardValue.Ace)
            {
                if (straight.Count >= 4 && straight[straight.Count - 1].Value == CardValue.King)
                {
                    straight.Add(cardsToCheck[0]);
                    royal = true;
                }
                //royal flush
            }
            //check if straight
            if (straight.Count >= 5)
            {
                straight.RemoveRange(0, straight.Count - 5);
                return true;
            }
            return false;
        }
    }
}
