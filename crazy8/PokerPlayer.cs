/*
 Nathaniel Collier
 2/19/2008
 Player class for 5 Card Studd
*/
using System;
using System.Collections.Generic;

namespace crazy8
{

    /* a poker player and a crazy 8 player should inherit from a Player class 
     * 
     *      ------ Make this happen --------- */

    public class PokerPlayer : Player
    {
        // instance variables
        
        private PokerRankType rank = null;

        // constructors
        public PokerPlayer(int NumberOfCards) : base( NumberOfCards )
        {
           
        }

        // properties

        /*
         Array index for the hand array
        */
        public Card this[int index]
        {
            get
            {
                if (index >= 0 && index < hand.Count)
                {
                    return hand[index]; // check for proper card range too


                }
                return null;
            }
            set
            {
                if (index >= 0 && index < hand.Count)
                {
                    hand[index] = value; // check for proper card range too

                    // since the hand changed our rank is going to be different so 
                    // we will mark it eligible for destruction by this
                    rank = null;// eligible for destruction
                }
            }
        }

        public PokerRankType Rank
        {
            get
            {
                // hand should never be null so we probably won't check for that condition

                if (rank == null)
                {
                    DetermineRank();
                }

                return rank;
            }
        }


        /*
         Array index for the result table
        */



        // methods


        /*
         This function will use the hand the player has been delt and will return the hand's rank
        */
        private void DetermineRank()
        {

            if (rank == null)
            {
                rank = new PokerRankType();
                rank.HandType = PokerRankType.HandRank.NOT_FOUND;
            }

            int[] FaceKey = new int[13]; // 13 faces
            int[] SuitKey = new int[4]; // 4 suits


            // setup face key
            for (int i = 0; i < hand.Count; ++i)
            {
                FaceKey[(int)hand[i].FaceInt()]++;
            }

            // setup suit key
            for (int i = 0; i < hand.Count; ++i)
            {
                SuitKey[hand[i].SuitInt()]++;
            }

            // run through face key
            for (int count = 0, face = 0, OrderCount = 0; count < FaceKey.Length; ++count)
            {

                face = FaceKey[count];

                if (rank.HandType == PokerRankType.HandRank.NOT_FOUND)
                {
                    rank.HandType = PokerRankType.HandRank.HIGH_CARD; // start off here procede downward
                    // if at the very end we still have this handtype points are generated there
                }

                if (rank.HandType < PokerRankType.HandRank.ONE_PAIR) // only check for one pair if we have found a handrank not greater than equal to one pair
                {
                    // this will be the first time x == 2
                    if (face == 2)
                    {
                        rank.HandType = PokerRankType.HandRank.ONE_PAIR;
                        rank.OnePair = (Card.FaceIndex)count;
                        Console.WriteLine("One Pair:  {0}", rank.OnePair);
                        rank.points = 100 + (int)rank.OnePair;
                        continue; // continue so that the check for the next pair doesn't catch this one
                    }
                }


                if (rank.HandType < PokerRankType.HandRank.TWO_PAIR)
                {
                    // this will be the second time x == 2
                    if (face == 2)
                    {
                        rank.SecondPair = (Card.FaceIndex)count;
                        rank.HandType = PokerRankType.HandRank.TWO_PAIR;
                        Console.WriteLine("Two Pairs:  {0} and {1}", rank.OnePair, rank.SecondPair);
                        rank.points = 200 + (int)rank.OnePair + (int)rank.SecondPair;
                        continue;
                    }
                }


                if (rank.HandType < PokerRankType.HandRank.THREE_KIND)
                {
                    if (face == 3)
                    {
                        // if we allready found a pair then now that we have found 3 of a kind makes this a full house
                        if (rank.OnePair != Card.FaceIndex.NOT_FOUND) // its a full house instead of three of a kind
                        {
                            rank.HandType = PokerRankType.HandRank.FULL_HOUSE;
                            // rank.OnePair should be set to the 2 of a kind
                            rank.ThreeKind = (Card.FaceIndex)count;
                            Console.WriteLine("Full House: (2)-> {0} (3)-> {1}", rank.OnePair, rank.ThreeKind);
                            rank.points = 600 + (int)rank.OnePair + 3 * (int)rank.ThreeKind;
                            continue;
                        }
                        else // three of a kind
                        {
                            rank.HandType = PokerRankType.HandRank.THREE_KIND;
                            rank.ThreeKind = (Card.FaceIndex)count;
                            Console.WriteLine("Three of a Kind: {0}", rank.ThreeKind);
                            rank.points = 300 + (int)rank.ThreeKind;
                            continue;
                        }
                    }
                }

                if (rank.HandType < PokerRankType.HandRank.FOUR_KIND)
                {
                    if (face == 4)
                    {
                        rank.HandType = PokerRankType.HandRank.FOUR_KIND;
                        rank.FourKind = (Card.FaceIndex)count;
                        Console.WriteLine("Four of a Kind: {0}", rank.FourKind);
                        rank.points = 700 + (int)rank.FourKind;
                        continue;
                    }
                }


                if (rank.HandType < PokerRankType.HandRank.STRAIGHT)
                {
                    if (face == 1)
                    {

                        ++OrderCount;

                        if (OrderCount == 5)
                        {
                            // the cards are in numerical order in the key from least to highest
                            // so once we reach the end of the straight we will be at the high card
                            rank.HandType = PokerRankType.HandRank.STRAIGHT;
                            rank.StraightHigh = (Card.FaceIndex)count;
                            Console.WriteLine("Straight:  {0}", rank.StraightHigh);
                            rank.points = 400 + (int)rank.StraightHigh;
                            continue;
                        }

                    }
                    else
                        OrderCount = 0;
                }

                // need to test for flush




            }


            // run through suit key
            for (int count = 0, suit = 0; count < SuitKey.Length; ++count)
            {
                suit = SuitKey[count];

                if (suit == 5) // all 5 cards the same suit
                {
                    // flush, we need to see if a straight has been found
                    if (rank.HandType == PokerRankType.HandRank.STRAIGHT)
                    {
                        rank.HandType = PokerRankType.HandRank.STRAIGHT_FLUSH;
                        rank.points = 800 + (int)rank.StraightHigh;
                        break;
                    }
                    else if (rank.HandType < PokerRankType.HandRank.STRAIGHT)
                    {
                        rank.HandType = PokerRankType.HandRank.FLUSH;
                        rank.Suit = (Card.SuitIndex)count;
                        rank.points = 500; // not sure what else to add to this yet
                        break;
                    }
                }

            }


            if (rank.HandType == PokerRankType.HandRank.HIGH_CARD)
            {

                // sum up the hands
                for (int i = 0; i < hand.Count; ++i)
                {
                    rank.Sum += (int)hand[i].FaceValue();
                }

                rank.points = rank.Sum;
            }

            // rank should be set and ready to go at this point
        }





    }

}