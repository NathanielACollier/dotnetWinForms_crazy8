using System;

namespace crazy8
{
    public abstract class CardGame
    {
        // instance variables
        protected int gameCount = 0; // count the number of games played

        protected DeckOfCards deck;


        // constructors

        public CardGame()
        {
            deck = new DeckOfCards();
        }

        // properties
        public int GameCount
        {
            get
            {
                return gameCount;
            }
        }

        // meathods


        /*
          A play meathod must be implimented
           Play starts a new game of whatever card game it is
         */
        public abstract void Play();


        /*
         This meathod is called when the player terminates the game
           It will release whatever resources where being used that
           needed to be terminated
          !!! This is not the same as new game !!!
         */
        public abstract void ExitGame();

    }
}
