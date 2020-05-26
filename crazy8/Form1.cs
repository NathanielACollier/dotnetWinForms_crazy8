using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace crazy8
{
    public partial class Form1 : Form
    {
        enum GameType
        {
            POKER, CRAZY8, BLACKJACK
        };

 

        
        GameType game = GameType.POKER;


        PokerGraphics PokerBoard;
        Crazy8Graphics Crazy8Board;

        

        Crazy8Game Crazy8;
        PokerGame Poker;

        SettingsFile Settings;
        

        public Form1()
        {

            InitializeComponent();

            Settings = new SettingsFile("settings.xml");

            // setup window
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            this.Text = Settings["window_title"];


            this.Size = new Size( Convert.ToInt32( Settings["width"] ), Convert.ToInt32( Settings["height"] ) );


            // set the game type
            if (Settings["game"] == "Crazy8")
                game = GameType.CRAZY8;
            else if (Settings["game"] == "Poker")
                game = GameType.POKER;
            else if (Settings["game"] == "Blackjack")
                game = GameType.BLACKJACK;

        
            // initialize the games

            switch (game)
            {
                case GameType.POKER:
                    PokerBoard = new PokerGraphics(this.Width, this.Height, Settings["picture_list"]);
                    Poker = new PokerGame(PokerBoard);
                    Poker.Play();
                    break;
                case GameType.CRAZY8:
                    Crazy8Board = new Crazy8Graphics(this.Width, this.Height, Settings["picture_list"]);
                    Crazy8 = new Crazy8Game(Crazy8Board);
                    Crazy8.Play();
                    break;
            }


        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            switch (game)
            {
                case GameType.POKER:
                    PokerBoard.MouseButtonHandler(e.X, e.Y, e.Button, Poker);
                    break;
                case GameType.CRAZY8:
                    Crazy8Board.MouseButtonHandler(e.X, e.Y, e.Button, Crazy8);
                    break;
            }
           
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            switch (game)
            {
                case GameType.POKER:
                    PokerBoard.MouseMoveHandler(e.X, e.Y);
                    break;
                case GameType.CRAZY8:
                    Crazy8Board.MouseMoveHandler(e.X, e.Y);
                    break;
            }

            this.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            switch (game)
            {
                case GameType.POKER:
                    PokerBoard.ReDraw(e.Graphics);
                    break;
                case GameType.CRAZY8:
                    Crazy8Board.ReDraw(e.Graphics);
                    break;
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            bool update_screen = false;

           

            switch (e.KeyCode)
            {
                case Keys.Q:
                case Keys.Escape:
                    Application.Exit();
                    break;
                case Keys.Left:
                    // show next picture to the left in the picture list
                    //topCard.DrawImage(slate,  pic_list.get_left_bitmap() );
                    update_screen = true;
                    break;
                case Keys.Right:
                    // show next picture to the right in picture list
                    //topCard.DrawImage(slate, pic_list.get_right_bitmap() );
                    update_screen = true;
                    break;
            }

            if (update_screen)
            {
                this.Refresh();
            }
        }


        /*
         I'm not sure if this is the right one to use (Look it up later )
          but for now I'm putting everything that needs to happen when the 
          game ends in here
         */
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            switch (game)
            {
                case GameType.POKER:
                    Poker.ExitGame();
                    break;
                case GameType.CRAZY8:
                    Crazy8.ExitGame();
                    break;
            }

            Settings.WriteSettings(); // make sure any changes to the settings file are written when we exit the game
        }

        


    }
}
