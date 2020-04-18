using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithCannon : MonoBehaviour
{
    public GameObject dollycart;
    public GameObject card;

    public float actualTime;
    private float TimeMax = 5;
    public bool canCannon;

    public void Start()
    {
        dollycart = Instantiate(card, transform.position, new Quaternion(0, 0, 0, 0));
        dollycart.GetComponent<TrackSpeed>().player = gameObject;
    }
    public void Update()
    {
        DoTime();
    }

    private void OnTriggerEnter(Collider other) //if collide with nametag cannon
    {
        if (other.tag == "Cannon")
        {
            if (other.GetComponent<colorCannon>().teamColor == GetComponent<TeamColor>().teamColor && canCannon)
            {
                canCannon = false;
                actualTime = TimeMax;
                Destroy(dollycart);
                dollycart = Instantiate(card, transform.position, new Quaternion(0, 0, 0, 0));
                dollycart.GetComponent<TrackSpeed>().player = gameObject;

                if (GetComponent<TeamColor>().teamColor == "Red")
                    dollycart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path = GameObject.FindGameObjectWithTag("CannonRedPath").GetComponent<Cinemachine.CinemachineSmoothPath>();
                else
                    dollycart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path = GameObject.FindGameObjectWithTag("CannonBluePath").GetComponent<Cinemachine.CinemachineSmoothPath>();


                gameObject.transform.SetParent(dollycart.transform);
                //Time.timeScale = 0.01f;
                gameObject.GetComponent<Moving>().isOnTrack = true;
                gameObject.GetComponent<Moving>().DOLaunch();
                other.gameObject.GetComponentInChildren<AnimationCannon>().test = true;
            }
        }
    }
    private void DoTime()
    {
        if (actualTime <= 0)
        {
            actualTime = TimeMax;
            canCannon = true;
        }
        else if (!canCannon)
        {
            actualTime -= Time.deltaTime;
        }
    }
}
