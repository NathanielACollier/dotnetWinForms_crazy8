/*
 Nathaniel Collier
 2/4/2008
 Lab Assignment #3
 Modified code from p348 Visual C# 2005 How To Program
 This is the card class
*/
using System;

namespace crazy8
{
    public class Card
    {
        // instance variables

        // This stuff is still saved as a string from the old code because
        // I haven't got around to changing it I have no idea why someone would
        // save such simple information as a string.
        private string face; // face of card 
        private string suit; // suit of card 

        // All possible values are defined by the below two arrays

        /*
          Value of the cards
     
         Make sure NOT_FOUND is at the very end of the list
        */
        public enum FaceValueType : int
        {
            ACE = 1, KING = 13, QUEEN = 12, JACK = 13, TEN = 10, NINE = 9, EIGHT = 8, SEVEN = 7, SIX = 6, FIVE = 5, FOUR = 4,
            THREE = 3, TWO = 2, NOT_FOUND = -1
        };

        /*
          Index of the cards
      
          Make sure that NOT_FOUND is at the very end of the list
        */
        public enum FaceIndex : int
        {
            ACE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, NOT_FOUND = -1
        };

        public enum SuitIndex : int
        {
            HEART, DIAMOND, CLUB, SPADE, NOT_FOUND = -1
        };

        // FaceValueList and FaceList are parallel arrays

        public static FaceValueType[] FaceValueList = { FaceValueType.TWO, FaceValueType.THREE, FaceValueType.FOUR, 
        FaceValueType.FIVE, FaceValueType.SIX, FaceValueType.SEVEN, 
        FaceValueType.EIGHT, FaceValueType.NINE, FaceValueType.TEN, 
        FaceValueType.JACK, FaceValueType.QUEEN, FaceValueType.KING, FaceValueType.ACE };

        // Make sure facelist is allways ordered from least value card to highest valued
        public static string[] FaceList = { "ace","two","three","four","five","six","seven","eight","nine","ten",
                                "jack","queen","king" };
        public static string[] SuitList = { "heart", "diamond", "spade", "club" };
        // constructors

        // two paramater constructor initializes card's face and suit
        public Card(string cardFace, string cardSuit)
        {
            Suit = cardSuit;
            Face = cardFace;
        }
        // properties


        public string Face
        {
            get
            {
                return face;
            }
            set
            {
                // FindFace checks to see if the card is in the facelist
                // if its not the value isn't set.
                if (FindFace(value) != FaceIndex.NOT_FOUND)
                {
                    face = value;
                }
            }
        }




        public string Suit
        {
            get
            {
                return suit;
            }
            set
            {
                if (FindSuit(value) != -1)
                {
                    suit = value;
                }
            }
        }




        // meathods

        /*
          Given a face string this function will return the integer value of
          the face (order it is in the array ).
          It is very important to remember that 0-ace and 1-two 2-three and so on.
        */
        public static FaceIndex FindFace(string face_)
        {
            for (FaceIndex i = FaceIndex.ACE; (int)i < FaceList.Length; ++i)
                if (FaceList[(int)i] == face_.ToLower())
                    return i;
            return FaceIndex.NOT_FOUND;
        }

        /*
          Given a suit string this function will return the position in the
          array list or order of the suit
          0-heart 1-diamond 2-spade 3-club
        */
        public static int FindSuit(string suit_)
        {
            for (int i = 0; i < SuitList.Length; ++i)
                if (SuitList[i] == suit_.ToLower())
                    return i;
            return -1;
        }

        /*
         String represenation of the card is returned
        */
        public override string ToString()
        {
            return face + " of " + suit;
        }

        public void SetCard(string face_, string suit_)
        {
            Face = face_;
            Suit = suit_;
        }

        /*
         Returns array index of the card
         FOR USE ONLY WHEN DEALING WITH FACELIST
        */
        public FaceIndex FaceInt()
        {
            return FindFace(Face);
        }

        /*
         Returns array index of the card
         FOR USE ONLY WHEN DEALING WITH SUITLIST
        */
        public int SuitInt()
        {
            return FindSuit(Suit);
        }

        /*
         returns the face value of a card NOT THE NUMBER OF THE CARD
         FOR USE ONLY WITH -----FACEVALUELIST----
         */
        public FaceValueType FaceValue()
        {
            return FaceValueList[(int)FindFace(Face)];
        }


        /*
         Location of the card in an ordered deck
         */
        public int Index()
        {
            int faceI = (int)FaceInt();

            int suitI = SuitInt();


            int index = faceI * 4 + suitI;



            return index;

        }


    }

}