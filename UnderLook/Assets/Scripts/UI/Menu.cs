using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Text text;
    public void Resume()
    {
        Debug.Log(1);
        GetComponentInChildren<UI>().showmenu = false;
        GetComponentInChildren<CameraController>().canmovevision = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Stats()
    {
        GetComponentInChildren<UI>().stat.SetActive(true);
        string path = System.IO.Directory.GetCurrentDirectory();
        string fileName = @"\TXTstats\stats_";
        string[] lines = System.IO.File.ReadAllLines(path + fileName + "jukantral.txt");
        string lestats = ""; 
        foreach (var line in lines)
        {
            lestats += line + System.Environment.NewLine;
        }
        text.text = lestats;
    }

    public void Option()
    {

    }

    public void Exit()
    {

    }
}
