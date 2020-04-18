using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Text text;
    public void Resume()
    {
        GetComponentInChildren<UI>().showmenu = false;
        Debug.Log(1);
        GetComponentInChildren<CameraController>().canmovevision = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Stats()
    {
        GetComponentInChildren<UI>().stat.SetActive(true);
        GetComponentInChildren<UI>().showstat = true;

        string path = System.IO.Directory.GetCurrentDirectory();
        string fileName = @"\TXTstats\stats_";
        string[] lines = System.IO.File.ReadAllLines(path + fileName + PhotonNetwork.player.NickName + ".txt");
        string lestats = ""; 
        foreach (var line in lines)
        {
            lestats += line + System.Environment.NewLine;
        }
        text.text = lestats;
    }

    public void Option()
    {
        GetComponentInChildren<UI>().option.SetActive(true);
        GetComponentInChildren<UI>().showoption = true;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
