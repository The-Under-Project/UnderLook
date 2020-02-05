using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkingDeactivate : MonoBehaviour
{


    [SerializeField] private GameObject playerCamera;
    [SerializeField] private MonoBehaviour[] scriptsToIgnore;


    private PhotonView photonView;

    // Use this for initialization
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        Initialize();
    }
    void Initialize()
    {
        if (photonView.isMine)
        {

        }
        else
        {
            if (playerCamera != null)
                playerCamera.SetActive(false);
            
            foreach (MonoBehaviour item in scriptsToIgnore)
            {
                item.enabled = false;
            }
        }
    }

}
