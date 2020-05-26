/*
 * Nathaniel Collier
 * 7/27/2008
 * 
 * This class will define the way to read and write to the settings file.
 * 
 * settings.xml
 *   - The file will be an XML file, The settings should be stored in some kind of
 *     string indexed array of strings (Thats what I would like), so that boolean values are stored as "false" and "true"
 *     then other information such as the location of graphics files could be stored as "C:\pictures" or something like that.
 *      
 *      settings['pictureFileLocation'] = "C:\pictures"; // this is how it should work ideally
 *   
 *   - Research will need to be done more into how xml files are read, some code for xml files has allready been explored under
 *     "\code\visual_studio_proj\c_sharp\console\xml_test" it should be usefull in finding out how to load the file.
 *   - Might want to create a schema for the file
 *         <group id="GeneralSettings">
 *            <option id="pictureFileLocation">C:\pictures</option>
 *            <option id="BackgroundGraphic">C:\pictures\background.bmp</option>
 *            <option id="GameType">Crazy8</option> <!-- possibilities: Crazy8 Poker Blackjack !-->
 *            <option id="Debug">false</option> <!-- displays player cards for everyone, and other info that makes testing easier !-->
 *         </group>
 *         <group id="Video">  <!-- the video is just an example probably something to be implimented far into the future !-->
 *            <option id="HorizontalDimension">1024</option>
 *            <option id="VerticalDimension">768</option>
 *         </group>
 *      Something like this should work very well (So try to get something like that done).
 */
using System;
using System.Collections;
using System.Xml;
using System.IO;

namespace crazy8
{
    class SettingsFile
    {
        // instance variables
        private Hashtable data = new Hashtable();
        private string filename;

        // constructors
        public SettingsFile(string file_name)
        {
            filename = file_name;

            LoadSettings();
            WriteSettings();
        }

        // properties

        /*
         *  This will be used to access the hash table
         */
        public string this[string index]
        {
            get
            {
                return data[index].ToString();
            }
            set
            {
                // This is so that the user can change the settings if we allow that

            }
        }

        // meathods

        /*
         * Loads values from the settings.xml document
         */
        public void LoadSettings()
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList GroupList;

            
            doc.Load(filename);
            // this should create a list of group as XmlNodes
            GroupList = doc.GetElementsByTagName("group");

            foreach (XmlNode node in GroupList)
            {
                if (node.Attributes["id"].Value == "GeneralSettings")
                {
                    // read in general settings
                    data.Add("picture_list", node["picture_list"].InnerText);
                    data.Add("background_file", node["background_file"].InnerText);
                    data.Add("game", node["game"].InnerText);
                    data.Add("debug", node["debug"].InnerText);
                    data.Add("window_title", node["window_title"].InnerText);
                }
                else if (node.Attributes["id"].Value == "Video")
                {
                    data.Add("width", node["width"].InnerText);
                    data.Add("height", node["height"].InnerText);
                }
                
            }
        }

        /*
         * Assuming the settings can be changed ingame we would write them back to the xml file here
         */
        public void WriteSettings()
        {
            // this will be easy to write we just write to the settings.xml file 
            // an exact copy of it except for the specific values we read from the hash table and
            // write those values to the file
            TextWriter tw = new StreamWriter(filename);

            tw.Write( "<settings>\n" +
                      "   <group id=\"GeneralSettings\">\n" +
                      "      <picture_list>"+data["picture_list"].ToString() + "</picture_list>\n" +
                      "      <background_file>"+data["background_file"].ToString() + "</background_file>\n" +
                      "      <game>"+data["game"].ToString()+"</game>\n" +
                      "      <debug>"+data["debug"].ToString()+"</debug>\n" +
                      "      <window_title>"+data["window_title"].ToString()+"</window_title>\n"+
                      "   </group>\n"+
                      "   <group id=\"Video\">\n"+
                      "      <width>"+data["width"].ToString()+"</width>\n"+
                      "      <height>"+data["height"].ToString()+"</height>\n"+
                      "   </group>\n"+
                      "</settings>\n" );

            tw.Close();

        }

        
        
    }
}
