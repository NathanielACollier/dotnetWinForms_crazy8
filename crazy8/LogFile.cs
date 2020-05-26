/*
 Nathaniel Collier
 7/24/2008
 Log File class.. Used to write to log files
*/

using System;
using System.IO;


namespace crazy8
{
    public class LogFile
    {
        // instance variables
        string filename;
        FileStream file;
        StreamWriter log;

        // constructors

        public LogFile(string filename_ )
        {
            filename = filename_;

            // open the file
            file = new FileStream(filename, FileMode.Append, FileAccess.Write);

            // create a stream writer to access the file
            log = new StreamWriter(file);

            log.WriteLine("----------------[" + DateTime.Now + "]----------------");
        }

        // properties


        // meathods

        public void Write(string text)
        {
            // could add the date or time to every line at some point
            log.Write(text);
        }

        /* 
         * Same as write except a new line is added before putting the text in the file
         */
        public void WriteLine(string text)
        {
            log.WriteLine(text);
        }

        public void Close()
        {
            log.Close();
            file.Close();
        }

    }
}