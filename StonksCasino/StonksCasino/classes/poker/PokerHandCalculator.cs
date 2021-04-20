using StonksCasino.classes.Main;
using StonksCasino.enums.card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StonksCasino.classes.poker
{
    class PokerHandCalculator
    {
        public static PokerHandValue GetHandValue(List<Card> hand, List<Card> table)
        {
            List<Card> cards = new List<Card>();
            cards.AddRange(hand);
            cards.AddRange(table);
            cards = cards.OrderBy(x => (int)x.Value).ToList();
            bool royal = false;
            bool straight = false;
            bool flush = false;

            bool fourOfAKind = false;
            bool fullHouse = false;
            bool threeOfAKind = false;
            bool pair = false;
            bool twopair = false;

            // resultFS = result Flush Straight 
            List<Card> resultFS = CheckFlushStraight(cards, out royal, out straight, out flush);
            List<Card> resultPair = CheckPairs(cards, out fourOfAKind, out  fullHouse, out  threeOfAKind, out  pair, out twopair);
            if (royal && flush)
            {
                MessageBox.Show($"RoyalFlush, {resultFS[0].Type} {resultFS[0].Value}, {resultFS[1].Type} {resultFS[1].Value}, {resultFS[2].Type} {resultFS[2].Value} {resultFS[3].Type} {resultFS[3].Value}, {resultFS[4].Type} {resultFS[4].Value}");
            }
            else if (straight && flush)
            {
                MessageBox.Show($"StraightFlush, {resultFS[0].Type} {resultFS[0].Value}, {resultFS[1].Type} {resultFS[1].Value}, {resultFS[2].Type} {resultFS[2].Value} {resultFS[3].Type} {resultFS[3].Value}, {resultFS[4].Type} {resultFS[4].Value}");
            }
            else if (fourOfAKind)
            {
                MessageBox.Show($"four of a kind, {resultPair[0].Type} {resultPair[0].Value}, {resultPair[1].Type} {resultPair[1].Value}, {resultPair[2].Type} {resultPair[2].Value} {resultPair[3].Type} {resultPair[3].Value}, {resultPair[4].Type} {resultPair[4].Value}");
            }
            else if (fullHouse)
            {
                MessageBox.Show($"FullHouse, {resultPair[0].Type} {resultPair[0].Value}, {resultPair[1].Type} {resultPair[1].Value}, {resultPair[2].Type} {resultPair[2].Value} {resultPair[3].Type} {resultPair[3].Value}, {resultPair[4].Type} {resultPair[4].Value}");
            }
            else if (flush)
            {
                MessageBox.Show($"Flush, {resultFS[0].Type} {resultFS[0].Value}, {resultFS[1].Type} {resultFS[1].Value}, {resultFS[2].Type} {resultFS[2].Value} {resultFS[3].Type} {resultFS[3].Value}, {resultFS[4].Type} {resultFS[4].Value}");
            }
            else if (straight)
            {
                MessageBox.Show($"straight, {resultFS[0].Type} {resultFS[0].Value}, {resultFS[1].Type} {resultFS[1].Value}, {resultFS[2].Type} {resultFS[2].Value} {resultFS[3].Type} {resultFS[3].Value}, {resultFS[4].Type} {resultFS[4].Value}");
            }
            else if (threeOfAKind)
            {
                MessageBox.Show($"FullHouse, {resultPair[0].Type} {resultPair[0].Value}, {resultPair[1].Type} {resultPair[1].Value}, {resultPair[2].Type} {resultPair[2].Value} {resultPair[3].Type} {resultPair[3].Value}, {resultPair[4].Type} {resultPair[4].Value}");
            }
            else if (twopair)
            {
                MessageBox.Show($"twopair, {resultPair[0].Type} {resultPair[0].Value}, {resultPair[1].Type} {resultPair[1].Value}, {resultPair[2].Type} {resultPair[2].Value} {resultPair[3].Type} {resultPair[3].Value}, {resultPair[4].Type} {resultPair[4].Value}");
            }
            else if (pair)
            {
                MessageBox.Show($"pair, {resultPair[0].Type} {resultPair[0].Value}, {resultPair[1].Type} {resultPair[1].Value}, {resultPair[2].Type} {resultPair[2].Value} {resultPair[3].Type} {resultPair[3].Value}, {resultPair[4].Type} {resultPair[4].Value}");
            }
            else
            {
                MessageBox.Show($"highend, {resultPair[0].Type} {resultPair[0].Value}, {resultPair[1].Type} {resultPair[1].Value}, {resultPair[2].Type} {resultPair[2].Value} {resultPair[3].Type} {resultPair[3].Value}, {resultPair[4].Type} {resultPair[4].Value}");
            }

            return null;
        }

        private static List<Card> CheckFlushStraight(List<Card> cards, out bool royal, out bool straight, out bool flush)
        {
            royal = false;
            straight = false;
            flush = false;
            // maak lijst van alle met dezelfde type
            List<Card> result = new List<Card>();
            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                List<Card> suit = cards.Select(x => x).Where(x => x.Type == type).ToList();
                if (suit.Count >= 5)
                {
                    flush = true;
                    suit = suit.OrderBy(x => (int)x.Value).ToList();
                   // List<Card> suitList = suit.ToList();

                    straight = CheckStraight(suit, out result, out royal);
                    if (!straight)
                    {
                        result = new List<Card>();
                        while (result.Count < 5)
                        {
                            result.Add(suit[suit.Count - 1]);
                            suit.RemoveAt(suit.Count - 1);
                        }
                    }
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
            List<List<Card>> straights = new List<List<Card>>();

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
                        straights.Add(straight);
                        straight = new List<Card>();
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
            if (straight.Count == 5 && straight[straight.Count -1].Value == CardValue.Ace)
            {
                royal = true;
            }

            //check for lowest
            if (cardsToCheck[cardsToCheck.Count-1].Value == CardValue.Ace && straight.Count < 5)
            {
                straights.Add(straight);
                foreach (List<Card> straightToCheck in straights)
                {
                    if(straightToCheck.Count == 4 && straightToCheck[0].Value == CardValue.Two)
                    {
                        straight = straightToCheck;
                        straight.Add(cardsToCheck[cardsToCheck.Count - 1]);
                        straight = straight.OrderBy(x => (int)x.Value).ToList();
                    }
                }
            }

            //check if straight
            if (straight.Count >= 5)
            {
                straight.RemoveRange(0, straight.Count - 5);
                return true;
            }
            return false;
        }

        private static List<Card> CheckPairs(List<Card> hand, out bool fourOfAKind, out bool fullHouse, out bool threeOfAKind, out bool pair, out bool twoPair)
        {
            fourOfAKind = false;
            fullHouse = false;
            threeOfAKind = false;
            pair = false;
            twoPair = false;

            List<List<Card>> pairs = new List<List<Card>>();

            List<Card> combo = new List<Card>();
            foreach (Card card in hand)
            {
                if (combo.Count == 0)
                {
                    combo.Add(card);
                }
                else if (combo[0].Value == card.Value)
                {
                    combo.Add(card);
                }
                else if (combo.Count > 1)
                {
                    pairs.Add(combo);
                    combo = new List<Card>();
                    combo.Add(card);
                }
                else
                {
                    combo = new List<Card>();
                    combo.Add(card);
                }
            }

            if(pairs.Count > 0)
            {
                //check four four of a kind
                List<Card> Result = CheckFourOfAKind(pairs, hand, out fourOfAKind);
                if (fourOfAKind)
                {
                    return Result;
                }

                //check for three of a kind and fullhouse
                Result = CheckThreeOfAKind(pairs, hand, out threeOfAKind, out fullHouse);
                if (fullHouse || threeOfAKind)
                {
                    return Result;
                }

                //check for pair and twopair also rerturn if nothing
                Result = CheckPair(pairs, hand, out pair, out twoPair);
                return Result;
            }
            List<Card> result = new List<Card>();
            while (result.Count < 5)
            {
                result.Add(hand[hand.Count - 1]);
                hand.RemoveAt(hand.Count - 1);
            }

            return result;
        }

        private static List<Card> CheckFourOfAKind(List<List<Card>> pairs, List<Card> hand, out bool FourOfAKind)
        {
            FourOfAKind = false;
            List<Card> result = new List<Card>();
            foreach (List<Card> pair in pairs)
            {
                if (pair.Count == 4)
                {
                    result.AddRange(pair);
                    foreach (Card card in pair)
                    {
                        hand.Remove(card);
                    }

                    result.Add(hand[hand.Count - 1]);

                    FourOfAKind = true;
                }
            }
            return result;
        }

        private static List<Card> CheckThreeOfAKind(List<List<Card>> pairs, List<Card> hand, out bool ThreeOfAKind, out bool FullHouse)
        {
            ThreeOfAKind  = false;
            FullHouse = false;
            List<Card> result = new List<Card>();

            if(pairs[0][0].Value == CardValue.Ace && pairs[0].Count == 3)
            {
                ThreeOfAKind = true;
                result.AddRange(pairs[0]);
                pairs.RemoveAt(0);
            }

            for (int i = pairs.Count-1; i >= 0; i--)
            {
                if (!ThreeOfAKind && pairs[i].Count == 3)
                {
                    ThreeOfAKind = true;
                    result.AddRange(pairs[i]);
                }
                else if (ThreeOfAKind)
                {
                    FullHouse = true;
                    result.Add(pairs[i][0]);
                    result.Add(pairs[i][1]);
                    return result;
                }
            }

            if (ThreeOfAKind)
            {
                
                foreach (Card card in result)
                {
                    hand.Remove(card);
                }


                result.Add(hand[hand.Count - 1]);
                result.Add(hand[hand.Count - 2]);

            }

            return result;
        }

        private static List<Card> CheckPair(List<List<Card>> pairs, List<Card> hand, out bool pair, out bool twoPair)
        {
            pair = false;
            twoPair = false;

            List<Card> result = new List<Card>();

            foreach (List<Card> combo in pairs)
            {
                if (result.Count < 4)
                {
                    result.AddRange(combo);
                }
            }

            if(result.Count >= 2)
            {
                pair = true;
            }
            if(result.Count >= 4)
            {
                twoPair = true;
            }

            foreach (Card card in result)
            {
                hand.Remove(card);
            }

            while (result.Count < 5)
            {
                result.Add(hand[hand.Count -1]);
                hand.RemoveAt(hand.Count - 1);
            }
            return result;
        }

    }
}
