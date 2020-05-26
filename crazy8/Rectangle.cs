using System;
using System.Collections.Generic;
using System.Text;

namespace crazy8
{
    public class Coordinate
    {
        // instance variables
        public int x=0;
        public int y=0;
        public int z=0;

        public Coordinate(int x_, int y_, int z_) { x = x_; y = y_; z = z_; }

        public Coordinate() { }


    }

    public class RectangleDimension
    {
        public int width = 0;
        public int height = 0;
        public int x = 0;
        public int y = 0;

        // initialization constructor
        public RectangleDimension( int x_, int y_,int w, int h)
        {
            width = w;
            height = h;
            x = x_;
            y = y_;
        }
        public RectangleDimension()
        {
        }
        // copy constructor
        public RectangleDimension(RectangleDimension copy)
        {
            this.width = copy.width;
            this.height = copy.height;
            this.x = copy.x;
            this.y = copy.y;
        }
        // conversion constructor
        public RectangleDimension(RectangleCoordinate coord)
        {
            this.x = coord.TopRightXPos;
            this.y = coord.TopRightYPos;
            this.width = (coord.BottomLeftXPos - coord.TopRightXPos);
            this.height = (coord.BottomLeftYPos - coord.TopRightYPos);
        }
    }

    public class RectangleCoordinate
    {
        public int TopRightXPos=0;
        public int TopRightYPos=0;
        public int BottomLeftXPos = 0;
        public int BottomLeftYPos = 0;

        public RectangleCoordinate(int urx, int ury, int blx, int bly) 
        { 
            TopRightXPos = urx; 
            TopRightYPos = ury; 
            BottomLeftXPos = blx; 
            BottomLeftYPos = bly; 
        }
        public RectangleCoordinate() 
        { 
        }
        public RectangleCoordinate(RectangleCoordinate copy) 
        { 
            this.TopRightXPos = copy.TopRightXPos; 
            this.TopRightYPos = copy.TopRightYPos; 
            this.BottomLeftXPos = copy.BottomLeftXPos; 
            this.BottomLeftYPos = copy.BottomLeftYPos; 
        }
        public RectangleCoordinate(RectangleDimension dim )
        {
            this.TopRightXPos=dim.x;
            this.TopRightYPos=dim.y;
            this.BottomLeftXPos=(dim.width+dim.x);
            this.BottomLeftYPos=(dim.height+dim.y);
        }

    }


    public class RectangleType
    {
        // instance variables
        RectangleCoordinate rectCoord;
        RectangleDimension rectDim;
        System.Drawing.Bitmap savedData;
        Boolean hidden = false;


        // constructors

        public RectangleType( RectangleCoordinate coord )
        {
            rectCoord = new RectangleCoordinate( coord );
            rectDim = new RectangleDimension( coord );
        }

        public RectangleType(RectangleDimension dim )
        {
            rectCoord = new RectangleCoordinate(dim);
            rectDim = new RectangleDimension(dim);
        }
        // properties

        // methods

        public RectangleDimension Dimension
        {
            get
            {
                return rectDim;
            }
        }

        public RectangleCoordinate Coordinate
        {
            get
            {
                return rectCoord;
            }
        }
        /*
         g is the surface/graphic object we are going to draw onto and p is the pen
         */
        public void Draw( System.Drawing.Graphics g, System.Drawing.Pen p )
        {
            g.DrawRectangle(p, rectDim.x, rectDim.y, rectDim.width, rectDim.height);
        }

        /*
         once again g is the surface/graphic object we are going to draw onto and b
         is the brush that we will use to draw with
         */
        public void Fill(System.Drawing.Graphics g, System.Drawing.Brush b)
        {
            g.FillRectangle( b, rectDim.x, rectDim.y, rectDim.width, rectDim.height);
        }

        /*
          surface is the graphic we will be drawing onto
         */
        public void DrawImage( System.Drawing.Graphics surface, System.Drawing.Bitmap picture )
        {
            surface.DrawImage(picture, rectDim.x, rectDim.y );
        }

        public void DrawText(System.Drawing.Graphics surface, string text,System.Drawing.Font f,  System.Drawing.Brush b)
        {
            surface.DrawString(text, f, b, rectDim.x, rectDim.y);
        }

        /*
         Clears the rectangle out completel
         */
        public void Clear(System.Drawing.Graphics surface)
        {
            Fill(surface, System.Drawing.Brushes.White);
        }

        /*
         Checks to see if a particular point is within the dimensions of the rectangle
         */
        public Boolean CheckPoint(int x, int y)
        {
            // if the object is not there the user can't click in it... (simplest way to do this)
            if (hidden == true) return false;

            bool x_range = false;
            bool y_range = false;

            // is the x coordinate in range
            if (x >= rectDim.x && x <= (rectDim.x + rectDim.width))
                x_range = true;

            if (y >= rectDim.y && y <= (rectDim.y + rectDim.height))
                y_range = true;

            // since both have to be true for the point to be inside the rectangle
            // we do this
            return x_range && y_range;
        }
        
        /*
         Saves an object then erases it
         */
        public void Hide(System.Drawing.Graphics slate, System.Drawing.Bitmap data )
        {
            hidden = true;
            savedData = data.Clone(new System.Drawing.Rectangle(rectDim.x, rectDim.y, rectDim.width, rectDim.height),System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            Fill(slate, System.Drawing.Brushes.White);

        }

        /*
         Restores an erased object
         */
        public void Show(System.Drawing.Graphics slate)
        {
            hidden = false;
            if (savedData == null)
                return; // do nothing if we haven't hid the area

            DrawImage(slate, savedData);
        }
    }
}
