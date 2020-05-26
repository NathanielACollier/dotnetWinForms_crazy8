/*
 * Nathaniel Collier
 * 7/22/2008
 * Abstract base class for card players
 */


using System;
using System.Collections.Generic;


namespace crazy8
{
    public abstract class Player
    {
        // instance variables

        protected List<Card> hand;
        protected int winCount = 0;


        // constructors
        public Player(int NumberOfCards)
        {
            hand = new List<Card>(NumberOfCards);

        }

        // properties
        public List<Card> Hand
        {
            get
            {
                return hand;
            }
        }

        public int WinCount
        {
            get
            {
                return winCount;
            }
        }

        // meathods

        public void IncrimentWinCount()
        {
            ++winCount;
        }


        public override string ToString()
        {
            string text = "";

            foreach (Card c in hand)
            {
                text += c + "\n";
            }

            return text;
        }


        

    }
}
