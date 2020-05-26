using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace crazy8
{



    public class picture_list
    {
        List<String> file_location = new List<string>();
        

        int current_pic = 0;
        int length = 0;

        public picture_list(String picture_list_file )
        {
           // constructor

           // load the images from the file 
            load(picture_list_file);

            
        }

        /*
          loads the picture list and creates all of the bitmap images in memory
        */
        public void load(String filename)
        {
           TextReader fin = new StreamReader(filename);
           String line;

           while( (line = fin.ReadLine()) != null )
           {
              // each line of the file should be the file location for the bitmap so
               ++length;
               file_location.Add(line);
           }
        }

        public System.Drawing.Bitmap get_bitmap(String card_name)
        {
            return null;
        }


        public System.Drawing.Bitmap this[int index]
        {
            get
            {
                int count = 0;
                String text = new String('\b', 1);

                foreach (String line in file_location)
                {
                    if (count == index)
                    {
                        text = line;
                        break;
                    }
                    ++count;
                }

                return new System.Drawing.Bitmap(text);
            }
            set
            {
                // leave this blank
            }
        }


        /*
         * Gets the current bitmap
         *   does not alter the current_pic index
         */
        public System.Drawing.Bitmap get_current_bitmap()
        {
            // make sure that current_pic is within the limits
            if (current_pic >= length) // length is the number of elements in the picture list
            {
                current_pic = 0;
            }


            return this[current_pic];
        }

        /*
         * Gets the bitmap to the left of the current one and assigns this new
         * index to the current_pic
         */
        public System.Drawing.Bitmap get_left_bitmap()
        {
            if (current_pic == 0)
            {
                current_pic = length - 1;
            }
            else
            {
                --current_pic;
            }

            return get_current_bitmap();
        }

        /*
         * Gets the bitmap to the right of the current one and assigns this new
         *  index to the current_pic
         */
        public System.Drawing.Bitmap get_right_bitmap()
        {
            if (current_pic >= (length - 1))
            {
                current_pic = 0;
            }
            else
            {
                ++current_pic;
            }

            return get_current_bitmap();
        }

    }
}
