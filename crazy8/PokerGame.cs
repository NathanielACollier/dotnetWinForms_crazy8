using System;
using System.Drawing;

namespace crazy8
{


    public class PokerGame : CardGame
    {
        // instance variables
        PokerGraphics board; // pointer to the board that stuff is drawn on so that we can display things
                            //  to the player
        LogFile log = new LogFile("log.txt");
        


        // constructors
        public PokerGame( PokerGraphics board_ ) : base()
        {
            board = board_;
           
        }


        // properties




        public override void Play()
        {

            /*
             Hide buttons that are not used in poker
             */
            board.HideNonPokerButtons();


            PokerPlayer[] PlayerList = new PokerPlayer[4];

            for (int i = 0; i < PlayerList.Length; ++i)
            {
                PlayerList[i] = new PokerPlayer(5);
            }
            
            deck.Shuffle();

            // deal cards to the players
            for (int i = 0; i < 5; ++i)
            {
                for (int player = 0; player < PlayerList.Length; ++player)
                {
                    PlayerList[player].Hand.Add( deck.DealCard() );


                }
            }

            board.DisplayPlayerCards(PlayerList[0].Hand, 0);
            board.DisplayPlayerCards(PlayerList[1].Hand, 1);
            board.DisplayPlayerCards(PlayerList[2].Hand, 2);
            board.DisplayPlayerCards(PlayerList[3].Hand, 3);


            /*
                Order the positions in the key from greatest point value to lowest point value
            */
            int[] PlayerPointsKey = new int[PlayerList.Length];
            int[] PlayerKey = new int[PlayerList.Length];




            // first populate PlayerKey
            for (int i = 0; i < PlayerList.Length; ++i)
            {
                PlayerKey[i] = i;
                PlayerPointsKey[i] = PlayerList[i].Rank.points;
            }

            
            // sort the players by the key to determine the win order of the hands
            StaticFunctions.BubbleSort(PlayerPointsKey, PlayerKey);

            // display on the screen the winning order and the hand types

            // player 1

            board.DrawPokerRanks(PlayerList[0].Rank, PlayerList[1].Rank, PlayerList[2].Rank, PlayerList[3].Rank);


            /*
             * Another very important note find out why on laptop visual studio lets you scroll up but not down ???
             */

            // setup logging of the poker game (should do the same thing for the crazy8 part)

            log.WriteLine( string.Format( "PokerLog:  Player1 Rank={0}\n\tPlayer2 Rank={1}\n\tPlayer3 Rank={2}\n\tPlayer4 Rank={3}\n",
                PlayerList[0].Rank.HandType, PlayerList[1].Rank.HandType, PlayerList[2].Rank.HandType, PlayerList[3].Rank.HandType ) );

           /*
            * create a log file somehow
            *    - log the players hands each one like
            *           Player1 Rank= %0 Cards= %1
            *           Player2 Rank= %2 Cards= %3
            *           Player3 Rank= %4 Cards= %5 etc...
            *    - Give a starting and ending time for the game
            *         (This will not be hard since the game is very simple right now)  */


            PlayerList[PlayerKey[PlayerList.Length - 1]].IncrimentWinCount(); // incriment the win count of the winning player


            /*
             * Since the wincount is known and below the game count is known display these on the screen somwhere
             *    So that it is easily accessible.
             *    -- Will need to decide which player the actual user is, either let them choose or just make them player1--
             */

            ++gameCount;
        }

        public override void ExitGame()
        {
            log.Close();
        }
    }

}