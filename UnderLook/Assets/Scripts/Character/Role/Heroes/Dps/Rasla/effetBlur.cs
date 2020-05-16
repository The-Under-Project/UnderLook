using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class effetBlur : MonoBehaviour
{
    public GameObject[] ennemiteam = new GameObject[4];


    public GameObject grenada;
    public GameObject grenadaInstantiated;
    public GameObject shootpoint;

    public float rangeOfGrenada;
    public bool launchedgrenada = false;
    public string allyteam;
    public int damagepertick;

    DepthOfField blurness;

    public void GenerateEnnemiTeam(string allyteam)
    {
        this.allyteam = allyteam;
        int i = 0;
        blurness = null;
        foreach (var p in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (p.GetComponent<TeamColor>().enemieColor == allyteam)
            {
                p.GetComponentInChildren<PostProcessVolume>().profile.TryGetSettings(out blurness);
                if (blurness != null)
                {
                    ennemiteam[i++] = p;
                }

            }
            blurness = null;
        }
    }
    // Update is called once per frame
    public void Update()
    {
        if (launchedgrenada)
        {
            int i = 0;
            foreach (var p in ennemiteam)
            {
                if (p != null)
                {
                    blurness = null;
                    PostProcessVolume volume = p.GetComponentInChildren<PostProcessVolume>();
                    volume.profile.TryGetSettings(out blurness);
                    if (Mathf.Abs(p.transform.position.y - grenadaInstantiated.transform.position.y) < rangeOfGrenada && Mathf.Abs(p.transform.position.x - grenadaInstantiated.transform.position.x) < rangeOfGrenada && Mathf.Abs(p.transform.position.z - grenadaInstantiated.transform.position.z) < rangeOfGrenada)
                    {
                        if (blurness != null)
                            blurness.active = true;
                        p.GetComponent<Player.Base>().hp -= damagepertick;
                    }
                    else
                    {
                        if (blurness != null)
                        {
                            blurness.active = false;
                        }
                    }
                    i++;
                }
            }
        }

    }

    public void Deactivate()
    {
        foreach (var p in ennemiteam)
        {
            if (p != null)
            {
                blurness = null;
                PostProcessVolume volume = p.GetComponentInChildren<PostProcessVolume>();
                volume.profile.TryGetSettings(out blurness);
                if (blurness != null)
                {
                    blurness.active = false;
                }
            }
        }
    }
}