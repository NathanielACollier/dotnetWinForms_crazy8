using System;


namespace crazy8
{

    public class Crazy8Game : CardGame
    {
        // instance variables
        Crazy8Graphics board; // this is the object all drawings will be done on

        LogFile log = new LogFile("log.txt"); // we need to write out debug information and stuff to this log file


        Crazy8Player[] PlayerList = new Crazy8Player[4];

        Card TopCard; // this is our playing card

        // constructors
        public Crazy8Game( Crazy8Graphics board_)
            : base()
        {
            board = board_;

            // setup the players
            for (int i = 0; i < PlayerList.Length; ++i)
            {
                PlayerList[i] = new Crazy8Player("Player " + i, i, 5);
            }

            board.HideNonCrazy8Buttons();
        }

        // properties

        
        // meathods

        public void MouseClickedInPlayerCard(int PlayerPos, int cardPosition)
        {
            // we should now play the card thats in player PlayerPos' hand at position cardPosition

            log.WriteLine("Crazy8Log: Player " + PlayerPos + " Clicked on card " + cardPosition);

            // we need to find out what card is in that position
            

            DrawCards();

        }

        public void MouseClickedOnUndo()
        {
            // we need to have an undo stack
            // and use it to undo the players last action
            // probably way into the future we will implement this
            log.WriteLine("Crazy8Log: Player clicked on the undo button");



            DrawCards(); // this draws the cards after whatever action that changed the cards takes place.
        }

        public void MouseClickedOnDraw()
        {
            Card card = deck.DealCard();
            // we should now draw a new card and add it to the players hand
            log.WriteLine("Crazy8Log: Player Draws " + card.ToString());

            PlayerList[0].InsertCard( card );

            DrawCards();


        }

        public void DrawCards()
        {
            board.DisplayPlayerCards(PlayerList[0].Hand, 0);
            board.DisplayPlayerCards(PlayerList[1].Hand, 1);
            board.DisplayPlayerCards(PlayerList[2].Hand, 2);
            board.DisplayPlayerCards(PlayerList[3].Hand, 3);
            board.DisplayTopCard(TopCard);
        }

        // The play meathod is how we start a new game
        public override void Play()
        {

            // reset players
            for (int i = 0; i < PlayerList.Length; ++i)
            {
                PlayerList[i].ResetPlayer(5);
            }

            deck.Shuffle();

            // deal cards to the players
            for (int i = 0; i < 5; ++i)
            {
                for (int player = 0; player < PlayerList.Length; ++player)
                {
                    PlayerList[player].InsertCard( deck.DealCard() );


                }
            }

            TopCard = deck.DealCard();

            DrawCards();

            // probably all that this meathod needs to do for right now
            // since the game will be played by clicking on cards.
        }

        public override void ExitGame()
        {
            log.Close();
        }
        

    }

}