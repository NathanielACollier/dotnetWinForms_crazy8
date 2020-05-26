using System;


namespace crazy8
{

    public class Crazy8Player : Player
    {
        // instance variables
        
        /*
         Inherits 
           protected Card[] hand;
         */

        public enum GameStatsIndex : int  // this is the index for the GameStats Array
        {
            TOTAL_SCORE = 0, GAMES_WON = 1, GAMES_LOST = 2, GAMES_PLAYED = 3
        };
        private int[] GameStats = new int[5];

        public enum CardSuitIndex : int // index to CardSuitCount
        {
            HEART = 0, DIAMOND = 1, SPADE = 2, CLUB = 3, EIGHT = 4
        };
        private int[] CardSuitCount = new int[5];


        private int cardPoints = 0; // The total number of points in the hand

        private Boolean pass = false; // wether the player is giving up their turn or not

        string playerName; // The name identifier for the player (Remember this includes computerized players)

        int id = 0; // identification number

        // constructors
        public Crazy8Player( string name, int identificationNumber, int NumberOfCards ) : base( NumberOfCards )
        {
            playerName = name;
            id = identificationNumber;
        }

        // properties

        public int CardPoints
        {
            get
            {
                cardPoints = 0;

                // cycle through and count up the points
                for (int i = 0; i < hand.Count; ++i)
                {
                    if (hand[i].FaceInt() == Card.FaceIndex.EIGHT)
                        cardPoints += 50; // crazy8 worth 50 points
                    else
                        cardPoints += (int)hand[i].FaceInt();
                }

                return cardPoints;
            }
        }

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

                    // below here we would recalculate things


                }
            }
        }
            // meathods

            /*
             Determines the highest suit in the hand
             */
        private Card.SuitIndex high_suit()
        {
            Card.SuitIndex high = Card.SuitIndex.NOT_FOUND;

              int num_heart = CardSuitCount[(int)Card.SuitIndex.HEART],
                num_diamond = CardSuitCount[(int)Card.SuitIndex.DIAMOND],
                num_spade = CardSuitCount[(int)Card.SuitIndex.SPADE],
                num_club = CardSuitCount[(int)Card.SuitIndex.CLUB];

            // simple if checks for this
            if (num_heart >= num_club && num_heart >= num_spade && num_heart >= num_diamond)
                high = Card.SuitIndex.HEART;
            else
                if (num_club >= num_heart && num_club >= num_spade && num_club >= num_diamond)
                    high = Card.SuitIndex.CLUB;
                else
                    if (num_spade >= num_heart && num_spade >= num_diamond && num_spade >= num_club)
                        high = Card.SuitIndex.SPADE;
                    else
                        if (num_diamond >= num_heart && num_diamond >= num_spade && num_diamond >= num_club)
                            high = Card.SuitIndex.DIAMOND;

            return high;
        }


        public void ResetPlayer(int NumberOfCards)
        {
            // reset all of the card statistics
            for (int i = 0; i < CardSuitCount.Length; ++i)
            {
                CardSuitCount[i] = 0;
            }

            pass = false;
            cardPoints = 0;

            hand.Clear(); // clear out the hand

        }


        /*
         This will insert a card into the player's hand
         */
        public void InsertCard(Card card)
        {
            // ok this is the only way I know how to do this at the moment

            

            // update stats
            if (card.FaceInt() == Card.FaceIndex.EIGHT)
            {
                ++CardSuitCount[4];
            }
            else
            {
                ++CardSuitCount[(int)card.SuitInt()];
            }

            hand.Add(card);


        }




    }

}