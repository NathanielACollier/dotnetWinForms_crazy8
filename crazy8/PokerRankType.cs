/*
  Nathaniel Collier
  2/29/2008
  Rank Type Class
*/

using System;

namespace crazy8
{

    public class PokerRankType
    {
        /*
      These should be in order from lowest rank to highest rank
      Note: It is rather absurd that although I have declared this enumerated datatype
            to contain ints it still has to be typecasted everytime it is used as an int.
     
     HIGH_CARD means that none of the others where found so basicly if all the hands return
               HIGH_CARD then the highest card of each hand will need to be compared and then
               the next highest and so on and so forth.
        */
        public enum HandRank : int
        {
            HIGH_CARD = 0, ONE_PAIR, TWO_PAIR, THREE_KIND, STRAIGHT, ROYAL_STRAIGHT,
            FLUSH, FULL_HOUSE, FOUR_KIND, STRAIGHT_FLUSH, ROYAL_FLUSH,
            NOT_FOUND = -1 // nothing after this one
        };

        public HandRank HandType = HandRank.NOT_FOUND;
        public Card.FaceIndex OnePair = Card.FaceIndex.NOT_FOUND;
        public Card.FaceIndex SecondPair = Card.FaceIndex.NOT_FOUND; // this is used in 2 pairs
        public Card.FaceIndex ThreeKind = Card.FaceIndex.NOT_FOUND;
        public Card.FaceIndex FourKind = Card.FaceIndex.NOT_FOUND;
        public Card.FaceIndex StraightHigh = Card.FaceIndex.NOT_FOUND; // used in straights
        public Card.SuitIndex Suit = Card.SuitIndex.NOT_FOUND;
        public int Sum = 0; // used when both players have nothing this will contain a sum of the face values


        /*
     Points for hands are determined as follows
     (IF THIS CHANGES -- please update this list of rules )
     HIGH_CARD  if this is all they got then there points are the value of all the cards combined

        ONE_PAIR   start out with 100 points
                   add to the 100 points the value of the card in the pair
                           
        TWO_PAIR   start out with 200 points
                   add to the 200 points the value of card in pair1 and card in pair2


        THREE_KIND  start out with 300 points
                   add to the 300 points the value of card in three of a kind


        STRAIGHT   start out with 400 points
                   add to the 400 points the value of the highest card in the straight

        FLUSH      start out with 500 points
                   add to the 500 points 

        FULL_HOUSE  start out with 600 points
                   add to the 600 points the 3*(value of the 3 of a kind card) and the value
                    of the 2 of a kind card

        FOUR_KIND   start out with 700 points
                   add to the 700 points the value of the 4 of a kind 
                           
        STRAIGHT_FLUSH start out with 800 points
                    add to the 800 points the value of the highest card in the flush
    */
        public int points = 0; // Number of points the hand scored


    }

}
