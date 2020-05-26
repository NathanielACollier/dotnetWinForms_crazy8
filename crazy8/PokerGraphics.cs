/*
 Nathaniel Collier
 5/20/2008
 Specific things pertaining to PokerGraphics go in this class which derives from the
  abstract class CardGraphics
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace crazy8
{
    public class PokerGraphics : CardGraphics
    {
        // instance variables

        RectangleType[] PlayerRankDisplay = new RectangleType[4];

        // constructors

        public PokerGraphics(int width_, int height_, string pic_list_filename)
            : base(width_, height_, pic_list_filename)
        {

            // setup player rank display boxes
            PlayerRankDisplay[0] = new RectangleType(new RectangleDimension(30, 225, 125, 17));
            PlayerRankDisplay[1] = new RectangleType(new RectangleDimension(250, 30, 125, 17));
            PlayerRankDisplay[2] = new RectangleType(new RectangleDimension(610, 225, 125, 17));
            PlayerRankDisplay[3] = new RectangleType(new RectangleDimension(110, 450, 125, 17));
        }

        // properties


        // meathods
        public void DrawPokerRanks(PokerRankType Player1, PokerRankType Player2, PokerRankType Player3, PokerRankType Player4)
        {

            // clear out the previous ranks first
            for (int i = 0; i < PlayerRankDisplay.Length; ++i)
            {
                PlayerRankDisplay[i].Fill(slate, redBrush);
            }


            PlayerRankDisplay[0].DrawText(slate, "Rank: " + Player1.HandType, arialblk_8pt, blackBrush);
            PlayerRankDisplay[1].DrawText(slate, "Rank: " + Player2.HandType, arialblk_8pt, blackBrush);
            PlayerRankDisplay[2].DrawText(slate, "Rank: " + Player3.HandType, arialblk_8pt, blackBrush);
            PlayerRankDisplay[3].DrawText(slate, "Rank: " + Player4.HandType, arialblk_8pt, blackBrush);

        }


        public void HideNonPokerButtons()
        {
            dealButton.Hide(slate, pic);
            hitButton.Hide(slate, pic);
            drawButton.Hide(slate, pic);
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



        public override void MouseButtonHandler(int Mx, int My, MouseButtons Mb, CardGame game)
        {

            if (newGameButton.CheckPoint(Mx, My))  // if the user clicks in on the new button we start a new game
            {
                game.Play();
            }
        }
    }
}
