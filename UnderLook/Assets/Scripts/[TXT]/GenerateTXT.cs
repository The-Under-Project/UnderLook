using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace Basics
{
    public class GenerateTXT : MonoBehaviour
    {
        public string PlayerName;
        [SerializeField] private bool trigger;
        [SerializeField] private bool overwrite;
        public void Update()
        {
            PlayerName = PhotonNetwork.player.NickName;
            if (trigger)
            {
                trigger = false;
                Launch();
            }
            if (overwrite)
            {
                overwrite = false;
                OverWrite();
            }
        }

        public void Launch()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            Debug.Log(path);

            if(!Directory.Exists("TXTstats"))
                Directory.CreateDirectory("TXTstats");
            else
                Debug.Log("Folder Already Exists");

            string fileName = @"TXTstats\stats_"; //..\..\..\..\


            fileName += PlayerName + ".txt";
            fileName = fileName.Replace(" ", "");
            fileName = fileName.ToLower();
            Debug.Log(fileName);

            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    Debug.Log("File Already Exists");
                }
                else
                {
                    using (StreamWriter sw = File.CreateText(fileName))
                    {
                        //Debug.Log(path);
                        Debug.Log("created: " + fileName);
                        sw.WriteLine("Name: " + PlayerName);
                        sw.WriteLine("Game_played: 0");
                        sw.WriteLine("Game_won: 0");
                        sw.WriteLine("Kills: 0");
                        sw.WriteLine("Deaths: 0");
                    }
                }	

            }
            catch 
            {
                Debug.Log("Wrong input");
            }
        }

        public void OverWrite()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            string fileName = @"TXTstats\stats_"; //..\..\..\..\


            fileName += PlayerName + ".txt";
            fileName = fileName.Replace(" ", "");
            fileName = fileName.ToLower();

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
        public void Add(int line, string playerName)
        {
            if (line > 1 && line < 6) // pas overwrite le nom ou des valeurs end ehors du txt
            {
                int line_to_edit = line; // Warning: 1-based indexing!
                string sourceFile = (@"TXTstats\stats_" + playerName + ".txt");
                string destinationFile = sourceFile;

                // Read the old file.
                string[] lines = File.ReadAllLines(destinationFile);

                // Write the new file over the old file.
                using (StreamWriter writer = new StreamWriter(destinationFile))
                {
                    for (int currentLine = 1; currentLine <= lines.Length; ++currentLine)
                    {
                        if (currentLine == line_to_edit)
                        {
                            string[] split = lines[currentLine - 1].Split(new char[] { ' ' });
                            int t = 0;
                            Int32.TryParse(split[1], out t);
                            t += 1;
                            writer.WriteLine(split[0] + " " +  t);
                        }
                        else
                        {
                            writer.WriteLine(lines[currentLine - 1]);
                        }
                    }
                }
            }
        }
    }
}