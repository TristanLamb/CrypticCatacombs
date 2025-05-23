﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;



namespace CrypticCatacombs
{
    public class Save
    {
        public int gameId, userId;
        public string gameName, baseFolder, backupFolder, backupPath;
        public bool loadingID = true;
        public XDocument saveFile;

        public Save(int GAMEID, string GAMENAME)
        {
            gameId = GAMEID;
            gameName = GAMENAME;
            //heroLevel = 1;

            //LoadGame();
            backupFolder = "bzaxcyk";
            backupPath = "bath";

            baseFolder = Globals.appDataFilePath +"\\"+gameName+"";

            CreateBaseFolders();

        }


        public void CreateBaseFolders()
        {
            CreateFolder(Globals.appDataFilePath +"\\"+gameName+"");
            CreateFolder(Globals.appDataFilePath +"\\"+gameName+"\\XML");
            CreateFolder(Globals.appDataFilePath +"\\"+gameName+"\\XML\\SavedGames");
        }

        public void CreateFolder(string s)
        {
            DirectoryInfo CreateSiteDirectory = new DirectoryInfo(s);
            if(!CreateSiteDirectory.Exists)
            {
                CreateSiteDirectory.Create();
            }
        }

        public virtual bool CheckIfFileExists(string PATH)
        {
            bool fileExists;

            fileExists = File.Exists(Globals.appDataFilePath + "\\"+gameName+"\\" + PATH);


            return fileExists;
            //return true;
        }



        public virtual void DeleteFile(string PATH)
        {
            File.Delete(PATH);
        }
        

        //only for XML
        public XDocument GetFile(string FILE)
        {
            if(CheckIfFileExists(FILE))
            {
                return XDocument.Load(Globals.appDataFilePath + "\\"+gameName+"\\"+FILE);
            }

            return null;
        }


        public virtual XDocument LoadFile(string FILEPATH, bool CHECKKEY = true)
        {
            XDocument xml = GetFile(FILEPATH);


            return xml;
        }




        public virtual void HandleSaveFormats(XDocument xml)
        {

            byte[] compress = Encoding.ASCII.GetBytes(StringToBinary(xml.ToString()));
            File.WriteAllBytes(Globals.appDataFilePath + "\\"+gameName+"\\XML\\SavedGames\\"+Convert.ToString(gameId, Globals.culture), compress);



        }

        //use for XML
        public virtual void HandleSaveFormats(XDocument xml, string PATH)
        {

            xml.Save(Globals.appDataFilePath + "\\"+gameName+"\\XML\\"+PATH);


        }


        #region Converting to Binary and back

        public static string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach(char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for(int i = 0;i < data.Length;i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }

            return Encoding.ASCII.GetString(byteList.ToArray());
        }
        #endregion



    }
}
