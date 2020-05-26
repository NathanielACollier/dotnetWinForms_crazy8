/*
  Nathaniel Collier
  2/4/2008
  This is the deck of cards for the poker game
*/
using System;

namespace crazy8
{

    public class DeckOfCards
    {
        // instance variables
        private Card[] deck; // array of Card objects
        private int currentCard; // index of next card to be dealt
        private const int NUMBER_OF_CARDS = 52; // number of cards (constant )
        private Random randomNumbers; // random number generator
        // constructors

        // constructor fills deck of cards
        public DeckOfCards()
        {
            deck = new Card[NUMBER_OF_CARDS]; // create the array of cards
            currentCard = 0; // start off with the first card in the array
            randomNumbers = new Random(); // initialize random number generator

            // populate deck
            for (int i = 0; i < deck.Length; ++i)
            {
                deck[i] = new Card(Card.FaceList[i % Card.FaceList.Length],
                                    Card.SuitList[i % Card.SuitList.Length]);
            }
        }
        // properties

        // meathods

        public void Shuffle()
        {
            // after shuffling, dealing should start at deck[0] again
            currentCard = 0;

            // for each card pick another random card and swap them
            for (int first = 0; first < deck.Length; ++first)
            {
                // select a random number between 0 and 51
                int second = randomNumbers.Next(NUMBER_OF_CARDS); //(0,51+1)

                // swap current card with randomly selected card
                Card temp = deck[first];
                deck[first] = deck[second];
                deck[second] = temp;
            }
        }

        // deal one card
        public Card DealCard()
        {
            // determine wether cards remain to be dealt
            if (currentCard < deck.Length)
            {
                return deck[currentCard++]; // return card then incriment
            }
            else
            {
                return null; // null indicates deck is empty
            }
        }
    }

}