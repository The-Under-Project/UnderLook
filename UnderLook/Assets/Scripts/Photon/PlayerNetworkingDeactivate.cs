using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkingDeactivate : MonoBehaviour
{


    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject[] myobjectsToIgnore;
    [SerializeField] private MonoBehaviour[] scriptsToIgnore;
    [SerializeField] private GameObject[] objectsToIgnore;


    private PhotonView photonView;

    // Use this for initialization
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        Initialize();
    }
    public void Initialize()
    {
        if (photonView.isMine)
        {
            foreach (var item in myobjectsToIgnore)
            {
                item.SetActive(false);
            }
        }
        else
        {
            if (playerCamera != null)
                playerCamera.SetActive(false);
            
            foreach (MonoBehaviour item in scriptsToIgnore)
            {
                item.enabled = false;
            }

            foreach (GameObject item in objectsToIgnore)
            {
                item.SetActive(false);
            }
        }
    }

}
