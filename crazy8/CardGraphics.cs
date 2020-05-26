/*
 Nathaniel Collier
 5/14/2008
 This class will contain generic Card graphics functions
  This class should have control over the slate (Graphical part of the screen)
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace crazy8
{
    public abstract class CardGraphics
    {
        // instance variables
        protected Bitmap pic;
        protected Graphics slate;

        protected picture_list pic_list; // this needs to be accessed by games

        protected RectangleType mouseMoveDisplay = new RectangleType(new RectangleDimension(620, 60, 150, 16));
        protected RectangleType TopCardGraphic = new RectangleType(new RectangleDimension(400, 300, 71, 96));
        protected RectangleType statusBar = new RectangleType(new RectangleDimension(0, 650, 800, 14));


        protected RectangleType[] PlayerBox = new RectangleType[4];

        



        protected RectangleType newGameButton = new RectangleType(new RectangleDimension(700, 500, 70, 20));
        protected RectangleType dealButton = new RectangleType(new RectangleDimension(700, 525, 35, 20));
        protected RectangleType undoButton = new RectangleType(new RectangleDimension(700, 550, 35, 20));
        protected RectangleType hitButton = new RectangleType(new RectangleDimension(700, 575, 25, 20));
        protected RectangleType drawButton = new RectangleType(new RectangleDimension(700, 600, 35, 20));


        protected System.Drawing.Font arialblk_8pt;
        protected Pen blkpen;
        protected Pen redpen;

        protected System.Drawing.Brush greenBrush = System.Drawing.Brushes.Green;
        protected System.Drawing.Brush blueBrush = System.Drawing.Brushes.Blue;
        protected System.Drawing.Brush redBrush = System.Drawing.Brushes.Red;
        protected System.Drawing.Brush blackBrush = System.Drawing.Brushes.Black;

        // constructors
        public CardGraphics( int width, int height, string pic_list_filename )
        {
            
            SetupSlate(width, height);
            


            // setup the different fonts that we will be using for text
            arialblk_8pt = new System.Drawing.Font("Arial Black", 8, FontStyle.Regular);

            // setup pens
            blkpen = new Pen(Color.Black, 1); // black pen width = 1
            redpen = new Pen(Color.Red, 1); // red pen width = 1


            // setup the player boxes
            PlayerBox[0] = new RectangleType(new RectangleDimension(30, 255, 71 * 5, 96 * 2));
            PlayerBox[1] = new RectangleType(new RectangleDimension(250, 60, 71 * 5, 96 * 2));
            PlayerBox[2] = new RectangleType(new RectangleDimension(490, 255, 71 * 5, 96 * 2));
            PlayerBox[3] = new RectangleType(new RectangleDimension(250, 450, 71 * 5, 96 * 2));





            InitialBoardSetup( pic_list_filename); // call this function last after everything else is setup
        }


        // properties



        // meathods


        public void ReDraw( Graphics formGraphics )
        {
             formGraphics.DrawImage(pic, 0, 0, pic.Width, pic.Height); // write the buffer to the form
        }


        public void SetupSlate(int width, int height)
        {
            // initialize bitmap back buffer ( So that we can have double buffering)
            pic = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            // get a graphical object that points to the graphic object of the bitmap back buffer
            slate = Graphics.FromImage(pic);

            // clear out buffer
            slate.Clear(Color.White);
        }

        public void InitialBoardSetup( string pic_list_filename)
        {

            // load the picture list
            pic_list = new picture_list(  pic_list_filename );


            for (int i = 0; i < PlayerBox.Length; ++i)
            {
                PlayerBox[i].Fill(slate, greenBrush);
            }



            newGameButton.Fill(slate, blueBrush);
            newGameButton.DrawText(slate, "New Game", arialblk_8pt, blackBrush);
            dealButton.Fill(slate, blueBrush);
            dealButton.DrawText(slate, "Deal", arialblk_8pt, blackBrush);
            undoButton.Fill(slate, blueBrush);
            undoButton.DrawText(slate, "Undo", arialblk_8pt, blackBrush);
            hitButton.Fill(slate, blueBrush);
            hitButton.DrawText(slate, "Hit", arialblk_8pt, blackBrush);
            drawButton.Fill(slate, blueBrush);
            drawButton.DrawText(slate, "Draw", arialblk_8pt, blackBrush);
        }

        /*
         The playerBox rectangle must be big enough to display the cards in the hand
         */
        public void DisplayPlayerCards(List<Card> hand, int playerID)
        {
            

            for (int i = 0; i < hand.Count; ++i)
            {
                DrawCardOnPlayer(PlayerBox[playerID].Dimension, i, pic_list[hand[i].Index()]);
            }
        }


        /*
         Given (x,y) coordinates this will return what position from {1,..,10}
          the coordinate is in
        */
        public int MouseInCardPosition(int x, int y, RectangleDimension playerBox)
        {
            int relativeX = x - playerBox.x;
            int relativeY = y - playerBox.y;

            int column = relativeX / 71;
            int row = relativeY / 96;

            int[,] matrix = { {1,2,3,4,5}, 
                              {6,7,8,9,10} };


            column = (column == 5) ? 4 : column; // the max 5 is column 4 not 5 see the explenation on the next line
            row = (row == 2) ? 1 : row; // if relativeY = 192 then 192/96 = 2 but the row is really 1 since thats the second row

            return matrix[row, column];
        }



        private Coordinate PositionCoordinates(int cardPosition)
        {
            Coordinate coord = new Coordinate();

            if (cardPosition > 4)
            {
                coord.y = 96;
                coord.x = (cardPosition - 5) * 71;
            }
            else
            {
                coord.y = 0;
                coord.x = cardPosition * 71;
            }


            return coord;
        }

        private void DrawCardOnPlayer(RectangleDimension playerBox, int cardPosition, System.Drawing.Bitmap picture)
        {
            Coordinate coord = PositionCoordinates(cardPosition);

            slate.DrawImage(picture, playerBox.x + coord.x, playerBox.y + coord.y, 71, 96);
        }



        // abstract meathod not implimented here
        public abstract void MouseMoveHandler(int mX, int mY);

        public abstract void MouseButtonHandler(int Mx, int My, MouseButtons Mb, CardGame game);



    }
}
