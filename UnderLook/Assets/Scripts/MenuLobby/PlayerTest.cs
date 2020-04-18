using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTest : MonoBehaviour
{
    public Canvas canvas;
    void Start()
    {
        gameObject.GetComponent<Text>().text = gameObject.GetComponentInParent<PhotonView>().owner.NickName; //just pick the nickname setup in "GetNameField"
        if (gameObject.GetComponentInParent<PhotonView>().isMine)
        {
            canvas.enabled = false;
        }
    }

}
