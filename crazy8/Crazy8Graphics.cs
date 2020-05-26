/*
 Nathaniel Collier
 5/20/2008
 This is where things that are specific to crazy8 graphics go
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace crazy8
{
    public class Crazy8Graphics : CardGraphics
    {

        // instance variables


        // constructors
        public Crazy8Graphics(int width_, int height_, string pic_list_filename)
            : base(width_, height_, pic_list_filename)
        {

        }

        // properties

        // meathods

        public void HideNonCrazy8Buttons()
        {
            dealButton.Hide(slate, pic);
            hitButton.Hide(slate, pic);

        }

        public void DisplayTopCard(Card TopCard)
        {
            TopCardGraphic.DrawImage(slate, pic_list[TopCard.Index()]);
        }

        public override void MouseMoveHandler(int mX, int mY)
        {
            mouseMoveDisplay.Draw(slate, new Pen(Color.Green, 20));
            mouseMoveDisplay.Fill(slate, Brushes.White);
            mouseMoveDisplay.DrawText(slate, "Mouse Pos X:" + mX + " Y:" + mY, arialblk_8pt, blueBrush);

            statusBar.Clear(slate);

            if (TopCardGraphic.CheckPoint(mX, mY))
            {
                statusBar.DrawText(slate, "Mouse in top card", arialblk_8pt, redBrush);
            }


            // See if the mouse is in any of the player cards
            for (int i = 0; i < PlayerBox.Length; ++i)
            {
                if (PlayerBox[i].CheckPoint(mX, mY))
                {
                    statusBar.DrawText(slate, "Mouse in player " + i + " cards: pos=" + MouseInCardPosition(mX, mY, PlayerBox[i].Dimension),
                                            arialblk_8pt, redBrush);
                    break; // out of for loop
                }
            }
        }

        /*
         This is where all the real action takes place
         */
        public override void MouseButtonHandler(int Mx, int My, MouseButtons Mb, CardGame game)
        {
            Crazy8Game Game = (Crazy8Game)game; // downcast


            // first check to see if the mouse was clicked in one of the player cards
            // See if the mouse is in any of the player cards
            for (int i = 0; i < PlayerBox.Length; ++i)
            {
                if (PlayerBox[i].CheckPoint(Mx, My))
                {
                    
                    int cardPosition = MouseInCardPosition(Mx, My, PlayerBox[i].Dimension);
                    // here we know what card was clicked

                    // the player is i
                    // the card is cardPosition
                    Game.MouseClickedInPlayerCard(i, cardPosition);
                    
                    // we found where they clicked so lets return
                    return;
                }
            }

            if (newGameButton.CheckPoint(Mx, My))  // if the user clicks in on the new button we start a new game
            {
                game.Play();
            }
            else if (drawButton.CheckPoint(Mx, My)) // if the user clicks in the draw button we will tell the game that player wants to draw a new card
            {
                Game.MouseClickedOnDraw();
            }
            else if (drawButton.CheckPoint(Mx,My) )// if the user clicks the undo button, we need to undo the players last action
            {
                Game.MouseClickedOnUndo();
            }
            
        }
    }
}
