using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTXT : MonoBehaviour
{
    public bool trigger;
    public string playerName;
    public int line;
    public int toModify;
    void Update()
    {
        if (trigger)
        {
            trigger = false;
            Launch();
        }
    }
    void Launch()
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
                        writer.WriteLine(split[0] + " " + toModify);
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
