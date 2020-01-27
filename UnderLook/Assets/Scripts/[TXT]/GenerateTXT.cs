using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Basics
{
    public class GenerateTXT : MonoBehaviour
    {
        public string PlayerName;
        [SerializeField] private bool trigger;
        public void Update()
        {
            if (trigger)
            {
                trigger = false;
                Launch();
            }
        }

        public void Launch()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            string fileName = @"TXTstats\stats_"; //..\..\..\..\


            fileName += PlayerName + ".txt";
            fileName = fileName.Replace(" ", "");
            fileName = fileName.ToLower();

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
                        sw.WriteLine("Game played: 0");
                        sw.WriteLine("Game won: 0");
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
    }
}